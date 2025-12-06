import { AUDIT_TRAIL_API } from "../constants/api";
import type { AuditTrail } from "../types/auditTrail";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Audit Trail API =====
export const getAuditTrails = async (): Promise<AuditTrail[]> => {
  return authFetch(AUDIT_TRAIL_API);
};

export const getAuditTrailsPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<AuditTrail[]>> => {
  return authFetch(`${AUDIT_TRAIL_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getAuditTrailById = async (id: number): Promise<AuditTrail> => {
  return authFetch(`${AUDIT_TRAIL_API}/${id}`);
};

export const createAuditTrail = async (auditTrail: AuditTrail): Promise<AuditTrail> => {
  return authFetch(AUDIT_TRAIL_API, {
    method: "POST",
    body: JSON.stringify(auditTrail),
  });
};

export const updateAuditTrail = async (auditTrail: AuditTrail): Promise<void> => {
  await authFetch(`${AUDIT_TRAIL_API}/${auditTrail.id}`, {
    method: "PUT",
    body: JSON.stringify(auditTrail),
  });
};

export const deleteAuditTrail = async (id: number): Promise<void> => {
  await authFetch(`${AUDIT_TRAIL_API}/${id}`, { method: "DELETE" });
};

export const searchAuditTrails = async (keyword: string): Promise<AuditTrail[]> => {
  return authFetch(`${AUDIT_TRAIL_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
