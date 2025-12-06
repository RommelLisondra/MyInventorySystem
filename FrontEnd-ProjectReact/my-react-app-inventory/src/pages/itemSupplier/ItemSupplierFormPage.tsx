import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useItemSupplier } from "../../hooks/useItemSupplier";
import type { ItemSupplier } from "../../types/itemSupplier";
import { ROUTES } from "../../constants/routes";
import type { ItemDetail } from "../../types/itemDetail";
import type { Supplier } from "../../types/supplier";

// Assume these modals exist
import SupplierModal from "../../components/modal/SupplierModal";
import ItemModal from "../../components/modal/ItemModal";

const ItemSupplierFormPage: React.FC = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { itemSuppliers, addNewItemSupplier, updateItemSupplier } = useItemSupplier();

  const existing = itemSuppliers.find((e) => e.id === Number(id));

  const [form, setForm] = useState<ItemSupplier>(
    existing || {
      id: 0,
      itemDetailCode: "",     // required string → empty string default
      supplierNo: "",         // required string → empty string default
      unitPrice: undefined,
      leadTime: undefined,
      terms: undefined,
      modifiedDateTime: new Date(),
      createdDateTime: new Date(),
      recStatus: undefined,
      
      // initialize as empty objects that match the interface
      itemDetail: {} as ItemDetail,
      supplier: {} as Supplier,
    }
  );

  const [showSupplierModal, setShowSupplierModal] = useState(false);
  const [showItemModal, setShowItemModal] = useState(false);

  useEffect(() => {
    if (existing) setForm(existing);
  }, [existing]);

  const handleChange = (field: keyof ItemSupplier, value: any) => {
    setForm((prev) => ({ ...prev, [field]: value }));
  };

  const handleSelectSupplier = (supplier: Supplier) => {
    setForm((prev) => ({
      ...prev,
      supplierNo: supplier.supplierNo,
      supplier,
    }));
    setShowSupplierModal(false);
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
    if (!form.itemDetailCode || !form.supplierNo) {
      alert("Please fill in required fields.");
      return;
    }

    if (id) updateItemSupplier(form);
    else addNewItemSupplier(form);

    navigate(ROUTES.ITEMSUPPLIER_LIST);
  };

  return (
    <div className="cardhead">
      <div className="content container-fluid">
        <div className="page-header">
          <div className="row">
            <div className="col">
                <h3 className="page-title">{id ? "Edit Item Supplier" : "Create Item Supplier"}</h3>
                  <ul className="breadcrumb">
                    <li className="breadcrumb-item">
                      <a
                          href="#"
                          onClick={(e) => {
                            e.preventDefault();
                            navigate(ROUTES.ITEMSUPPLIER_LIST);
                          }}
                      >
                        ItemSupplier List
                      </a>
                    </li>
                    <li className="breadcrumb-item active">
                      {id ? "Edit Item Supplier" : "Create Item Supplier"}
                    </li>
                  </ul>
            </div>
          </div>
        </div>

        <div className="row">
          <div className="col-lg-12">
            <div className="card">
              <div className="card-header">
                <h5 className="card-title">Item Supplier Details</h5>
              </div>
              <div className="card-body">
                <form>
                    <div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Supplier No.</label>
                        <div className="col-md-10">
                          <div className="d-flex align-items-center">
                            <input
                              type="text"
                              className="form-control"
                              value={form.supplierNo}
                              onChange={(e) => handleChange("supplierNo", e.target.value)}
                              style={{ maxWidth: "500px" }}   /* optional */
                            />
                            <button
                              type="button"
                              className="btn btn-primary"
                              style={{ marginLeft: "6px" }}
                              onClick={() => setShowSupplierModal(true)}
                            >
                              Find
                            </button>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div className="form-group row">
                        <label className="col-form-label col-md-2">Supplier Name</label>
                        <div className="col-md-10">
                        <input
                            type="text"
                            className="form-control"
                            value={form.supplier?.address || ""}
                            readOnly
                        />
                        </div>
                    </div>
                    <div className="form-group row">
                        <label className="col-form-label col-md-2">Address</label>
                        <div className="col-md-10">
                        <input
                            type="text"
                            className="form-control"
                            value={form.supplier?.address || ""}
                            readOnly
                        />
                        </div>
                    </div>
                    <div className="form-group row">
                        <label className="col-form-label col-md-2">Contact Person</label>
                        <div className="col-md-10">
                        <input
                            type="text"
                            className="form-control"
                            value={form.supplier?.address || ""}
                            readOnly
                        />
                        </div>
                    </div>
                    <div className="form-group row">
                        <label className="col-form-label col-md-2">Contact No.</label>
                        <div className="col-md-10">
                        <input
                            type="text"
                            className="form-control"
                            value={form.supplier?.address || ""}
                            readOnly
                        />
                        </div>
                    </div>
                    <div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Item DetailCode</label>
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
                    </div>
                    <div className="form-group row">
                        <label className="col-form-label col-md-2">Brand Name</label>
                        <div className="col-md-10">
                        <input
                            type="text"
                            className="form-control"
                            value={form.supplier?.address || ""}
                            readOnly
                        />
                        </div>
                    </div>
                    <div className="form-group row">
                        <label className="col-form-label col-md-2">Description</label>
                        <div className="col-md-10">
                        <input
                            type="text"
                            className="form-control"
                            value={form.supplier?.address || ""}
                            readOnly
                        />
                        </div>
                    </div>
                    <div className="form-group row">
                        <label className="col-form-label col-md-2">Unit Price</label>
                        <div className="col-md-10">
                        <input
                            type="text"
                            className="form-control"
                            value={form.unitPrice}
                            onChange={(e) => handleChange("unitPrice", e.target.value)}
                        />
                        </div>
                    </div>

                    <div className="form-group row">
                        <label className="col-form-label col-md-2">Terms</label>
                        <div className="col-md-10">
                        <input
                            type="text"
                            className="form-control"
                            value={form.terms}
                            onChange={(e) => handleChange("terms", e.target.value)}
                        />
                        </div>
                    </div>
                    
                    <div className="form-group row">
                        <label className="col-form-label col-md-2">Lead Time</label>
                        <div className="col-md-10">
                        <input
                            type="text"
                            className="form-control"
                            value={form.leadTime}
                            onChange={(e) => handleChange("leadTime", e.target.value)}
                        />
                        </div>
                    </div>
                    <div className="form-group row mt-3">
                        <div className="col-md-10 offset-md-2 text-end">
                          <button 
                              className="btn btn-success me-2" 
                              type="button"
                              onClick={handleSubmit}
                          >
                              {id ? "Update" : "Create"}
                          </button>
                          <button
                              className="btn btn-secondary"
                              type="button"
                              onClick={() => navigate(ROUTES.ITEMSUPPLIER_LIST)}
                          >
                              Cancel
                          </button>
                        </div>
                    </div>
                </form>
                {/* Modals */}
                {showSupplierModal && (
                    <SupplierModal
                        onSelect={handleSelectSupplier}
                        onClose={() => setShowSupplierModal(false)} 
                        show={showSupplierModal}
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

export default ItemSupplierFormPage;

