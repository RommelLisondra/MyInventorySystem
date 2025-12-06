import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchRoles,
  fetchRoleById,
  searchRoles,
  addRole,
  updateRoleById,
  deleteRoleById,
} from "../features/roles/rolesThunk";
import type { Role } from "../types/role";

interface UseRoleReturn {
  role: Role[];
  selectedrole: Role | null;
  loading: boolean;
  error: string | null;
  addNewRole: (role: Role) => void;
  updateRole: (role: Role) => void;
  deleteRole: (id: number) => void;
  reloadRoles: () => void;
  getRoleById: (id: number) => void;
  searchRole: (name: string) => void;
}

export const useRole = (): UseRoleReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { role, selectedrole, loading, error } = useSelector(
    (state: RootState) => state.role
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchRoles());
  }, [dispatch]);

  const addNewRole = useCallback(
    (role: Role) => {
      dispatch(addRole(role));
    },
    [dispatch]
  );

  const updateRole = useCallback(
    (role: Role) => {
      dispatch(updateRoleById(role));
    },
    [dispatch]
  );

  const deleteRole = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Role?")) {
        dispatch(deleteRoleById(id));
      }
    },
    [dispatch]
  );

  const reloadRoles = useCallback(() => {
    dispatch(fetchRoles());
  }, [dispatch]);

  const getRoleById = useCallback(
    (id: number) => {
      dispatch(fetchRoleById(id));
    },
    [dispatch]
  );

  const searchRole = useCallback(
    (keyword: string) => {
      dispatch(searchRoles(keyword));
    },
    [dispatch]
  );

  return {
    role,
    selectedrole,
    loading,
    error,
    addNewRole,
    updateRole,
    deleteRole,
    reloadRoles,
    getRoleById,
    searchRole,
  };
};
