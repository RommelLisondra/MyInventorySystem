import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchHolidays,
  fetchHolidayById,
  searchHolidays,
  addHoliday,
  updateHolidayById,
  deleteHolidayById,
} from "../features/holiday/holidayThunk";
import type { Holiday } from "../types/holiday";

interface UseHolidayReturn {
    holiday: Holiday[];
    selectedHoliday: Holiday | null;
    loading: boolean;
    error: string | null;
    addNewHoliday: (Holiday: Holiday) => void;
    updateHoliday: (Holiday: Holiday) => void;
    deleteHoliday: (id: number) => void;
    reloadHolidays: () => void;
    getHolidayById: (id: number) => void;
    searchHoliday: (name: string) => void;
}

export const useHoliday = (): UseHolidayReturn => {
    const dispatch = useDispatch<AppDispatch>();
    const { holiday, selectedHoliday, loading, error } = useSelector(
      (state: RootState) => state.holiday
    );

    // Load on mount
    useEffect(() => {
      dispatch(fetchHolidays());
    }, [dispatch]);

    const addNewHoliday = useCallback(
      (Holiday: Holiday) => {
        dispatch(addHoliday(Holiday));
      },
      [dispatch]
    );

    const updateHoliday = useCallback(
      (Holiday: Holiday) => {
        dispatch(updateHolidayById(Holiday));
      },
      [dispatch]
    );

    const deleteHoliday = useCallback(
      (id: number) => {
        if (confirm("Are you sure you want to delete this Holiday?")) {
          dispatch(deleteHolidayById(id));
        }
      },
      [dispatch]
    );

    const reloadHolidays = useCallback(() => {
      dispatch(fetchHolidays());
    }, [dispatch]);

    const getHolidayById = useCallback(
      (id: number) => {
        dispatch(fetchHolidayById(id));
      },
      [dispatch]
    );

    const searchHoliday = useCallback((keyword: string) => {
        dispatch(searchHolidays(keyword));
      },
      [dispatch]
    );

    return {
      holiday,
      selectedHoliday,
      loading,
      error,
      addNewHoliday,
      updateHoliday,
      deleteHoliday,
      reloadHolidays,
      getHolidayById,
      searchHoliday,
    };
};
