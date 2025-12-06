import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useItemInventory } from "../../hooks/useItemInventory";
import type { ItemInventory} from "../../types/itemInventory";
import type { Warehouse} from "../../types/warehouse";
import type { Location} from "../../types/location";
import type { ItemDetail} from "../../types/itemDetail";
import { ROUTES } from "../../constants/routes";

import WarehouseModal from "../../components/modal/WarehouseModal";
import LocationModal from "../../components/modal/LocationModal";
import ItemModal from "../../components/modal/ItemModal";

const ItemInventoryFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { itemInventory, addNewItemInventory, updateItemInventory } = useItemInventory();

    const existing = itemInventory.find((e) => e.id === Number(id));

    const [form, setForm] = useState<ItemInventory>(
        existing || {
            id: 0,
            itemDetailCode: "",
            warehouseCode: "",
            LocationCode: "",
            QtyOnHand: undefined,
            QtyOnOrder: undefined,
            QtyReserved: undefined,
            ExtendedQtyOnHand: undefined,
            SalesReturnItemQty: undefined,
            PurchaseReturnItemQty: undefined,
            LastStockCount: undefined,
            recStatus: undefined,
            itemDetailNavigation: {} as ItemDetail,
            warehouseNavigation: {} as Warehouse,
            LocationCodeNavigation: {} as Location,
        }
    );

    const [showWarehouseModal, setShowWarehouseModal] = useState(false);
    const [showLocationModal, setShowLocationModal] = useState(false);
    const [showItemModal, setShowItemModal] = useState(false);

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof ItemInventory, value: any) => {
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSelectWarehouse = (warehouse: Warehouse) => {
        setForm((prev) => ({
          ...prev,
          warehouseCode: warehouse.wareHouseCode,
          warehouse,
        }));
        setShowWarehouseModal(false);
    };

    const handleSelectLocation = (location: Location) => {
        setForm((prev) => ({
          ...prev,
          LocationCode: location.locationCode,
          location,
        }));
        setShowLocationModal(false);
    };
    
    const handleSelectItemDetail = (item: ItemDetail) => {
        setForm((prev) => ({
          ...prev,
          itemDetailCode: item.ItemDetailCode,
          itemDetail: item,
        }));
        setShowItemModal(false);
    };

    const handleSubmit = () => {
        if (!form.itemDetailCode) {
            return;
        } 

        if (id) updateItemInventory(form);
        else addNewItemInventory(form);
                
        navigate(ROUTES.ITEM_LIST);
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                    <div className="row">
                        <div className="col">
                            <h3 className="page-title">{id ? "Edit ItemInventory" : "Create ItemInventory"}</h3>
                            <ul className="breadcrumb">
                                <li className="breadcrumb-item">
                                    <a
                                        href="#"
                                        onClick={(e) => {
                                            e.preventDefault();
                                            navigate(ROUTES.ITEM_LIST);
                                        }}
                                    >
                                        ItemInventory List
                                    </a>
                                </li>
                                <li className="breadcrumb-item active">
                                    {id ? "Edit ItemInventory" : "Create ItemInventory"}
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <h5 className="card-title">ItemInventory Details</h5>
                            </div>
                            <div className="card-body">
                                <form>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">ItemDetail Code</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.itemDetailCode}
                                                    onChange={(e) => handleChange("itemDetailCode", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                                <button
                                                    type="button"
                                                    className="btn btn-primary"
                                                    style={{ marginLeft: "6px" }}
                                                    onClick={() => setShowItemModal(true)}
                                                    >
                                                    Find
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Warehouse</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.warehouseCode}
                                                    onChange={(e) => handleChange("warehouseCode", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                                <button
                                                    type="button"
                                                    className="btn btn-primary"
                                                    style={{ marginLeft: "6px" }}
                                                    onClick={() => setShowWarehouseModal(true)}
                                                    >
                                                    Find
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Location</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.LocationCode}
                                                    onChange={(e) => handleChange("LocationCode", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                                <button
                                                    type="button"
                                                    className="btn btn-primary"
                                                    style={{ marginLeft: "6px" }}
                                                    onClick={() => setShowLocationModal(true)}
                                                    >
                                                    Find
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group row mt-3">
                                        <div className="col-md-10 offset-md-2 text-end">
                                            <button className="btn btn-success me-2" type="submit" onClick={handleSubmit}>
                                                {id ? "Update" : "Create"}
                                            </button>
                                            <button
                                                className="btn btn-secondary"
                                                type="button"
                                                onClick={() => navigate(ROUTES.ITEM_LIST)}
                                            >
                                                Cancel
                                            </button>
                                        </div>
                                    </div>
                                </form>
                                {/* Modals */}
                                {showWarehouseModal && (
                                    <WarehouseModal
                                        onSelect={handleSelectWarehouse}
                                        onClose={() => setShowWarehouseModal(false)} 
                                        show={showWarehouseModal}
                                    />
                                )}
                                {showLocationModal && (
                                    <LocationModal
                                        onSelect={handleSelectLocation}
                                        onClose={() => setShowLocationModal(false)} 
                                        show={showLocationModal}
                                    />
                                )}
                                {showItemModal && (
                                    <ItemModal
                                        onSelect={handleSelectItemDetail}
                                        onClose={() => setShowItemModal(false)} 
                                        show={showItemModal}
                                    />
                                )}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ItemInventoryFormPage;
