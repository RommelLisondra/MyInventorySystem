import { APPROVAL_FLOW_API } from "../constants/api";
import type { ApprovalFlow } from "../types/approvalFlow";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getApprovalFlows = async (): Promise<ApprovalFlow[]> => {
  return authFetch(APPROVAL_FLOW_API);
};

export const getApprovalFlowsPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<ApprovalFlow[]>> => {
  return authFetch(`${APPROVAL_FLOW_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getApprovalFlowById = async (id: number): Promise<ApprovalFlow> => {
  return authFetch(`${APPROVAL_FLOW_API}/${id}`);
};

export const createApprovalFlow = async (approvalFlow: ApprovalFlow): Promise<ApprovalFlow> => {
  return authFetch(APPROVAL_FLOW_API, {
    method: "POST",
    body: JSON.stringify(approvalFlow),
  });
};

export const updateApprovalFlow = async (approvalFlow: ApprovalFlow): Promise<void> => {
  await authFetch(`${APPROVAL_FLOW_API}/${approvalFlow.id}`, {
    method: "PUT",
    body: JSON.stringify(approvalFlow),
  });
};

export const deleteApprovalFlow = async (id: number): Promise<void> => {
  await authFetch(`${APPROVAL_FLOW_API}/${id}`, { method: "DELETE" });
};

export const searchApprovalFlows = async (keyword: string): Promise<ApprovalFlow[]> => {
  return authFetch(`${APPROVAL_FLOW_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
