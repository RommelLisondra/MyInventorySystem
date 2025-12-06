import { EMPLOYEE_API } from "../constants/api";
import type { EmployeeSalesRef } from "../types/employeeSalesRef";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Employee API =====
export const getEmployeeSalesRef = async (): Promise<EmployeeSalesRef[]> => {
  return authFetch(EMPLOYEE_API);
};

export const getEmployeeSalesRefPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<EmployeeSalesRef[]>> => {
  return authFetch(`${EMPLOYEE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getEmployeeSalesRefById = async (id: number): Promise<EmployeeSalesRef> => {
  return authFetch(`${EMPLOYEE_API}/${id}`);
};

export const createEmployeeSalesRef = async (employee: EmployeeSalesRef): Promise<EmployeeSalesRef> => {
  return authFetch(EMPLOYEE_API, {
    method: "POST",
    body: JSON.stringify(employee),
  });
};

export const updateEmployeeSalesRef = async (employee: EmployeeSalesRef): Promise<void> => {
  await authFetch(`${EMPLOYEE_API}/${employee.id}`, {
    method: "PUT",
    body: JSON.stringify(employee),
  });
};

export const deleteEmployeeSalesRef = async (id: number): Promise<void> => {
  await authFetch(`${EMPLOYEE_API}/${id}`, { method: "DELETE" });
};

export const searchEmployeeSalesRef = async (keyword: string): Promise<EmployeeSalesRef[]> => {
  return authFetch(`${EMPLOYEE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
