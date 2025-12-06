import { RECEIVING_REPORT_API } from "../constants/api";
import type { ReceivingReportMaster } from "../types/receivingReportMaster";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService";

export const getReceivingReports = async (): Promise<ReceivingReportMaster[]> => {
  return authFetch(RECEIVING_REPORT_API);
};

export const getReceivingReportsPaged = async (pageNumber: number = 1, pageSize: number = 20 ): Promise<PagedResponse<ReceivingReportMaster[]>> => {
  return authFetch(`${RECEIVING_REPORT_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getReceivingReportById = async (id: number): Promise<ReceivingReportMaster> => {
  return authFetch(`${RECEIVING_REPORT_API}/${id}`);
};

export const createReceivingReport = async (receivingReportMaster: ReceivingReportMaster): Promise<ReceivingReportMaster> => {
  return authFetch(RECEIVING_REPORT_API, {
    method: "POST",
    body: JSON.stringify(receivingReportMaster),
  });
};

export const updateReceivingReport = async (receivingReportMaster: ReceivingReportMaster): Promise<void> => {
  await authFetch(`${RECEIVING_REPORT_API}/${receivingReportMaster.id}`, {
    method: "PUT",
    body: JSON.stringify(receivingReportMaster),
  });
};

export const deleteReceivingReport = async (id: number): Promise<void> => {
  await authFetch(`${RECEIVING_REPORT_API}/${id}`, {
    method: "DELETE",
  });
};

export const searchReceivingReports = async (keyword: string): Promise<ReceivingReportMaster[]> => {
  return authFetch(`${RECEIVING_REPORT_API}/search?keyword=${encodeURIComponent(keyword)}`);
};