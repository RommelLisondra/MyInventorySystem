import type { Category } from "./category";

export interface Brand {
  id: number;
  brandName: string;
  description?: string;
  createdDateTime: Date;
  modifiedDateTime: Date;
  recStatus?: string;

  // Navigation Property
  categories: Category[];
}