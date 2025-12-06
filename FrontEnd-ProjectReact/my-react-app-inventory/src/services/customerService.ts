import { CUSTOMER_API } from "../constants/api";
import type { Customer } from "../types/customer";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Customer API =====
export const getCustomers = async (): Promise<Customer[]> => {
  return authFetch(CUSTOMER_API);
};

export const getCustomersPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<Customer[]>> => {
  return authFetch(`${CUSTOMER_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getCustomerById = async (id: number): Promise<Customer> => {
  return authFetch(`${CUSTOMER_API}/${id}`);
};

export const createCustomer = async (customer: Customer): Promise<Customer> => {
  return authFetch(CUSTOMER_API, {
    method: "POST",
    body: JSON.stringify(customer),
  });
};

export const updateCustomer = async (customer: Customer): Promise<void> => {
  await authFetch(`${CUSTOMER_API}/${customer.id}`, {
    method: "PUT",
    body: JSON.stringify(customer),
  });
};

export const deleteCustomer = async (id: number): Promise<void> => {
  await authFetch(`${CUSTOMER_API}/${id}`, { method: "DELETE" });
};

export const searchCustomers = async (keyword: string): Promise<Customer[]> => {
  return authFetch(`${CUSTOMER_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
