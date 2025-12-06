import type { Role } from "./role";

export interface RolePermission {
  id: number;
  roleId: number;
  permissionName: string;
  canView: boolean;
  canAdd: boolean;
  canEdit: boolean;
  canDelete: boolean;
  recStatus?: string | null;
  role: Role;
}