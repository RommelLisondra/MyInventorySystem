import type { RootState } from "../../app/store";

export const selectCustomers = (state: RootState) => state.customer.customers;
export const selectCustomerLoading = (state: RootState) => state.customer.loading;
export const selectCustomerError = (state: RootState) => state.customer.error;

// Optional: selector para sa isang customer by id
export const selectCustomerById = (id: number) => (state: RootState) =>
  state.customer.customers.find((c) => c.id === id);
