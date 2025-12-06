import type { Category } from "./category";

export interface SubCategory {
  id: number;
  subCategoryName: string;
  description?: string;
  categoryId: number; // FK to Category
  createdDateTime: Date;
  modifiedDateTime: Date;
  recStatus?: string;

  // Navigation Property
  category: Category;
}