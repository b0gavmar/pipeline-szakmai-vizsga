import { describe, it, expect, beforeEach, vi } from 'vitest'

import { mount } from '@vue/test-utils'
import { setActivePinia, createPinia } from 'pinia'
import { createTestingPinia } from '@pinia/testing'
import ChargingInstancesView from '@/views/ChargingInstancesView.vue'

import { createRouter, createMemoryHistory } from 'vue-router'

import { useUserStore } from '@/stores/user'
import { useToast } from 'vue-toastification'


const toastMock = {
  error: vi.fn(),
  success: vi.fn(),
}

vi.mock('vue-toastification', () => ({
  useToast: () => toastMock,
}))

describe('ChargingInstances', () => {
  beforeEach(() => {
    setActivePinia(createPinia())
  })

  it('renders properly', () => {
    const wrapper = mount(ChargingInstancesView, {
      global: {
        plugins: [
          createTestingPinia({
            createSpy: vi.fn,
          }),
        ],
        stubs: {
          ChargingInstanceCard: true,
          DateRangeFilter: true,
        },
      },
    })
    expect(wrapper.text()).toContain('My number of charging instances:')
  })

  it('displays all ongoing charging instances', async () => {
    const wrapper = mount(ChargingInstancesView, {
      global: {
        plugins: [
          createTestingPinia({
            createSpy: vi.fn,
            initialState: {
              chargingInstance: {
                ongoingChargingInstances: [
                  { id: '1', start: '2025-04-24T11:56:03', end: null },
                  { id: '2', start: '2025-04-24T11:57:03', end: null },
                  { id: '3', start: '2025-04-24T11:58:03', end: null },
                ],
              },
            },
          }),
        ],
        stubs: {
          ChargingInstanceCard: true,
          DateRangeFilter: true,
        },
      },
    })

    const cards = wrapper.findAllComponents({ name: 'ChargingInstanceCard' })
    expect(cards.length).toBe(3)
  })
})

describe('Router', () => {
  it('navigates to profile page', async () => {
    const router = createRouter({
      history: createMemoryHistory(),
      routes: [
        { path: '/', component: { template: '<div>Home</div>' } },
        { path: '/profile', component: { template: '<div>Profile</div>' } },
      ],
    })

    await router.push('/profile')
    await router.isReady()

    expect(router.currentRoute.value.fullPath).toBe('/profile')
  })
})


describe('useUserStore', () => {
  beforeEach(() => {
    setActivePinia(createPinia())
    vi.clearAllMocks()
  })

  it('shows error if email is empty on updateUser', async () => {
    const store = useUserStore()

    const userToUpdate = {
      id: '123',
      email: '',
      name: 'Test User',
    }

    await store.updateUser(userToUpdate)

    expect(toastMock.error).toHaveBeenCalledWith('Email is empty or is in an invalid format!')
  })

  it('shows error if email is invalid on updateUser', async () => {
    const store = useUserStore()

    const userToUpdate = {
      id: '123',
      email: 'noDotOrAt',
      name: 'Test User',
    }

    await store.updateUser(userToUpdate)

    expect(toastMock.error).toHaveBeenCalledWith('Email is empty or is in an invalid format!')
  })
})