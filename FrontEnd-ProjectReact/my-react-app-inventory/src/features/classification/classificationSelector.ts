import type { RootState } from "../../app/store";

export const selectclassifications = (state: RootState) => state.classification.classification;
export const selectclassificationLoading = (state: RootState) => state.classification.loading;
export const selectclassificationError = (state: RootState) => state.classification.error;

// Optional: selector para sa isang customer by id
export const selectclassificationById = (id: number) => (state: RootState) =>
  state.classification.classification.find((c) => c.id === id);
