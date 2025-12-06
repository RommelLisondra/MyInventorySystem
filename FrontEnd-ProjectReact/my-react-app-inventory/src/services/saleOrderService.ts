import { SALES_ORDER_API } from "../constants/api";
import type { SalesOrderMaster } from "../types/salesOrderMaster";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getSalesOrders = async (): Promise<SalesOrderMaster[]> => {
  return authFetch(SALES_ORDER_API);
};

export const getSalesOrdersPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<SalesOrderMaster[]>> => {
  return authFetch(`${SALES_ORDER_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getSalesOrderById = async (id: number): Promise<SalesOrderMaster> => {
  return authFetch(`${SALES_ORDER_API}/${id}`);
};

export const createSalesOrder = async (salesOrderMaster: SalesOrderMaster): Promise<SalesOrderMaster> => {
  return authFetch(SALES_ORDER_API, {
    method: "POST",
    body: JSON.stringify(salesOrderMaster),
  });
};

export const updateSalesOrder = async (salesOrderMaster: SalesOrderMaster): Promise<void> => {
   await authFetch(`${SALES_ORDER_API}/${salesOrderMaster.id}`, {
    method: "PUT",
    body: JSON.stringify(salesOrderMaster),
  });
};

export const deleteSalesOrder = async (id: number): Promise<void> => {
  await authFetch(`${SALES_ORDER_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchSalesOrders = async (keyword: string): Promise<SalesOrderMaster[]> => {
  return authFetch(`${SALES_ORDER_API}/search?keyword=${encodeURIComponent(keyword)}`);
};