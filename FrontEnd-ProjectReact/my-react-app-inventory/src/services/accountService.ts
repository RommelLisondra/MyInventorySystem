import { ACCOUNT_API } from "../constants/api";
import type { Account } from "../types/account";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getAccount = async (): Promise<Account[]> => {
  return authFetch(ACCOUNT_API);
};

export const getAccountsPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<Account[]>> => {
  return authFetch(`${ACCOUNT_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getAccountById = async (id: number): Promise<Account> => {
  return authFetch(`${ACCOUNT_API}/${id}`);
};

export const createAccount = async (Account: Account): Promise<Account> => {
  return authFetch(ACCOUNT_API, {
    method: "POST",
    body: JSON.stringify(Account),
  });
};

export const updateAccount = async (Account: Account): Promise<void> => {
  await authFetch(`${ACCOUNT_API}/${Account.id}`, {
    method: "PUT",
    body: JSON.stringify(Account),
  });
};

export const deleteAccount = async (id: number): Promise<void> => {
  await authFetch(`${ACCOUNT_API}/${id}`, { method: "DELETE" });
};

export const searchAccount = async (keyword: string): Promise<Account[]> => {
  return authFetch(`${ACCOUNT_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
