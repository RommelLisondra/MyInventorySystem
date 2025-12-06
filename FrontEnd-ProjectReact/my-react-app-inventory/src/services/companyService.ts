import { COMPANY_API } from "../constants/api";
import type { Company } from "../types/company";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getCompany = async (): Promise<Company[]> => {
  return authFetch(COMPANY_API);
};

export const getCompanyPaged = async (
  pageNumber: number = 1,
  pageSize: number = 20
): Promise<PagedResponse<Company[]>> => {
  return authFetch(`${COMPANY_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getCompanyById = async (id: number): Promise<Company> => {
  return authFetch(`${COMPANY_API}/${id}`);
};

export const createCompany = async (Company: Company): Promise<Company> => {
  return authFetch(COMPANY_API, {
    method: "POST",
    body: JSON.stringify(Company),
  });
};

export const updateCompany = async (Company: Company): Promise<void> => {
  await authFetch(`${COMPANY_API}/${Company.id}`, {
    method: "PUT",
    body: JSON.stringify(Company),
  });
};

export const deleteCompany = async (id: number): Promise<void> => {
  await authFetch(`${COMPANY_API}/${id}`, { method: "DELETE" });
};

export const searchCompany = async (keyword: string): Promise<Company[]> => {
  return authFetch(`${COMPANY_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
