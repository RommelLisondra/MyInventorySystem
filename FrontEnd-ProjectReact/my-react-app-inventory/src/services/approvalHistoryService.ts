import { APPROVAL_HISTORY_API } from "../constants/api";
import type { ApprovalHistory } from "../types/approvalHistory";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval History API =====
export const getApprovalHistorys = async (): Promise<ApprovalHistory[]> => {
  return authFetch(APPROVAL_HISTORY_API);
};

export const getApprovalHistorysPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<ApprovalHistory[]>> => {
  return authFetch(`${APPROVAL_HISTORY_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getApprovalHistoryById = async (id: number): Promise<ApprovalHistory> => {
  return authFetch(`${APPROVAL_HISTORY_API}/${id}`);
};

export const createApprovalHistory = async (approvalHistory: ApprovalHistory): Promise<ApprovalHistory> => {
  return authFetch(APPROVAL_HISTORY_API, {
    method: "POST",
    body: JSON.stringify(approvalHistory),
  });
};

export const updateApprovalHistory = async (approvalHistory: ApprovalHistory): Promise<void> => {
  await authFetch(`${APPROVAL_HISTORY_API}/${approvalHistory.id}`, {
    method: "PUT",
    body: JSON.stringify(approvalHistory),
  });
};

export const deleteApprovalHistory = async (id: number): Promise<void> => {
  await authFetch(`${APPROVAL_HISTORY_API}/${id}`, { method: "DELETE" });
};

export const searchApprovalHistorys = async (keyword: string): Promise<ApprovalHistory[]> => {
  return authFetch(`${APPROVAL_HISTORY_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
