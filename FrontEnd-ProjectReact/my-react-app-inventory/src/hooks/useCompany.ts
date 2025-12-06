import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchCompanys,
  fetchCompanyById,
  searchCompany,
  addCompany,
  updateCompanyById,
  deleteCompanyById,
} from "../features/company/companyThunk";
import type { Company } from "../types/company";

interface UseCompanyReturn {
    company: Company[];
    selectedCompany: Company | null;
    loading: boolean;
    error: string | null;
    addNewCompany: (Company: Company) => void;
    updateCompany: (Company: Company) => void;
    deleteCompany: (id: number) => void;
    reloadCompanys: () => void;
    getCompanyById: (id: number) => void;
    searchCompanies: (name: string) => void;
}

export const useCompany = (): UseCompanyReturn => {
    const dispatch = useDispatch<AppDispatch>();
    const { company, selectedCompany, loading, error } = useSelector(
      (state: RootState) => state.company
    );

    // Load on mount
    useEffect(() => {
      dispatch(fetchCompanys());
    }, [dispatch]);

    const addNewCompany = useCallback(
      (Company: Company) => {
        dispatch(addCompany(Company));
      },
      [dispatch]
    );

    const updateCompany = useCallback(
      (Company: Company) => {
        dispatch(updateCompanyById(Company));
      },
      [dispatch]
    );

    const deleteCompany = useCallback(
      (id: number) => {
        if (confirm("Are you sure you want to delete this Company?")) {
          dispatch(deleteCompanyById(id));
        }
      },
      [dispatch]
    );

    const reloadCompanys = useCallback(() => {
      dispatch(fetchCompanys());
    }, [dispatch]);

    const getCompanyById = useCallback(
      (id: number) => {
        dispatch(fetchCompanyById(id));
      },
      [dispatch]
    );

    const searchCompanies = useCallback((keyword: string) => {
        dispatch(searchCompany(keyword));
      },
      [dispatch]
    );

    return {
      company,
      selectedCompany,
      loading,
      error,
      addNewCompany,
      updateCompany,
      deleteCompany,
      reloadCompanys,
      getCompanyById,
      searchCompanies,
    };
};
