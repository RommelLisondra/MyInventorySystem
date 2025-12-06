import type { RootState } from "../../app/store";

export const selectEmployeeSalesRef = (state: RootState) => state.employeeSalesRef.employeeSalesRef;
export const selectEmployeeSalesRefLoading = (state: RootState) => state.employeeSalesRef.loading;
export const selectEmployeeSalesRefError = (state: RootState) => state.employeeSalesRef.error;

// Optional: selector para sa isang EmployeeSalesRef by id
export const selectEmployeeSalesRefById = (id: number) => (state: RootState) =>
  state.employeeSalesRef.employeeSalesRef.find((c) => c.id === id);
