import { describe, it, expect, beforeEach } from "vitest";
import { setActivePinia, createPinia } from "pinia";
import { useChargingStationStore, api } from "@/stores/chargingStations";
import MockAdapter from "axios-mock-adapter";

describe("ChargingStationStore", () => {
  let mockAxios;

  beforeEach(() => {
    setActivePinia(createPinia());
    mockAxios = new MockAdapter(api);
  });

  it("fetchStations GET", async () => {
    const store = useChargingStationStore();
    const stations = [{ id: 1, name: "Station 1" }];

    mockAxios
      .onGet("https://localhost:7020/api/chargingStation/FilteredStations", {
        params: { input: "" },
      })
      .reply(200, stations);

    await store.fetchStations("");
    expect(store.chargingStations).toEqual(stations);
  });
});
