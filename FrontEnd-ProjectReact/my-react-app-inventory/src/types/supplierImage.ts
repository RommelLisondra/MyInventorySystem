import type { Supplier } from "./supplier";

export interface SupplierImage {
  id: number;
  supplierNo: string;
  filePath?: string;
  supNoNavigation: Supplier;
}