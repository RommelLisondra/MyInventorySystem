import { OFFICIAL_RECEIPT_API } from "../constants/api";
import type { OfficialReceiptMaster } from "../types/officialReceiptMaster";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getOfficialReceipts = async (): Promise<OfficialReceiptMaster[]> => {
  return authFetch(OFFICIAL_RECEIPT_API);
};

export const getOfficialReceiptsPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<OfficialReceiptMaster[]>> => {
  return authFetch(`${OFFICIAL_RECEIPT_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getOfficialReceiptById = async (id: number): Promise<OfficialReceiptMaster> => {
  return authFetch(`${OFFICIAL_RECEIPT_API}/${id}`);
};

export const createOfficialReceipt = async (officialReceiptMaster: OfficialReceiptMaster): Promise<OfficialReceiptMaster> => {
 return authFetch(OFFICIAL_RECEIPT_API, {
    method: "POST",
    body: JSON.stringify(officialReceiptMaster),
  });
};

export const updateOfficialReceipt = async (officialReceiptMaster: OfficialReceiptMaster): Promise<void> => {
  await authFetch(`${OFFICIAL_RECEIPT_API}/${officialReceiptMaster.id}`, {
    method: "PUT",
    body: JSON.stringify(officialReceiptMaster),
  });
};

export const deleteOfficialReceipt = async (id: number): Promise<void> => {
  await authFetch(`${OFFICIAL_RECEIPT_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchOfficialReceipts = async (keyword: string): Promise<OfficialReceiptMaster[]> => {
  return authFetch(`${OFFICIAL_RECEIPT_API}/search?keyword=${encodeURIComponent(keyword)}`);
};