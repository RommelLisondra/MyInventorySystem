export interface InventoryBalance {
    id: number;
    itemDetailNo: string;
    warehouseId: number;
    quantityOnHand: number;
    lastUpdated?: Date | null;
    recStatus?: string | null;
}