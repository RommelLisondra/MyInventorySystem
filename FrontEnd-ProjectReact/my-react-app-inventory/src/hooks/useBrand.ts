import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchBrands,
  fetchBrandById,
  searchBrands,
  addBrand,
  updateBrandById,
  deleteBrandById,
} from "../features/brand/brandThunk";
import type { Brand } from "../types/brand";

interface UseBrandReturn {
  brands: Brand[];
  selectedBrand: Brand | null;
  loading: boolean;
  error: string | null;
  addNewBrand: (brand: Brand) => void;
  updateBrand: (brand: Brand) => void;
  deleteBrand: (id: number) => void;
  reloadBrands: () => void;
  getBrandById: (id: number) => void;
  searchBrand: (name: string) => void;
}

export const useBrand = (): UseBrandReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { brands, selectedBrand, loading, error } = useSelector(
    (state: RootState) => state.brand
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchBrands());
  }, [dispatch]);

  const addNewBrand = useCallback(
    (brand: Brand) => {
      dispatch(addBrand(brand));
    },
    [dispatch]
  );

  const updateBrand = useCallback(
    (brand: Brand) => {
      dispatch(updateBrandById(brand));
    },
    [dispatch]
  );

  const deleteBrand = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Brand?")) {
        dispatch(deleteBrandById(id));
      }
    },
    [dispatch]
  );

  const reloadBrands = useCallback(() => {
    dispatch(fetchBrands());
  }, [dispatch]);

  const getBrandById = useCallback(
    (id: number) => {
      dispatch(fetchBrandById(id));
    },
    [dispatch]
  );

  const searchBrand = useCallback((keyword: string) => {
      dispatch(searchBrands(keyword));
    },
    [dispatch]
  );

  return {
    brands,
    selectedBrand,
    loading,
    error,
    addNewBrand,
    updateBrand,
    deleteBrand,
    reloadBrands,
    getBrandById,
    searchBrand,
  };
};
