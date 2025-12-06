import type { RootState } from "../../app/store";

export const selectReceivingReport = (state: RootState) => state.receivingReport.receivingReport;
export const selectReceivingReportLoading = (state: RootState) => state.receivingReport.loading;
export const selectReceivingReportError = (state: RootState) => state.receivingReport.error;

// Optional: selector para sa isang customer by id
export const selectReceivingReportById = (id: number) => (state: RootState) =>
  state.receivingReport.receivingReport.find((c) => c.id === id);
