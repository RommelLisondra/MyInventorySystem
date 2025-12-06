import { EMPLOYEE_API } from "../constants/api";
import type { EmployeeApprover } from "../types/employeeApprover";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Employee API =====
export const getEmployeeApprovers = async (): Promise<EmployeeApprover[]> => {
  return authFetch(EMPLOYEE_API);
};

export const getEmployeeApproversPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<EmployeeApprover[]>> => {
  return authFetch(`${EMPLOYEE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getEmployeeApproverById = async (id: number): Promise<EmployeeApprover> => {
  return authFetch(`${EMPLOYEE_API}/${id}`);
};

export const createEmployeeApprover = async (employee: EmployeeApprover): Promise<EmployeeApprover> => {
  return authFetch(EMPLOYEE_API, {
    method: "POST",
    body: JSON.stringify(employee),
  });
};

export const updateEmployeeApprover = async (employee: EmployeeApprover): Promise<void> => {
  await authFetch(`${EMPLOYEE_API}/${employee.id}`, {
    method: "PUT",
    body: JSON.stringify(employee),
  });
};

export const deleteEmployeeApprover = async (id: number): Promise<void> => {
  await authFetch(`${EMPLOYEE_API}/${id}`, { method: "DELETE" });
};

export const searchEmployeeApprovers = async (keyword: string): Promise<EmployeeApprover[]> => {
  return authFetch(`${EMPLOYEE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
