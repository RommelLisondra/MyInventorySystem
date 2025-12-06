import type { Employee } from "./employee";

export interface ApprovalHistory {
  id: number;
  moduleName: string;
  refNo: string;
  approvedBy?: string | null;
  dateApproved?: Date | null;
  remarks?: string | null;
  recStatus?: string | null;
  
  approvedByNavigation?: Employee | null;
}