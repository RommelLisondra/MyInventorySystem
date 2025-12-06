import type { Company } from "./company";
import type { Warehouse } from "./warehouse";
import type { Employee } from "./employee";
import type { ItemWarehouseMapping } from "./itemWarehouse";

export interface Branch {
  id: number;
  companyId: number;
  branchCode: string;
  branchName: string;
  address: boolean;
  contactNo?: string | null;
  email?: string | null;
  createdDateTime?: string | null;
  modifiedDateTime?: string | null;
  isActive?: boolean;

  company: Company; // you'll need to define this interface
  warehouses: Warehouse[]; // define this interface as well
  employees: Employee[]; // define this interface
  itemWarehouseMappings: ItemWarehouseMapping[]; // define this interface
}