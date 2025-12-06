
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchHolidays,
  fetchHolidayById,
  addHoliday,
  updateHolidayById,
  deleteHolidayById,
  searchHolidays,
  fetchHolidaysPaged,
} from "./holidayThunk";
import type { Holiday } from "../../types/holiday";
import type { PagedResponse } from "../../types/pagedResponse";

interface HolidayState {
  holiday: Holiday[];
  selectedHoliday: Holiday | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: HolidayState = {
  holiday: [],
  selectedHoliday: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const HolidaySlice = createSlice({
  name: "Holiday",
  initialState,
  reducers: {
    clearSelectedHoliday: (state) => {
      state.selectedHoliday = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchHolidays.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchHolidays.fulfilled, (state, action: PayloadAction<Holiday[]>) => {
        state.loading = false;
        state.holiday = action.payload;
      })
      .addCase(fetchHolidays.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchHolidaysPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchHolidaysPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Holiday[]>>) => {
          state.loading = false;
          state.holiday = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchHolidaysPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchHolidayById.fulfilled, (state, action: PayloadAction<Holiday>) => {
        state.selectedHoliday = action.payload;
      })

      // Add
      .addCase(addHoliday.fulfilled, (state: { holiday: Holiday[]; }, action: PayloadAction<Holiday>) => {
        state.holiday.push(action.payload);
      })

      // Update
      .addCase(updateHolidayById.fulfilled, (state: { holiday: any[]; }, action: PayloadAction<Holiday>) => {
        const index = state.holiday.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.holiday[index] = action.payload;
      })

      // Delete
      .addCase(deleteHolidayById.fulfilled, (state: { holiday: any[]; }, action: PayloadAction<number>) => {
        state.holiday = state.holiday.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchHolidays.fulfilled, (state: { holiday: Holiday[]; }, action: PayloadAction<Holiday[]>) => {
        state.holiday = action.payload;
      });
  },
});

export const { clearSelectedHoliday } = HolidaySlice.actions;
export default HolidaySlice.reducer;
