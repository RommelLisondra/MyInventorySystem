import { SUPPLIER_API } from "../constants/api";
import type { Supplier } from "../types/supplier";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getSuppliers = async (): Promise<Supplier[]> => {
  return authFetch(SUPPLIER_API);
};

export const getSuppliersPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<Supplier[]>> => {
  return authFetch(`${SUPPLIER_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getSupplierById = async (id: number): Promise<Supplier> => {
  return authFetch(`${SUPPLIER_API}/${id}`);
};

export const createSupplier = async (supplier: Supplier): Promise<Supplier> => {
  return authFetch(SUPPLIER_API, {
    method: "POST",
    body: JSON.stringify(supplier),
  });
};

export const updateSupplier = async (supplier: Supplier): Promise<void> => {
  await authFetch(`${SUPPLIER_API}/${supplier.id}`, {
    method: "PUT",
    body: JSON.stringify(supplier),
  });
};

export const deleteSupplier = async (id: number): Promise<void> => {
  await authFetch(`${SUPPLIER_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchSuppliers = async (keyword: string): Promise<Supplier[]> => {
  return authFetch(`${SUPPLIER_API}/search?keyword=${encodeURIComponent(keyword)}`);
};