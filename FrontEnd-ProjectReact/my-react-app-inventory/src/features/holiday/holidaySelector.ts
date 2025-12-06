import type { RootState } from "../../app/store";

export const selectHoliday = (state: RootState) => state.holiday.holiday;
export const selectHolidayLoading = (state: RootState) => state.holiday.loading;
export const selectHolidayError = (state: RootState) => state.holiday.error;

// Optional: selector para sa isang customer by id
export const selectHolidayById = (id: number) => (state: RootState) =>
  state.holiday.holiday.find((c) => c.id === id);
