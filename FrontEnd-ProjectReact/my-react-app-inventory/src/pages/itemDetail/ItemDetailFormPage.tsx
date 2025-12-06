import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useItemDetail } from "../../hooks/useItemDetail";
import type { ItemDetail } from "../../types/itemDetail";
import type { Warehouse } from "../../types/warehouse";
import type { Location } from "../../types/location";
import { ROUTES } from "../../constants/routes";


import WarehouseModal from "../../components/modal/WarehouseModal";
import LocationModal from "../../components/modal/LocationModal";
import ItemModal from "../../components/modal/ItemModal";

// import DatePicker from "react-datepicker";
// import "react-datepicker/dist/react-datepicker.css";

const ItemDetailFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { itemDetails, addNewItemDetail, updateItemDetail } = useItemDetail();

    const [tableData, setTableData] = useState<ItemDetail[]>([]);

    const existing = itemDetails.find((e) => e.id === Number(id));

    const [form, setForm] = useState<ItemDetail>(
        existing || {
            id: 0,
            ItemDetailCode: "",
            ItemId: "",
            ItemDetailNo: "",
            Barcode: undefined,
            SerialNo: undefined,
            PartNo: undefined,
            WarehouseCode: "",
            LocationCode: "",
            Volume: undefined,
            Size: undefined,
            Weight: undefined,
            ExpiryDate: undefined,
            UnitMeasure: undefined,
            Unitprice: undefined,
            Warranty: undefined,
            ModifiedDateTime: new Date().toISOString(),
            CreatedDateTime: new Date().toISOString(),
            Eoq: undefined,
            Rop: undefined,
            RecStatus: undefined,

            // Navigation properties
            Item: null,
            ItemImage: null,
            DeliveryReceiptDetailFile: [],
            ItemInventory: [],
            ItemSupplier: [],
            ItemUnitMeasure: [],
            PurchaseOrderDetail: [],
            PurchaseRequisitionDetail: [],
            PurchaseReturnDetail: [],
            ReceivingReportDetail: [],
            SalesInvoiceDetail: [],
            SalesOrderDetail: [],
            SalesReturnDetail: [],
            StockCountDetail: []
        }
    );

    const [showWarehouseModal, setShowWarehouseModal] = useState(false);
    const [showLocationModal, setShowLocationModal] = useState(false);
    const [showItemModal, setShowItemModal] = useState(false);

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof ItemDetail, value: any) => {
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
        if (!form.ItemId) {
            return;
        }

        if (id) updateItemDetail(form);
        else addNewItemDetail(form);

        navigate(ROUTES.ITEM_DETAIL_LIST);
    };

    const handleAddToTable = () => {
        if (!form.ItemDetailCode) {
            return;
        }

        setTableData((prev) => [...prev, { ...form }]);

        setForm({
            ...form,
            id: 0,
            //ItemDetailCode: "",
            ItemId: "",
            ItemDetailNo: "",
            Barcode: "",
            SerialNo: "",
            PartNo: "",
            WarehouseCode: "",
            LocationCode: "",
            Volume: "",
            Size: "",
            Weight: "",
            ExpiryDate: "",
            UnitMeasure: "",
            Unitprice: 0,
            Warranty: "",
            ModifiedDateTime: new Date().toISOString(),
            CreatedDateTime: new Date().toISOString(),
            Eoq: 0,
            Rop: 0,
            RecStatus: "",
        });
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                    <div className="row">
                        <div className="col">
                            <h3 className="page-title">{id ? "Edit ItemDetail" : "Create ItemDetail"}</h3>
                            <ul className="breadcrumb">
                                <li className="breadcrumb-item">
                                    <a
                                        href="#"
                                        onClick={(e) => {
                                            e.preventDefault();
                                            navigate(ROUTES.ITEM_DETAIL_LIST);
                                        }}
                                    >
                                        ItemDetail List
                                    </a>
                                </li>
                                <li className="breadcrumb-item active">
                                    {id ? "Edit ItemDetail" : "Create ItemDetail"}
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <h5 className="card-title">Item</h5>
                            </div>
                            <div className="card-body">
                                <form>
                                    <div className="row">
                                        <div className="col-xl-6">
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">ItemDetail Code</label>
                                                <div className="col-lg-7">
                                                    <div className="input-group" style={{ maxWidth: "500px" }}>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            value={form.ItemDetailCode}
                                                            onChange={(e) => handleChange("ItemDetailCode", e.target.value)}
                                                        />
                                                        <button
                                                            type="button"
                                                            className="btn btn-primary"
                                                            onClick={() => setShowItemModal(true)}
                                                        >
                                                            Find
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">Warehouse</label>
                                                <div className="col-lg-7">
                                                    <div className="input-group" style={{ maxWidth: "500px" }}>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            value={form.WarehouseCode}
                                                            onChange={(e) => handleChange("WarehouseCode", e.target.value)}
                                                        />
                                                        <button
                                                            type="button"
                                                            className="btn btn-primary"
                                                            onClick={() => setShowWarehouseModal(true)}
                                                        >
                                                            Find
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">Location</label>
                                                <div className="col-lg-7">
                                                    <div className="input-group" style={{ maxWidth: "500px" }}>
                                                        <input
                                                            type="text"
                                                            className="form-control"
                                                            value={form.LocationCode}
                                                            onChange={(e) => handleChange("LocationCode", e.target.value)}
                                                        />
                                                        <button
                                                            type="button"
                                                            className="btn btn-primary"
                                                            onClick={() => setShowLocationModal(true)}
                                                        >
                                                            Find
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">Barcode</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={form.Barcode}
                                                        onChange={(e) => handleChange("Barcode", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                        {/* Right Column */}
                                        <div className="col-xl-6">
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">SerialNo</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={form.SerialNo}
                                                        onChange={(e) => handleChange("SerialNo", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">PartNo</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={form.PartNo}
                                                        onChange={(e) => handleChange("PartNo", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">Volume</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={form.Volume}
                                                        onChange={(e) => handleChange("Volume", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">Size</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={form.Size}
                                                        onChange={(e) => handleChange("Size", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    {/* Address Section */}
                                    <div className="row">
                                        {/* Left Address */}
                                        <div className="col-xl-6">
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">Weight</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={form.Weight}
                                                        onChange={(e) => handleChange("Weight", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">ExpiryDate</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="date"
                                                        className="form-control"
                                                        value={form.ExpiryDate ? form.ExpiryDate.substring(0, 10) : ""}
                                                        onChange={(e) => handleChange("ExpiryDate", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                            {/* <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">Expiry Date</label>
                                                <div className="col-lg-9">
                                                    <DatePicker
                                                        selected={form.ExpiryDate ? new Date(form.ExpiryDate) : null}
                                                        onChange={(date) => handleChange("ExpiryDate", date?.toISOString() ?? null)}
                                                        dateFormat="MM-dd-yyyy"
                                                        className="form-control dynamic-input"
                                                    />
                                                </div>
                                            </div> */}

                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">UnitMeasure</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={form.UnitMeasure}
                                                        onChange={(e) => handleChange("UnitMeasure", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                        {/* Right Address */}
                                        <div className="col-xl-6">
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">Unitprice</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={form.Unitprice}
                                                        onChange={(e) => handleChange("Unitprice", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">Warranty</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={form.Warranty}
                                                        onChange={(e) => handleChange("Warranty", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">Eoq</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={form.Eoq}
                                                        onChange={(e) => handleChange("Eoq", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                            <div className="form-group row">
                                                <label className="col-lg-3 col-form-label">Rop</label>
                                                <div className="col-lg-9">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        value={form.Rop}
                                                        onChange={(e) => handleChange("Rop", e.target.value)}
                                                        style={{ maxWidth: "500px" }}
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="d-flex justify-content-between align-items-center mt-3">
                                        {/* LEFT BUTTONS */}
                                        <div>
                                            <button className="btn btn-success me-2" type="submit" onClick={handleSubmit}>
                                                {id ? "Update all Item Detail" : "Create all Item Detail"}
                                            </button>
                                            
                                            <button className="btn btn-primary me-2" type="button" onClick={handleAddToTable}>
                                                Add To Table
                                            </button>
                                        </div>

                                        {/* RIGHT BUTTONS */}
                                        <div>
                                            <button className="btn btn-success me-2" type="submit" onClick={handleSubmit}>
                                                {id ? "Update Item" : "Create Item"}
                                            </button>

                                            <button
                                                className="btn btn-secondary"
                                                type="button"
                                                onClick={() => navigate(ROUTES.ITEM_DETAIL_LIST)}
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
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header d-flex justify-content-between align-items-center">
                                <h5 className="card-title mb-0">Details</h5>
                            </div>
                            <div className="card-body">
                                <form>
                                    <div className="table-responsive">
                                        <table className="table table-bordered table-striped">
                                            <thead>
                                                <tr>
                                                    <th>ItemDetail Code</th>
                                                    <th>Barcode</th>
                                                    <th>SerialNo</th>
                                                    <th>PartNo</th>
                                                    <th>WarehouseCode</th>
                                                    <th>LocationCode</th>
                                                    <th>Volume</th>
                                                    <th>Size</th>
                                                    <th>Weight</th>
                                                    <th>ExpiryDate</th>
                                                    <th>UnitMeasure</th>
                                                    <th>Unitprice</th>
                                                    <th>Warranty</th>
                                                    <th>Eoq</th>
                                                    <th>Rop</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {tableData.map((item, idx) => (
                                                    <tr key={idx}>
                                                        <td>{item.ItemDetailCode}</td>
                                                        <td>{item.Barcode}</td>
                                                        <td>{item.SerialNo}</td>
                                                        <td>{item.PartNo}</td>
                                                        <td>{item.WarehouseCode}</td>
                                                        <td>{item.LocationCode}</td>
                                                        <td>{item.Volume}</td>
                                                        <td>{item.Size}</td>
                                                        <td>{item.Weight}</td>
                                                        <td>{item.ExpiryDate}</td>
                                                        <td>{item.UnitMeasure}</td>
                                                        <td>{item.Unitprice}</td>
                                                        <td>{item.Warranty}</td>
                                                        <td>{item.Eoq}</td>
                                                        <td>{item.Rop}</td>
                                                    </tr>
                                                ))}
                                            </tbody>
                                        </table>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ItemDetailFormPage;
