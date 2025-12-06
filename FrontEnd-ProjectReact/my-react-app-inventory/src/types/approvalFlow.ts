export interface ApprovalFlow {
  id: number;
  moduleName: string;
  level: number;
  approverId: number;
  isFinalLevel: boolean;
  recStatus?: string | null;
}