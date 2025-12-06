export interface AuditTrail {
    id: number;
    tableName: string;
    primaryKey: string;
    action?: string | null;
    changedBy?: string | null;
    changedDate?: Date | null;
    oldValue?: string | null;
    newValue?: string | null;
}