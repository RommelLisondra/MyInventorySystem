import { ITEM_BARCODE_API } from "../constants/api";
import type { ItemBarcode } from "../types/itemBarcode";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getItemBarcodes = async (): Promise<ItemBarcode[]> => {
  return authFetch(ITEM_BARCODE_API);
};

export const getItemBarcodePaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<ItemBarcode[]>> => {
  return authFetch(`${ITEM_BARCODE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getItemBarcodeById = async (id: number): Promise<ItemBarcode> => {
  return authFetch(`${ITEM_BARCODE_API}/${id}`);
};

export const createItemBarcode = async (ItemBarcode: ItemBarcode): Promise<ItemBarcode> => {
  return authFetch(ITEM_BARCODE_API, {
    method: "POST",
    body: JSON.stringify(ItemBarcode),
  });
};

export const updateItemBarcode = async (ItemBarcode: ItemBarcode): Promise<void> => {
  await authFetch(`${ITEM_BARCODE_API}/${ItemBarcode.id}`, {
    method: "PUT",
    body: JSON.stringify(ItemBarcode),
  });
};

export const deleteItemBarcode = async (id: number): Promise<void> => {
  await authFetch(`${ITEM_BARCODE_API}/${id}`, { method: "DELETE" });
};

export const searchItemBarcode = async (keyword: string): Promise<ItemBarcode[]> => {
  return authFetch(`${ITEM_BARCODE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
