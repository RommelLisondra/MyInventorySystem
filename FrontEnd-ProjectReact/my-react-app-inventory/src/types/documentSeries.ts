export interface DocumentSeries {
    id: number;
    branchId: number;
    documentType: string;
    prefix: string;
    nextNumber?: number | null;
    year?: number | null;
    suffix?: string | null;
    recStatus?: string | null;
}