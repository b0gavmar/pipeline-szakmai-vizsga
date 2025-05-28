import { describe, it, expect, beforeEach } from "vitest";
import { setActivePinia, createPinia } from "pinia";
import { useUserStore, api } from "@/stores/user";
import axios from "axios";
import MockAdapter from "axios-mock-adapter";

describe("UserStore", () => {
  let mockAxios;

  beforeEach(() => {
    setActivePinia(createPinia());
    mockAxios = new MockAdapter(api);
  });

  it("checkAuth GET", async () => {
    const store = useUserStore();

    mockAxios.onGet("https://localhost:7020/api/ApplicationUser/me").reply(200);

    const result = await store.checkAuth();
    expect(result).toBe(true);
  });

  it("checkAuth GET fail", async () => {
    const store = useUserStore();

    mockAxios.onGet("https://localhost:7020/api/ApplicationUser/me").reply(401);

    const result = await store.checkAuth();
    expect(result).toBe(false);
  });

  it("fetchUser GET", async () => {
    const store = useUserStore();
    const userData = { id: 2, email: "example@test.com" };

    mockAxios
      .onGet("https://localhost:7020/api/ApplicationUser/me")
      .reply(200, userData);

    await store.fetchUser();
    expect(store.user).toEqual(userData);
    expect(store.isAuthenticated).toBe(true);
  });

  it("login POST + userdata", async () => {
    const store = useUserStore();
    const userData = { id: 1, email: "test@test.com" };

    mockAxios
      .onPost("https://localhost:7020/api/Authentication/login")
      .reply(200);
    mockAxios
      .onGet("https://localhost:7020/api/ApplicationUser/me")
      .reply(200, userData);

    await store.login("test@test.com", "password");
    expect(store.user).toEqual(userData);
    expect(store.isAuthenticated).toBe(true);
  });

  it("register POST + userdata", async () => {
    const store = useUserStore();
    const userData = { id: 3, email: "new@user.com" };

    mockAxios
      .onPost("https://localhost:7020/api/Authentication/register")
      .reply(200);
    mockAxios
      .onGet("https://localhost:7020/api/ApplicationUser/me")
      .reply(200, userData);

    await store.register("new@user.com", "pw123");
    expect(store.user).toEqual(userData);
    expect(store.isAuthenticated).toBe(true);
  });

  it("logout POST", async () => {
    const store = useUserStore();
    store.user = { id: 1, email: "test@test.com" };
    store.isAuthenticated = true;

    mockAxios
      .onPost("https://localhost:7020/api/Authentication/logout")
      .reply(200);

    await store.logout();
    expect(store.user).toBe(null);
    expect(store.isAuthenticated).toBe(false);
  });

  it("updateUser PUT", async () => {
    const store = useUserStore();
    const updatedUser = { id: 1, email: "updated@user.com" };

    mockAxios.onPut("https://localhost:7020/api/ApplicationUser").reply(200);

    await store.updateUser(updatedUser);
    expect(store.user).toEqual(updatedUser);
  });
});
