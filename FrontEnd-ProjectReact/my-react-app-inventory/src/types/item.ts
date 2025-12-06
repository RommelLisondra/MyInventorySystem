
import type { Brand } from "./brand";
import type { Category } from "./category";
import type { ItemDetail } from "./itemDetail";

export interface Item {
  id: number;
  ItemCode: string;
  ItemName: string;
  Desc: string;
  BrandId: number;
  Model: string;
  CategoryId: number;
  ClassificationId: number;
  CreatedDateTime: string; // Dates are typically represented as strings in JSON
  ModifiedDateTime: string;
  RecStatus?: string;
  Brand: Brand; // Navigation property
  Category: Category // Navigation property
  ItemDetails?: ItemDetail[]; // Optional array
}