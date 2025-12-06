import type { RootState } from "../../app/store";

export const selectCompany = (state: RootState) => state.company.company;
export const selectCompanyLoading = (state: RootState) => state.company.loading;
export const selectCompanyError = (state: RootState) => state.company.error;

// Optional: selector para sa isang customer by id
export const selectCompanyById = (id: number) => (state: RootState) =>
  state.company.company.find((c) => c.id === id);
