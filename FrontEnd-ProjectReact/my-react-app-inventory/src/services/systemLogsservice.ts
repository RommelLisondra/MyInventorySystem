import { SYSTEM_LOGS_API } from "../constants/api";
import type { SystemLog } from "../types/systemLog";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getSystemLogs = async (): Promise<SystemLog[]> => {
  return authFetch(SYSTEM_LOGS_API);
};

export const getSystemLogsPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<SystemLog[]>> => {
  return authFetch(`${SYSTEM_LOGS_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getSystemLogById = async (id: number): Promise<SystemLog> => {
  return authFetch(`${SYSTEM_LOGS_API}/${id}`);
};

export const createSystemLog = async (systemLog: SystemLog): Promise<SystemLog> => {
  return authFetch(SYSTEM_LOGS_API, {
    method: "POST",
    body: JSON.stringify(systemLog),
  });
};

export const updateSystemLog = async (systemLog: SystemLog): Promise<void> => {
  await authFetch(`${SYSTEM_LOGS_API}/${systemLog.id}`, {
    method: "PUT",
    body: JSON.stringify(systemLog),
  });
};

export const deleteSystemLog = async (id: number): Promise<void> => {
  await authFetch(`${SYSTEM_LOGS_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchSystemLogs = async (keyword: string): Promise<SystemLog[]> => {
  return authFetch(`${SYSTEM_LOGS_API}/search?keyword=${encodeURIComponent(keyword)}`);
};