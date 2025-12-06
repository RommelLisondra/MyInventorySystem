export interface InventoryAdjustmentDetail {
    id: number;
    adjustmentId: number;
    itemDetailNo: string;
    quantity: number;
    movementType?: string | null;
    recStatus?: string | null;
}