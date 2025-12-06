import { STOCK_TRANSFER_API } from "../constants/api";
import type { StockTransfer } from "../types/stockTransfer";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getStockTransfers = async (): Promise<StockTransfer[]> => {
  return authFetch(STOCK_TRANSFER_API);
};

export const getStockTransfersPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<StockTransfer[]>> => {
  return authFetch(`${STOCK_TRANSFER_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getStockTransferById = async (id: number): Promise<StockTransfer> => {
  return authFetch(`${STOCK_TRANSFER_API}/${id}`);
};

export const createStockTransfer = async (stockTransfer: StockTransfer): Promise<StockTransfer> => {
  return authFetch(STOCK_TRANSFER_API, {
    method: "POST",
    body: JSON.stringify(stockTransfer),
  });
};

export const updateStockTransfer = async (stockTransfer: StockTransfer): Promise<void> => {
   await authFetch(`${STOCK_TRANSFER_API}/${stockTransfer.id}`, {
    method: "PUT",
    body: JSON.stringify(stockTransfer),
  });
};

export const deleteStockTransfer = async (id: number): Promise<void> => {
  await authFetch(`${STOCK_TRANSFER_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchStockTransfers = async (keyword: string): Promise<StockTransfer[]> => {
  return authFetch(`${STOCK_TRANSFER_API}/search?keyword=${encodeURIComponent(keyword)}`);
};