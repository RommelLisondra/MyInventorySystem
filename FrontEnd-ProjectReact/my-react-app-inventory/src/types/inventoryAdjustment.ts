import type { InventoryAdjustmentDetail } from "./inventoryAdjustmentDetail";

export interface InventoryAdjustment {
    id: number;
    adjustmentNo: string;
    adjustmentDate: string;
    companyId: number;
    warehouseId?: number | null;
    remarks?: string | null;
    createdBy?: string | null;
    createdDate?: Date | null;
    recStatus?: string | null;

    inventoryAdjustmentDetail : InventoryAdjustmentDetail | null;
}