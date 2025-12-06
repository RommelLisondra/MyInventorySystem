import { DOCUMENT_SERIES_API } from "../constants/api";
import type { DocumentSeries } from "../types/documentSeries";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Document Series API =====
export const getDocumentSeriess = async (): Promise<DocumentSeries[]> => {
  return authFetch(DOCUMENT_SERIES_API);
};

export const getDocumentSeriessPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<DocumentSeries[]>> => {
  return authFetch(`${DOCUMENT_SERIES_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getDocumentSeriesById = async (id: number): Promise<DocumentSeries> => {
  return authFetch(`${DOCUMENT_SERIES_API}/${id}`);
};

export const createDocumentSeries = async (documentSeries: DocumentSeries): Promise<DocumentSeries> => {
  return authFetch(DOCUMENT_SERIES_API, {
    method: "POST",
    body: JSON.stringify(documentSeries),
  });
};

export const updateDocumentSeries = async (documentSeries: DocumentSeries): Promise<void> => {
  await authFetch(`${DOCUMENT_SERIES_API}/${documentSeries.id}`, {
    method: "PUT",
    body: JSON.stringify(documentSeries),
  });
};

export const deleteDocumentSeries = async (id: number): Promise<void> => {
  await authFetch(`${DOCUMENT_SERIES_API}/${id}`, { method: "DELETE" });
};

export const searchDocumentSeriess = async (keyword: string): Promise<DocumentSeries[]> => {
  return authFetch(`${DOCUMENT_SERIES_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
