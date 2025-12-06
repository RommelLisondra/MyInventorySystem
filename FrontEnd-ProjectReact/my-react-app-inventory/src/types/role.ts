import type { RolePermission } from "./rolePermission";
import type { UserAccount } from "./userAccount";

export interface Role {
  id: number;
  roleName: string;
  description?: string | null;
  recStatus?: string | null;
  rolePermission?: RolePermission[] | null;
  userAccount?: UserAccount[] | null;
}