import { SALES_INVOICE_API } from "../constants/api";
import type { SalesInvoiceMaster } from "../types/salesInvoiceMaster";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getSalesInvoices = async (): Promise<SalesInvoiceMaster[]> => {
  return authFetch(SALES_INVOICE_API);
};

export const getSalesInvoicesPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<SalesInvoiceMaster[]>> => {
  return authFetch(`${SALES_INVOICE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getSalesInvoiceById = async (id: number): Promise<SalesInvoiceMaster> => {
  return authFetch(`${SALES_INVOICE_API}/${id}`);
};

export const createSalesInvoice = async (salesInvoiceMaster: SalesInvoiceMaster): Promise<SalesInvoiceMaster> => {
 return authFetch(SALES_INVOICE_API, {
    method: "POST",
    body: JSON.stringify(salesInvoiceMaster),
  });
};

export const updateSalesInvoice = async (salesInvoiceMaster: SalesInvoiceMaster): Promise<void> => {
  await authFetch(`${SALES_INVOICE_API}/${salesInvoiceMaster.id}`, {
    method: "PUT",
    body: JSON.stringify(salesInvoiceMaster),
  });
};

export const deleteSalesInvoice = async (id: number): Promise<void> => {
 await authFetch(`${SALES_INVOICE_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchSalesInvoices = async (keyword: string): Promise<SalesInvoiceMaster[]> => {
  return authFetch(`${SALES_INVOICE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};