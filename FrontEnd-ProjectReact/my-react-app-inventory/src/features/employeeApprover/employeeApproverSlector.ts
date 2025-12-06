import type { RootState } from "../../app/store";

export const selectEmployeeApprover = (state: RootState) => state.employeeApprover.employeeApprover;
export const selectEmployeeApproverLoading = (state: RootState) => state.employeeApprover.loading;
export const selectEmployeeApproverError = (state: RootState) => state.employeeApprover.error;

// Optional: selector para sa isang customer by id
export const selectEmployeeApproverById = (id: number) => (state: RootState) =>
  state.employeeApprover.employeeApprover.find((c) => c.id === id);
