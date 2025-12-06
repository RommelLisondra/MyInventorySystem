import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchRolePermissions,
  fetchRolePermissionById,
  searchRolePermissions,
  addRolePermission,
  updateRolePermissionById,
  deleteRolePermissionById,
} from "../features/rolesPermission/rolesPermissionThunk";
import type { RolePermission } from "../types/rolePermission";

interface UseRolePermissionReturn {
  rolePermission: RolePermission[];
  selectedrolePermission: RolePermission | null;
  loading: boolean;
  error: string | null;
  addNewRolePermission: (RolePermission: RolePermission) => void;
  updateRolePermission: (RolePermission: RolePermission) => void;
  deleteRolePermission: (id: number) => void;
  reloadRolePermissions: () => void;
  getRolePermissionById: (id: number) => void;
  searchRolePermission: (name: string) => void;
}

export const useRolePermission = (): UseRolePermissionReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { rolePermission, selectedrolePermission, loading, error } = useSelector(
    (state: RootState) => state.rolePermission
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchRolePermissions());
  }, [dispatch]);

  const addNewRolePermission = useCallback(
    (RolePermission: RolePermission) => {
      dispatch(addRolePermission(RolePermission));
    },
    [dispatch]
  );

  const updateRolePermission = useCallback(
    (RolePermission: RolePermission) => {
      dispatch(updateRolePermissionById(RolePermission));
    },
    [dispatch]
  );

  const deleteRolePermission = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this RolePermission?")) {
        dispatch(deleteRolePermissionById(id));
      }
    },
    [dispatch]
  );

  const reloadRolePermissions = useCallback(() => {
    dispatch(fetchRolePermissions());
  }, [dispatch]);

  const getRolePermissionById = useCallback(
    (id: number) => {
      dispatch(fetchRolePermissionById(id));
    },
    [dispatch]
  );

  const searchRolePermission = useCallback(
    (keyword: string) => {
      dispatch(searchRolePermissions(keyword));
    },
    [dispatch]
  );

  return {
    rolePermission,
    selectedrolePermission,
    loading,
    error,
    addNewRolePermission,
    updateRolePermission,
    deleteRolePermission,
    reloadRolePermissions,
    getRolePermissionById,
    searchRolePermission,
  };
};
