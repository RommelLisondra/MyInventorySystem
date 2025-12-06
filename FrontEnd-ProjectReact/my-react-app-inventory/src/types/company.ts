import type { Branch } from "./branch";

export interface Company {
  id: number;
  companyCode: string;
  companyName: string;
  address?: string;
  contactNo?: string;
  email?: string;
  createdDateTime: string; // or Date
  modifiedDateTime?: string; // or Date
  isActive: boolean;

  // Navigation property
  branches: Branch[]; // define this interface as well
}