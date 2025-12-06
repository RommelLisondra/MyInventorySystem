import type { RootState } from "../../app/store";

export const selectSystemSettings = (state: RootState) => state.systemSettings.systemSettings;
export const selectSystemSettingsLoading = (state: RootState) => state.systemSettings.loading;
export const selectSystemSettingsError = (state: RootState) => state.systemSettings.error;

// Optional: selector para sa isang customer by id
export const selectSystemSettingsById = (id: number) => (state: RootState) =>
  state.systemSettings.systemSettings.find((c) => c.id === id);
