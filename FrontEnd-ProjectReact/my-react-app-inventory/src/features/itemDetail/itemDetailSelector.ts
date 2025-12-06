import type { RootState } from "../../app/store";

export const selectItemDetails = (state: RootState) => state.itemDetail.itemDetails;
export const selectItemDetailsLoading = (state: RootState) => state.itemDetail.loading;
export const selectItemDetailsError = (state: RootState) => state.itemDetail.error;

// Optional: selector para sa isang customer by id
export const selectItemDetailsById = (id: number) => (state: RootState) =>
  state.itemDetail.itemDetails.find((c) => c.id === id);
