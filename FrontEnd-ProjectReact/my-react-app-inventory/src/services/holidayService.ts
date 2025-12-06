import { HOLIDAY_API } from "../constants/api";
import type { Holiday } from "../types/holiday";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getHolidays = async (): Promise<Holiday[]> => {
  return authFetch(HOLIDAY_API);
};

export const getHolidayPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<Holiday[]>> => {
  return authFetch(`${HOLIDAY_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getHolidayById = async (id: number): Promise<Holiday> => {
  return authFetch(`${HOLIDAY_API}/${id}`);
};

export const createHoliday = async (Holiday: Holiday): Promise<Holiday> => {
  return authFetch(HOLIDAY_API, {
    method: "POST",
    body: JSON.stringify(Holiday),
  });
};

export const updateHoliday = async (Holiday: Holiday): Promise<void> => {
  await authFetch(`${HOLIDAY_API}/${Holiday.id}`, {
    method: "PUT",
    body: JSON.stringify(Holiday),
  });
};

export const deleteHoliday = async (id: number): Promise<void> => {
  await authFetch(`${HOLIDAY_API}/${id}`, { method: "DELETE" });
};

export const searchHoliday = async (keyword: string): Promise<Holiday[]> => {
  return authFetch(`${HOLIDAY_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
