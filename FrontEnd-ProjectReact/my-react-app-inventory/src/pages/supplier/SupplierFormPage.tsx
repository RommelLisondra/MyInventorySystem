import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useSupplier } from "../../hooks/useSupplier";
import type { Supplier } from "../../types/supplier";
import { ROUTES } from "../../constants/routes";

const SupplierFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { suppliers, addNewSupplier, updateSupplier } = useSupplier();

    const existing = suppliers.find((e) => e.id === Number(id));

    const [form, setForm] = useState<Supplier>(
      existing || {
        id: 0,
        guid: "",              // unique identifier
        supplierNo: "",        // supplier number/code
        name: "",
        address: "",
        address1: "",
        address2: "",
        city: "",
        state: "",
        country: "",
        emailAddress: "",
        faxNo: "",
        mobileNo: "",
        postalCode: "",
        notes: "",
        contactNo: "",
        contactPerson: "",
        recStatus: "A",   // default status
        lastPono: "",

        itemDetails: [],
        itemSuppliers: [],
        purchaseOrderMasterFiles: [],
        purchaseReturnMasterFiles: [],
        receivingReportMasterFiles: [],
        supplerImages: [],
      }
    );

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof Supplier, value: any) => {
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSubmit = () => {
        if (!form.supplierNo) {
            return;
        }
        
        if (id) updateSupplier(form);
        else addNewSupplier(form);
        
        navigate(ROUTES.WAREHOUSE_LIST);
    };

    return (
      <div className="cardhead">
        <div className="content container-fluid">
          <div className="page-header">
            <div className="row">
              <div className="col">
                  <h3 className="page-title">{id ? "Edit Supplier" : "Create Supplier"}</h3>
                    <ul className="breadcrumb">
                      <li className="breadcrumb-item">
                        <a
                            href="#"
                            onClick={(e) => {
                                e.preventDefault();
                                navigate(ROUTES.SUPPLIER_LIST);
                            }}
                        >
                            Supplier List
                        </a>
                      </li>
                      <li className="breadcrumb-item active">
                        {id ? "Edit Supplier" : "Create Supplier"}
                      </li>
                    </ul>
              </div>
            </div>
          </div>

          <div className="row">
            <div className="col-lg-12">
              <div className="card">
                <div className="card-header">
                  <h5 className="card-title">Supplier Details</h5>
                </div>
                <div className="card-body">
                  <form>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">Supplier Name</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.name}
                          onChange={(e) => handleChange("name", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">Address 1</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.address}
                          onChange={(e) => handleChange("address1", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">Address 2</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.address1}
                          onChange={(e) => handleChange("address2", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">Address 3</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.address2}
                          onChange={(e) => handleChange("address", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">Country</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.country}
                          onChange={(e) => handleChange("country", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">State</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.state}
                          onChange={(e) => handleChange("state", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">City</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.city}
                          onChange={(e) => handleChange("city", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">Postal Code</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.postalCode}
                          onChange={(e) => handleChange("postalCode", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">Email Address</label>
                      <div className="col-md-10">
                        <input
                          type="email"
                          className="form-control"
                          value={form.emailAddress}
                          onChange={(e) => handleChange("emailAddress", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">Mobile No</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.mobileNo}
                          onChange={(e) => handleChange("mobileNo", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">Contact No</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.contactNo}
                          onChange={(e) => handleChange("contactNo", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">FaxNo</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.faxNo}
                          onChange={(e) => handleChange("faxNo", e.target.value)}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label className="col-form-label col-md-2">Notes</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.notes}
                          onChange={(e) => handleChange("notes", e.target.value)}
                        />
                      </div>
                    </div>

                    <div className="form-group row">
                      <label className="col-form-label col-md-2">Contact Person</label>
                      <div className="col-md-10">
                        <input
                          type="text"
                          className="form-control"
                          value={form.contactPerson}
                          onChange={(e) => handleChange("contactPerson", e.target.value)}
                        />
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
                          onClick={() => navigate(ROUTES.SUPPLIER_LIST)}
                        >
                          Cancel
                        </button>
                      </div>
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

export default SupplierFormPage;
