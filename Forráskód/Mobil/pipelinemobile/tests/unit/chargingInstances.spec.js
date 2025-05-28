import { beforeEach, describe, expect } from "vitest";
import { createPinia, setActivePinia } from "pinia";
import { useChargingInstanceStore, api } from "@/stores/chargingInstances";
import MockAdapter from "axios-mock-adapter";
describe("ChargingInstanceStore", () => {
  let mockAxios;
  beforeEach(() => {
    setActivePinia(createPinia());
    mockAxios = new MockAdapter(api);
  });

  it("fetchOngoingChargingInstances GET", async () => {
    const store = useChargingInstanceStore();
    const ongoing = [{ chargingPortId: 123, deviceId: 123 }];
    mockAxios
      .onGet("https://localhost:7020/api/charginginstance/myOngoingInstances")
      .reply(200, ongoing);

    await store.fetchOngoingChargingInstances();
    expect(store.ongoingChargingInstances).toEqual(ongoing);
  });

  it("newChargingInstance POST", async () => {
    const store = useChargingInstanceStore();
    const instance = { chargingPortId: 123, deviceId: 123 };
    mockAxios.onPost("https://localhost:7020/api/charginginstance").reply(200);
    mockAxios
      .onGet("https://localhost:7020/api/charginginstance/myOngoingInstances")
      .reply(200, [instance]);

    await store.newChargingInstance(instance);
    expect(store.ongoingChargingInstances).toEqual([instance]);
  });
});
