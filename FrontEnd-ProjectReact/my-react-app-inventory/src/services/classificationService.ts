import { CLASSIFICATION_API } from "../constants/api";
import type { Classification } from "../types/classification";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getClassifications = async (): Promise<Classification[]> => {
  return authFetch(CLASSIFICATION_API);
};

export const getClassificationsPaged = async (pageNumber: number = 1,pageSize: number = 20): Promise<PagedResponse<Classification[]>> => {
  return authFetch(`${CLASSIFICATION_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getClassificationById = async (id: number): Promise<Classification> => {
  return authFetch(`${CLASSIFICATION_API}/${id}`);
};

export const createClassification = async (Classification: Classification): Promise<Classification> => {
  return authFetch(CLASSIFICATION_API, {
    method: "POST",
    body: JSON.stringify(Classification),
  });
};

export const updateClassification = async (Classification: Classification): Promise<void> => {
  await authFetch(`${CLASSIFICATION_API}/${Classification.id}`, {
    method: "PUT",
    body: JSON.stringify(Classification),
  });
};

export const deleteClassification = async (id: number): Promise<void> => {
  await authFetch(`${CLASSIFICATION_API}/${id}`, { method: "DELETE" });
};

export const searchClassifications = async (keyword: string): Promise<Classification[]> => {
  return authFetch(`${CLASSIFICATION_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
