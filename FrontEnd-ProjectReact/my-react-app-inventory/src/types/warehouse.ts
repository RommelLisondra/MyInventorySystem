import type { ItemDetail } from "./itemDetail";
import type { Location } from "./location";

export interface Warehouse {
  id: number;
  wareHouseCode: string;
  name: string;
  modifiedDateTime: Date;
  createdDateTime: Date;
  status?: string;

  itemDetail?: ItemDetail;
  location?: Location;
}