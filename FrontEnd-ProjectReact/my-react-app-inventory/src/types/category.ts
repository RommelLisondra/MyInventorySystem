import type { Brand } from "./brand";
import type { SubCategory } from "./subCategory";

export interface Category {
  id: number;
  categoryName: string;
  description?: string;
  brandId: number; // FK to Brand
  createdDateTime: Date;
  modifiedDateTime: Date;
  recStatus?: string;

  // Navigation Properties
  brand: Brand;
  subCategories: SubCategory[];
}