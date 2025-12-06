import { EMPLOYEE_API } from "../constants/api";
import type { Employee } from "../types/employee";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Employee API =====
export const getEmployees = async (): Promise<Employee[]> => {
  return authFetch(EMPLOYEE_API);
};

export const getEmployeesPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<Employee[]>> => {
  return authFetch(`${EMPLOYEE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getEmployeeById = async (id: number): Promise<Employee> => {
  return authFetch(`${EMPLOYEE_API}/${id}`);
};

export const createEmployee = async (employee: Employee): Promise<Employee> => {
  return authFetch(EMPLOYEE_API, {
    method: "POST",
    body: JSON.stringify(employee),
  });
};

export const updateEmployee = async (employee: Employee): Promise<void> => {
  await authFetch(`${EMPLOYEE_API}/${employee.id}`, {
    method: "PUT",
    body: JSON.stringify(employee),
  });
};

export const deleteEmployee = async (id: number): Promise<void> => {
  await authFetch(`${EMPLOYEE_API}/${id}`, { method: "DELETE" });
};

export const searchEmployees = async (keyword: string): Promise<Employee[]> => {
  return authFetch(`${EMPLOYEE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
