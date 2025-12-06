import type { RootState } from "../../app/store";

export const selectApprovalHistorys = (state: RootState) => state.approvalHistory.approvalHistory;
export const selectApprovalHistoryLoading = (state: RootState) => state.approvalHistory.loading;
export const selectApprovalHistoryError = (state: RootState) => state.approvalHistory.error;

// Optional: selector para sa isang ApprovalHistory by id
export const selectApprovalHistoryById = (id: number) => (state: RootState) =>
  state.approvalHistory.approvalHistory.find((c) => c.id === id);
