import type { RootState } from "../../app/store";

export const selectCustomers = (state: RootState) => state.employeeChecker.employeeCheckers;
export const selectCustomerLoading = (state: RootState) => state.employeeChecker.loading;
export const selectCustomerError = (state: RootState) => state.employeeChecker.error;

// Optional: selector para sa isang customer by id
export const selectCustomerById = (id: number) => (state: RootState) =>
  state.employeeChecker.employeeCheckers.find((c) => c.id === id);
