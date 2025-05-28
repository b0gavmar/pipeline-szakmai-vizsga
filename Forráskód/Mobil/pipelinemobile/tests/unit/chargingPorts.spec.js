import { describe, it, expect, beforeEach } from "vitest";
import { setActivePinia, createPinia } from "pinia";
import { useChargingPortStore, api } from "@/stores/chargingPorts";
import MockAdapter from "axios-mock-adapter";

describe("ChargingPortStore", () => {
  let mockAxios;

  beforeEach(() => {
    setActivePinia(createPinia());
    mockAxios = new MockAdapter(api);
  });

  it("fetchPorts GET", async () => {
    const store = useChargingPortStore();
    const stationId = 123;
    const ports = [{ id: 1, portNumber: "Port 1" }];

    mockAxios
      .onGet(
        `https://localhost:7020/api/ChargingPort/FilteredPortsOfChargingStation/${stationId}`
      )
      .reply(200, ports);

    await store.fetchPorts(stationId);
    expect(store.chargingPorts).toEqual(ports);
  });
});
