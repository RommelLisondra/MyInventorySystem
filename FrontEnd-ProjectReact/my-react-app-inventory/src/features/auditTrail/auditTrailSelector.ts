import type { RootState } from "../../app/store";

export const selectAuditTrails = (state: RootState) => state.auditTrail.auditTrail;
export const selectAuditTrailLoading = (state: RootState) => state.auditTrail.loading;
export const selectAuditTrailError = (state: RootState) => state.auditTrail.error;

// Optional: selector para sa isang AuditTrail by id
export const selectAuditTrailById = (id: number) => (state: RootState) =>
  state.auditTrail.auditTrail.find((c) => c.id === id);
