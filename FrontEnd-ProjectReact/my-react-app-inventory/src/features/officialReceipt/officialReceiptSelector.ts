import type { RootState } from "../../app/store";

export const selectOfficialReceipt = (state: RootState) => state.officialReceipt.OfficialReceipt;
export const selectOfficialReceiptLoading = (state: RootState) => state.officialReceipt.loading;
export const selectOfficialReceiptError = (state: RootState) => state.officialReceipt.error;

// Optional: selector para sa isang customer by id
export const selectOfficialReceiptById = (id: number) => (state: RootState) =>
  state.officialReceipt.OfficialReceipt.find((c) => c.id === id);
