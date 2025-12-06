
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as holidayService from "../../services/holidayService";
import type { Holiday } from "../../types/holiday";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Holidays
export const fetchHolidays = createAsyncThunk(
  "holiday/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await holidayService.getHolidays();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all Holidays By page
export const fetchHolidaysPaged = createAsyncThunk<PagedResponse<Holiday[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "holiday/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await holidayService.getHolidayPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single Holiday
export const fetchHolidayById = createAsyncThunk(
  "holiday/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await holidayService.getHolidayById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Holiday
export const addHoliday = createAsyncThunk(
  "holiday/add",
  async (Holiday: Holiday, thunkAPI) => {
    try {
      return await holidayService.createHoliday(Holiday);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateHolidayById = createAsyncThunk<Holiday, Holiday>(
  "holiday/update",
  async (Holiday, thunkAPI) => {
    try {
      await holidayService.updateHoliday(Holiday);
      // Return updated Holiday so reducer gets correct payload type
      return Holiday;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Holiday
export const deleteHolidayById = createAsyncThunk<number, number>(
  "holiday/delete",
  async (id, thunkAPI) => {
    try {
      await holidayService.deleteHoliday(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Holidays
export const searchHolidays = createAsyncThunk(
  "holiday/search",
  async (name: string, thunkAPI) => {
    try {
      return await holidayService.searchHoliday(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

