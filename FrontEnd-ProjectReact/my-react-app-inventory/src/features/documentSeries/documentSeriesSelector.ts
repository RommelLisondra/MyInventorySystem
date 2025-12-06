import type { RootState } from "../../app/store";

export const selectDocumentSeries = (state: RootState) => state.documentSeries.documentSeries;
export const selectDocumentSeriesLoading = (state: RootState) => state.documentSeries.loading;
export const selectDocumentSeriesError = (state: RootState) => state.documentSeries.error;

// Optional: selector para sa isang DocumentSeries by id
export const selectDocumentSeriesById = (id: number) => (state: RootState) =>
  state.documentSeries.documentSeries.find((c) => c.id === id);
