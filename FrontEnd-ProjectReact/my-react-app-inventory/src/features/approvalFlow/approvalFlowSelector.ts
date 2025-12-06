import type { RootState } from "../../app/store";

export const selectApprovalFlows = (state: RootState) => state.approvalFlow.approvalFlows;
export const selectApprovalFlowLoading = (state: RootState) => state.approvalFlow.loading;
export const selectApprovalFlowError = (state: RootState) => state.approvalFlow.error;

// Optional: selector para sa isang customer by id
export const selectApprovalFlowById = (id: number) => (state: RootState) =>
  state.approvalFlow.approvalFlows.find((c) => c.id === id);
