import { DELIVERY_RECEIPT_API } from "../constants/api";
import type { DeliveryReceiptMaster } from "../types/deliveryReceiptMaster";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Delivery Receipt API =====
export const getDeliveryReceipts = async (): Promise<DeliveryReceiptMaster[]> => {
  return authFetch(DELIVERY_RECEIPT_API);
};

export const getDeliveryReceiptsPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<DeliveryReceiptMaster[]>> => {
  return authFetch(`${DELIVERY_RECEIPT_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getDeliveryReceiptById = async (id: number): Promise<DeliveryReceiptMaster> => {
  return authFetch(`${DELIVERY_RECEIPT_API}/${id}`);
};

export const createDeliveryReceipt = async (deliveryReceipt: DeliveryReceiptMaster): Promise<DeliveryReceiptMaster> => {
  return authFetch(DELIVERY_RECEIPT_API, {
    method: "POST",
    body: JSON.stringify(deliveryReceipt),
  });
};

export const updateDeliveryReceipt = async (deliveryReceipt: DeliveryReceiptMaster): Promise<void> => {
  await authFetch(`${DELIVERY_RECEIPT_API}/${deliveryReceipt.id}`, {
    method: "PUT",
    body: JSON.stringify(deliveryReceipt),
  });
};

export const deleteDeliveryReceipt = async (id: number): Promise<void> => {
  await authFetch(`${DELIVERY_RECEIPT_API}/${id}`, { method: "DELETE" });
};

export const searchDeliveryReceipts = async (keyword: string): Promise<DeliveryReceiptMaster[]> => {
  return authFetch(`${DELIVERY_RECEIPT_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
