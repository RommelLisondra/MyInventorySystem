import { PURCHASE_RETURN_API } from "../constants/api";
import type { PurchaseReturnMaster } from "../types/purchaseReturnMaster";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getPurchaseReturns = async (): Promise<PurchaseReturnMaster[]> => {
  return authFetch(PURCHASE_RETURN_API);
};

export const getPurchaseReturnsPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<PurchaseReturnMaster[]>> => {
  return authFetch(`${PURCHASE_RETURN_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getPurchaseReturnById = async (id: number): Promise<PurchaseReturnMaster> => {
  return authFetch(`${PURCHASE_RETURN_API}/${id}`);
};

export const createPurchaseReturn = async (purchaseReturnMaster: PurchaseReturnMaster): Promise<PurchaseReturnMaster> => {
  return authFetch(PURCHASE_RETURN_API, {
    method: "POST",
    body: JSON.stringify(purchaseReturnMaster),
  });
};

export const updatePurchaseReturn = async (purchaseReturnMaster: PurchaseReturnMaster): Promise<void> => {
  await authFetch(`${PURCHASE_RETURN_API}/${purchaseReturnMaster.id}`, {
    method: "PUT",
    body: JSON.stringify(purchaseReturnMaster),
  });
};

export const deletePurchaseReturn = async (id: number): Promise<void> => {
  await authFetch(`${PURCHASE_RETURN_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchPurchaseReturns = async (keyword: string): Promise<PurchaseReturnMaster[]> => {
  return authFetch(`${PURCHASE_RETURN_API}/search?keyword=${encodeURIComponent(keyword)}`);
};