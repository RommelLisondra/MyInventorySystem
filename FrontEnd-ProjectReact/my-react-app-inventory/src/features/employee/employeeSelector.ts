import type { RootState } from "../../app/store";

export const selectEmployees = (state: RootState) => state.employee.employees;
export const selectEmployeeLoading = (state: RootState) => state.employee.loading;
export const selectEmployeeError = (state: RootState) => state.employee.error;

// Optional: selector para sa isang customer by id
export const selectEmployeeById = (id: number) => (state: RootState) =>
  state.employee.employees.find((c) => c.id === id);
