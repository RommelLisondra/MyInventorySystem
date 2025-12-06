import { SYSTEM_SETTINGS_API } from "../constants/api";
import type { SystemSetting } from "../types/systemSetting";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getSystemSettings = async (): Promise<SystemSetting[]> => {
  return authFetch(SYSTEM_SETTINGS_API);
};

export const getSystemSettingsPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<SystemSetting[]>> => {
  return authFetch(`${SYSTEM_SETTINGS_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getSystemSettingById = async (id: number): Promise<SystemSetting> => {
  return authFetch(`${SYSTEM_SETTINGS_API}/${id}`);
};

export const createSystemSetting = async (systemSetting: SystemSetting): Promise<SystemSetting> => {
  return authFetch(SYSTEM_SETTINGS_API, {
    method: "POST",
    body: JSON.stringify(systemSetting),
  });
};

export const updateSystemSetting = async (systemSetting: SystemSetting): Promise<void> => {
  await authFetch(`${SYSTEM_SETTINGS_API}/${systemSetting.id}`, {
    method: "PUT",
    body: JSON.stringify(systemSetting),
  });
};

export const deleteSystemSetting = async (id: number): Promise<void> => {
  await authFetch(`${SYSTEM_SETTINGS_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchSystemSettings = async (keyword: string): Promise<SystemSetting[]> => {
  return authFetch(`${SYSTEM_SETTINGS_API}/search?keyword=${encodeURIComponent(keyword)}`);
};