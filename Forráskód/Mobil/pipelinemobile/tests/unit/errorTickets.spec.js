import { describe, it, expect, beforeEach } from "vitest";
import { setActivePinia, createPinia } from "pinia";
import { useErrorTicketStore, api } from "@/stores/errortickets";
import MockAdapter from "axios-mock-adapter";

describe("ErrorTicketStore", () => {
  let mockAxios;

  beforeEach(() => {
    setActivePinia(createPinia());
    mockAxios = new MockAdapter(api);
  });

  it("fetchErrorTicketsOfStation GET", async () => {
    const store = useErrorTicketStore();
    const stationId = 123;
    const tickets = [
      { id: 1, description: "Error 1" },
      { id: 2, description: "Error 2" },
    ];

    mockAxios
      .onGet(
        `https://localhost:7020/api/errorTicket/TicketsOfChargingStation/${stationId}`
      )
      .reply(200, tickets);

    await store.fetchErrorTicketsOfStation(stationId);
    expect(store.errorTickets).toEqual(tickets);
  });

  it("newErrorTicket POST + refresh", async () => {
    const store = useErrorTicketStore();
    const newTicket = {
      id: 2,
      description: "Error B",
      chargingStationId: 456,
    };

    mockAxios.onPost("https://localhost:7020/api/errorTicket/").reply(200);
    mockAxios
      .onGet(
        `https://localhost:7020/api/errorTicket/TicketsOfChargingStation/${newTicket.chargingStationId}`
      )
      .reply(200, [newTicket]);

    await store.newErrorTicket(newTicket);
    expect(store.errorTickets).toEqual([newTicket]);
  });
});
