import type { Branch } from "./branch";
import type { Company } from "./company";

export interface Holiday {
  id: number;
  holidayName: string;
  holidayDate: string; // or Date
  companyId?: number;
  branchId?: number;
  isRecurring: boolean;
  description?: string;
  createdDateTime: string; // or Date
  modifiedDateTime?: string; // or Date

  company?: Company;
  branch?: Branch;
}