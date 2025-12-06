import { PURCHASE_ORDER_API } from "../constants/api";
import type { PurchaseOrderMaster } from "../types/purchaseOrderMaster";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getpurchaseOrders = async (): Promise<PurchaseOrderMaster[]> => {
  return authFetch(PURCHASE_ORDER_API);
};

export const getpurchaseOrdersPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<PurchaseOrderMaster[]>> => {
  return authFetch(`${PURCHASE_ORDER_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getpurchaseOrderById = async (id: number): Promise<PurchaseOrderMaster> => {
  return authFetch(`${PURCHASE_ORDER_API}/${id}`);
};

export const createpurchaseOrder = async (purchaseOrderMaster: PurchaseOrderMaster): Promise<PurchaseOrderMaster> => {
  return authFetch(PURCHASE_ORDER_API, {
    method: "POST",
    body: JSON.stringify(purchaseOrderMaster),
  });
};

export const updatepurchaseOrder = async (purchaseOrderMaster: PurchaseOrderMaster): Promise<void> => {
  await authFetch(`${PURCHASE_ORDER_API}/${purchaseOrderMaster.id}`, {
    method: "PUT",
    body: JSON.stringify(purchaseOrderMaster),
  });
};

export const deletepurchaseOrder = async (id: number): Promise<void> => {
  await authFetch(`${PURCHASE_ORDER_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchpurchaseOrders = async (keyword: string): Promise<PurchaseOrderMaster[]> => {
  return authFetch(`${PURCHASE_ORDER_API}/search?keyword=${encodeURIComponent(keyword)}`);
};