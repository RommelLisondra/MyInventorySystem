import { USER_ACCOUNT_API } from "../constants/api";
import type { UserAccount } from "../types/userAccount";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getUserAccounts = async (): Promise<UserAccount[]> => {
  return authFetch(USER_ACCOUNT_API);
};

export const getUserAccountsPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<UserAccount[]>> => {
  return authFetch(`${USER_ACCOUNT_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getUserAccountById = async (id: number): Promise<UserAccount> => {
  return authFetch(`${USER_ACCOUNT_API}/${id}`);
};

export const createUserAccount = async (userAccount: UserAccount): Promise<UserAccount> => {
  return authFetch(USER_ACCOUNT_API, {
    method: "POST",
    body: JSON.stringify(userAccount),
  });
};

export const updateUserAccount = async (userAccount: UserAccount): Promise<void> => {
  await authFetch(`${USER_ACCOUNT_API}/${userAccount.id}`, {
    method: "PUT",
    body: JSON.stringify(userAccount),
  });
};

export const deleteUserAccount = async (id: number): Promise<void> => {
  await authFetch(`${USER_ACCOUNT_API}/${id}`, { method: "DELETE" });
};

export const searchUserAccounts = async (keyword: string): Promise<UserAccount[]> => {
  return authFetch(`${USER_ACCOUNT_API}/search?keyword=${encodeURIComponent(keyword)}`);
};