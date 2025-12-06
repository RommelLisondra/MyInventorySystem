export interface DocumentReference {
    id: number;
    refNo: string;
    moduleName: string;
    dateReferenced?: Date | null;
    recStatus?: string | null;
}