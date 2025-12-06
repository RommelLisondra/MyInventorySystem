import type { RootState } from "../../app/store";

export const selectSalesInvoice = (state: RootState) => state.salesInvoice.salesInvoice;
export const selectSalesInvoiceLoading = (state: RootState) => state.salesInvoice.loading;
export const selectSalesInvoiceError = (state: RootState) => state.salesInvoice.error;

// Optional: selector para sa isang customer by id
export const selectSalesInvoiceById = (id: number) => (state: RootState) =>
  state.salesInvoice.salesInvoice.find((c) => c.id === id);
