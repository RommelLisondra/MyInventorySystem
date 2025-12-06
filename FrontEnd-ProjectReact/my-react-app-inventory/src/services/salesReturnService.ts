import { SALES_RETURN_API } from "../constants/api";
import type { SalesReturnMaster } from "../types/salesReturnMaster";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getSalesReturns = async (): Promise<SalesReturnMaster[]> => {
  return authFetch(SALES_RETURN_API);
};

export const getSalesReturnsPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<SalesReturnMaster[]>> => {
  return authFetch(`${SALES_RETURN_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getSalesReturnById = async (id: number): Promise<SalesReturnMaster> => {
  return authFetch(`${SALES_RETURN_API}/${id}`);
};

export const createSalesReturn = async (salesReturnMaster: SalesReturnMaster): Promise<SalesReturnMaster> => {
  return authFetch(SALES_RETURN_API, {
    method: "POST",
    body: JSON.stringify(salesReturnMaster),
  });
};

export const updateSalesReturn = async (salesReturnMaster: SalesReturnMaster): Promise<void> => {
  await authFetch(`${SALES_RETURN_API}/${salesReturnMaster.id}`, {
    method: "PUT",
    body: JSON.stringify(salesReturnMaster),
  });
};

export const deleteSalesReturn = async (id: number): Promise<void> => {
 await authFetch(`${SALES_RETURN_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchSalesReturns = async (keyword: string): Promise<SalesReturnMaster[]> => {
  return authFetch(`${SALES_RETURN_API}/search?keyword=${encodeURIComponent(keyword)}`);
};