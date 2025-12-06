import type { Warehouse } from "./warehouse";
import type { ItemDetail } from "./itemDetail";

export interface Location {
  id: number;
  locationCode: string;
  wareHouseCode: string;
  name: string;
  modifiedDateTime: Date;
  createdDateTime: Date;
  status?: string;

  wareHouseCodeNavigation: Warehouse;
  itemDetail?: ItemDetail;
}