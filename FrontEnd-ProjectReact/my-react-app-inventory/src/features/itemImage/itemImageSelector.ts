import type { RootState } from "../../app/store";

export const selectItemImages = (state: RootState) => state.itemImage.itemImages;
export const selectItemImagesLoading = (state: RootState) => state.itemImage.loading;
export const selectItemImageError = (state: RootState) => state.itemImage.error;

// Optional: selector para sa isang customer by id
export const selectItemImageById = (id: number) => (state: RootState) =>
  state.itemImage.itemImages.find((c) => c.id === id);
