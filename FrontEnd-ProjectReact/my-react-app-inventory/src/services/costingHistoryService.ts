import { COSTING_HISTORY_API } from "../constants/api";
import type { CostingHistory } from "../types/costingHistory";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getCostingHistory = async (): Promise<CostingHistory[]> => {
  return authFetch(COSTING_HISTORY_API);
};

export const getCostingHistorysPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<CostingHistory[]>> => {
  return authFetch(`${COSTING_HISTORY_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getCostingHistoryById = async (id: number): Promise<CostingHistory> => {
  return authFetch(`${COSTING_HISTORY_API}/${id}`);
};

export const createCostingHistory = async (CostingHistory: CostingHistory): Promise<CostingHistory> => {
  return authFetch(COSTING_HISTORY_API, {
    method: "POST",
    body: JSON.stringify(CostingHistory),
  });
};

export const updateCostingHistory = async (CostingHistory: CostingHistory): Promise<void> => {
  await authFetch(`${COSTING_HISTORY_API}/${CostingHistory.id}`, {
    method: "PUT",
    body: JSON.stringify(CostingHistory),
  });
};

export const deleteCostingHistory = async (id: number): Promise<void> => {
  await authFetch(`${COSTING_HISTORY_API}/${id}`, { method: "DELETE" });
};

export const searchCostingHistory = async (keyword: string): Promise<CostingHistory[]> => {
  return authFetch(`${COSTING_HISTORY_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
