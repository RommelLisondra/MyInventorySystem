import { EMPLOYEE_API } from "../constants/api";
import type { EmployeeChecker } from "../types/employeeChecker";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Employee API =====
export const getEmployeeCheckers = async (): Promise<EmployeeChecker[]> => {
  return authFetch(EMPLOYEE_API);
};

export const getEmployeeCheckersPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<EmployeeChecker[]>> => {
  return authFetch(`${EMPLOYEE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getEmployeeCheckerById = async (id: number): Promise<EmployeeChecker> => {
  return authFetch(`${EMPLOYEE_API}/${id}`);
};

export const createEmployeeChecker = async (employee: EmployeeChecker): Promise<EmployeeChecker> => {
  return authFetch(EMPLOYEE_API, {
    method: "POST",
    body: JSON.stringify(employee),
  });
};

export const updateEmployeeChecker = async (employee: EmployeeChecker): Promise<void> => {
  await authFetch(`${EMPLOYEE_API}/${employee.id}`, {
    method: "PUT",
    body: JSON.stringify(employee),
  });
};

export const deleteEmployeeChecker = async (id: number): Promise<void> => {
  await authFetch(`${EMPLOYEE_API}/${id}`, { method: "DELETE" });
};

export const searchEmployeeCheckers = async (keyword: string): Promise<EmployeeChecker[]> => {
  return authFetch(`${EMPLOYEE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
