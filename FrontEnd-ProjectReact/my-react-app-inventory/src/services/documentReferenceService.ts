import { DOCUMENT_REFERENCE_API } from "../constants/api";
import type { DocumentReference } from "../types/documentReference";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Document Reference API =====
export const getDocumentReferences = async (): Promise<DocumentReference[]> => {
  return authFetch(DOCUMENT_REFERENCE_API);
};

export const getDocumentReferencesPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<DocumentReference[]>> => {
  return authFetch(`${DOCUMENT_REFERENCE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getDocumentReferenceById = async (id: number): Promise<DocumentReference> => {
  return authFetch(`${DOCUMENT_REFERENCE_API}/${id}`);
};

export const createDocumentReference = async (documentReference: DocumentReference): Promise<DocumentReference> => {
  return authFetch(DOCUMENT_REFERENCE_API, {
    method: "POST",
    body: JSON.stringify(documentReference),
  });
};

export const updateDocumentReference = async (documentReference: DocumentReference): Promise<void> => {
  await authFetch(`${DOCUMENT_REFERENCE_API}/${documentReference.id}`, {
    method: "PUT",
    body: JSON.stringify(documentReference),
  });
};

export const deleteDocumentReference = async (id: number): Promise<void> => {
  await authFetch(`${DOCUMENT_REFERENCE_API}/${id}`, { method: "DELETE" });
};

export const searchDocumentReferences = async (keyword: string): Promise<DocumentReference[]> => {
  return authFetch(`${DOCUMENT_REFERENCE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
