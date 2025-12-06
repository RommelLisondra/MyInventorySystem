import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useWarehouse } from "../../hooks/useWarehouse";
import type { Warehouse } from "../../types/warehouse";
import { ROUTES } from "../../constants/routes";

const WarehouseFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { warehouses, addNewWarehouse, updateWarehouse } = useWarehouse();

    const existing = warehouses.find((e) => e.id === Number(id));

    const [form, setForm] = useState<Warehouse>(
        existing || {
            id: 0,
            wareHouseCode: "",
            name: "",
            createdDateTime: new Date(),
            modifiedDateTime: new Date(),
            status: "A",
            itemDetail: undefined,
            location: undefined,
        }
    );

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof Warehouse, value: any) => {
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSubmit = () => {
        if (!form.wareHouseCode) {
            return;
        }
    
        if (id) updateWarehouse(form);
        else addNewWarehouse(form);

        navigate(ROUTES.WAREHOUSE_LIST);
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                    <div className="row">
                        <div className="col">
                            <h3 className="page-title">{id ? "Edit Warehouse" : "Create Warehouse"}</h3>
                            <ul className="breadcrumb">
                                <li className="breadcrumb-item">
                                <a
                                    href="#"
                                    onClick={(e) => {
                                        e.preventDefault();
                                        navigate(ROUTES.WAREHOUSE_LIST);
                                    }}
                                >
                                    Warehouse List
                                </a>
                                </li>
                                <li className="breadcrumb-item active">
                                {id ? "Edit Warehouse" : "Create Warehouse"}
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <h5 className="card-title">Warehouse Details</h5>
                            </div>
                            <div className="card-body">
                                <form>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Warehouse Name</label>
                                        <div className="col-md-10">
                                        <input
                                            type="text"
                                            className="form-control"
                                            value={form.name}
                                            onChange={(e) => handleChange("name", e.target.value)}
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
                                            onClick={() => navigate(ROUTES.WAREHOUSE_LIST)}
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

export default WarehouseFormPage;
