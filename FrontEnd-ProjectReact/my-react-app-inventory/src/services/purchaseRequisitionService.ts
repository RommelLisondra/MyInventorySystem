import { PURCHASE_REQUISITION_API } from "../constants/api";
import type { PurchaseRequisitionMaster } from "../types/purchaseRequisitionMaster";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getPurchaseRequisitions = async (): Promise<PurchaseRequisitionMaster[]> => {
  return authFetch(PURCHASE_REQUISITION_API);
};

export const getPurchaseRequisitionsPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<PurchaseRequisitionMaster[]>> => {
  return authFetch(`${PURCHASE_REQUISITION_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getPurchaseRequisitionById = async (id: number): Promise<PurchaseRequisitionMaster> => {
  return authFetch(`${PURCHASE_REQUISITION_API}/${id}`);
};

export const createPurchaseRequisition = async (purchaseRequisitionMaster: PurchaseRequisitionMaster): Promise<PurchaseRequisitionMaster> => {
  return authFetch(PURCHASE_REQUISITION_API, {
    method: "POST",
    body: JSON.stringify(purchaseRequisitionMaster),
  });
};

export const updatePurchaseRequisition = async (purchaseRequisitionMaster: PurchaseRequisitionMaster): Promise<void> => {
  await authFetch(`${PURCHASE_REQUISITION_API}/${purchaseRequisitionMaster.id}`, {
    method: "PUT",
    body: JSON.stringify(purchaseRequisitionMaster),
  });
};

export const deletePurchaseRequisition = async (id: number): Promise<void> => {
  await authFetch(`${PURCHASE_REQUISITION_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchPurchaseRequisitions = async (keyword: string): Promise<PurchaseRequisitionMaster[]> => {
  return authFetch(`${PURCHASE_REQUISITION_API}/search?keyword=${encodeURIComponent(keyword)}`);
};