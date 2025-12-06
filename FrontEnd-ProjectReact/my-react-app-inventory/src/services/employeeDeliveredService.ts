import { EMPLOYEE_API } from "../constants/api";
import type { EmployeeDelivered } from "../types/employeeDelivered";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Employee API =====
export const getEmployeeDelivered = async (): Promise<EmployeeDelivered[]> => {
  return authFetch(EMPLOYEE_API);
};

export const getEmployeeDeliveredPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<EmployeeDelivered[]>> => {
  return authFetch(`${EMPLOYEE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getEmployeeDeliveredById = async (id: number): Promise<EmployeeDelivered> => {
  return authFetch(`${EMPLOYEE_API}/${id}`);
};

export const createEmployeeDelivered = async (employee: EmployeeDelivered): Promise<EmployeeDelivered> => {
  return authFetch(EMPLOYEE_API, {
    method: "POST",
    body: JSON.stringify(employee),
  });
};

export const updateEmployeeDelivered = async (employee: EmployeeDelivered): Promise<void> => {
  await authFetch(`${EMPLOYEE_API}/${employee.id}`, {
    method: "PUT",
    body: JSON.stringify(employee),
  });
};

export const deleteEmployeeDelivered = async (id: number): Promise<void> => {
  await authFetch(`${EMPLOYEE_API}/${id}`, { method: "DELETE" });
};

export const searchEmployeeDelivered = async (keyword: string): Promise<EmployeeDelivered[]> => {
  return authFetch(`${EMPLOYEE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
