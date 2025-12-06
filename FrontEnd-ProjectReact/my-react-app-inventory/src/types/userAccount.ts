import type { Role } from "./role";

export interface UserAccount {
  id: number;
  username: string;
  passwordHash: string;
  fullName?: string | null;
  email?: string | null;
  roleId?: number | null;
  recStatus?: string | null;
  role?: Role | null;
}