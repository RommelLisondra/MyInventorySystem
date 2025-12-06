import type { RootState } from "../../app/store";

export const selectEmployeeDeliverer = (state: RootState) => state.employeeDeliverer.employeeDelivered;
export const selectEmployeeDelivererLoading = (state: RootState) => state.employeeDeliverer.loading;
export const selectEmployeeDelivererError = (state: RootState) => state.employeeDeliverer.error;

// Optional: selector para sa isang EmployeeDeliverer by id
export const selectEmployeeDelivererById = (id: number) => (state: RootState) =>
  state.employeeDeliverer.employeeDelivered.find((c) => c.id === id);
