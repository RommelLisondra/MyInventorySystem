import type { Item } from "./item";

export interface Classification {
  id: number;
  name: string;
  description?: string;
  createdDateTime: Date;
  modifiedDateTime: Date;
  recStatus?: string;

  // Collection of Items
  items: Item[];
}