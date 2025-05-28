import { describe, it, expect, beforeEach } from "vitest";
import { setActivePinia, createPinia } from "pinia";
import { useDeviceStore, api } from "@/stores/devices";
import MockAdapter from "axios-mock-adapter";

describe("DeviceStore", () => {
  let mockAxios;

  beforeEach(() => {
    setActivePinia(createPinia());
    mockAxios = new MockAdapter(api);
  });

  it("fetchDevices GET", async () => {
    const store = useDeviceStore();
    const devices = [{ id: 1, name: "Test 1" }];
    mockAxios
      .onGet("https://localhost:7020/api/Device/myFilteredDevices")
      .reply(200, devices);

    await store.fetchDevices("");
    expect(store.devices).toEqual(devices);
  });

  it("fetchNonChargingDevices GET", async () => {
    const store = useDeviceStore();
    const devices = [{ id: 2, name: "TestNonCharging" }];
    mockAxios
      .onGet("https://localhost:7020/api/Device/myNonChargingDevices")
      .reply(200, devices);

    await store.fetchNonChargingDevices("");
    expect(store.devices).toEqual(devices);
  });

  it("addDevice POST", async () => {
    const store = useDeviceStore();
    const newDevice = { id: 3, name: "NewDevice" };

    mockAxios.onPost("https://localhost:7020/api/Device").reply(200);
    mockAxios
      .onGet("https://localhost:7020/api/Device/myFilteredDevices")
      .reply(200, [newDevice]);

    await store.addDevice(newDevice);
    expect(store.devices).toEqual([newDevice]);
  });

  it("saveDevice PUT", async () => {
    const store = useDeviceStore();
    const deviceToSave = { id: 4, name: "SavedDevice" };

    mockAxios.onPut("https://localhost:7020/api/Device").reply(200);
    mockAxios
      .onGet("https://localhost:7020/api/Device/myFilteredDevices")
      .reply(200, [deviceToSave]);

    await store.saveDevice(deviceToSave);
    expect(store.devices).toEqual([deviceToSave]);
  });

  it("deleteDevice DELETE", async () => {
    const store = useDeviceStore();
    const deviceId = 5;

    mockAxios
      .onDelete(`https://localhost:7020/api/Device/${deviceId}`)
      .reply(200);
    mockAxios
      .onGet("https://localhost:7020/api/Device/myFilteredDevices", {
        params: { input: "" },
      })
      .reply(200, []);

    await store.deleteDevice(deviceId);
    expect(store.devices).toEqual([]);
  });
});
