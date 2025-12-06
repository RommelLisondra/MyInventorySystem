import { ITEM_UNIT_MEASURE_API } from "../constants/api";
import type { ItemUnitMeasure } from "../types/itemUnitMeasure";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getItemUnitMeasures = async (): Promise<ItemUnitMeasure[]> => {
  return authFetch(ITEM_UNIT_MEASURE_API);
};

export const getItemUnitMeasuresPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<ItemUnitMeasure[]>> => {
  return authFetch(`${ITEM_UNIT_MEASURE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getItemUnitMeasureById = async (id: number): Promise<ItemUnitMeasure> => {
  return authFetch(`${ITEM_UNIT_MEASURE_API}/${id}`);
};

export const createItemUnitMeasure = async (itemUnitMeasure: ItemUnitMeasure): Promise<ItemUnitMeasure> => {
  return authFetch(ITEM_UNIT_MEASURE_API, {
    method: "POST",
    body: JSON.stringify(itemUnitMeasure),
  });
};

export const updateItemUnitMeasure = async (itemUnitMeasure: ItemUnitMeasure): Promise<void> => {
  await authFetch(`${ITEM_UNIT_MEASURE_API}/${itemUnitMeasure.id}`, {
    method: "PUT",
    body: JSON.stringify(itemUnitMeasure),
  });
};

export const deleteItemUnitMeasure = async (id: number): Promise<void> => {
  await authFetch(`${ITEM_UNIT_MEASURE_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchItemUnitMeasures = async (keyword: string): Promise<ItemUnitMeasure[]> => {
  return authFetch(`${ITEM_UNIT_MEASURE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};