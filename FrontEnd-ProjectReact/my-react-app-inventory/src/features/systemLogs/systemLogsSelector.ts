import type { RootState } from "../../app/store";

export const selectSystemLog = (state: RootState) => state.systemLogs.systemLogs;
export const selectSystemLogLoading = (state: RootState) => state.systemLogs.loading;
export const selectSystemLogError = (state: RootState) => state.systemLogs.error;

// Optional: selector para sa isang customer by id
export const selectSystemLogById = (id: number) => (state: RootState) =>
  state.systemLogs.systemLogs.find((c) => c.id === id);
