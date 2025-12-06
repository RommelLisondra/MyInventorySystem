import type { RootState } from "../../app/store";

export const selectItemUnitMeasures = (state: RootState) => state.itemUnitMeasure.itemUnitMeasures;
export const selectItemUnitMeasuresLoading = (state: RootState) => state.itemUnitMeasure.loading;
export const selectItemUnitMeasureError = (state: RootState) => state.itemUnitMeasure.error;

// Optional: selector para sa isang customer by id
export const selectItemUnitMeasureById = (id: number) => (state: RootState) =>
  state.itemUnitMeasure.itemUnitMeasures.find((c) => c.id === id);
