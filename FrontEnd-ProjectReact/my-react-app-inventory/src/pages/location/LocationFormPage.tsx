import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useLocation } from "../../hooks/useLocation";
import type { Location } from "../../types/location";
import { ROUTES } from "../../constants/routes";
import type { Warehouse } from "../../types/warehouse";

const LocationFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { locations, addNewLocation, updateLocation } = useLocation();

    const existing = locations.find((e) => e.id === Number(id));

    const [form, setForm] = useState<Location>(
        existing || {
            id: 0,
            locationCode: "",
            wareHouseCode: "",
            name: "",
            createdDateTime: new Date(),
            modifiedDateTime: new Date(),
            status: "A",
            itemDetail: undefined,

            wareHouseCodeNavigation: {} as Warehouse, // initialize as empty object casted as Employee
        }
    );

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof Location, value: any) => {
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSubmit = () => {
        if (!form.locationCode) {
            return;
        }
    
        if (id) updateLocation(form);
        else addNewLocation(form);

        navigate(ROUTES.LOCATION_LIST);
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                    <div className="row">
                        <div className="col">
                            <h3 className="page-title">{id ? "Edit Location" : "Create Location"}</h3>
                            <ul className="breadcrumb">
                                <li className="breadcrumb-item">
                                <a
                                    href="#"
                                    onClick={(e) => {
                                        e.preventDefault();
                                        navigate(ROUTES.LOCATION_LIST);
                                    }}
                                >
                                    Location List
                                </a>
                                </li>
                                <li className="breadcrumb-item active">
                                {id ? "Edit Location" : "Create Location"}
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <h5 className="card-title">Location Details</h5>
                            </div>
                            <div className="card-body">
                                <form>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Location Name</label>
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
                                            onClick={() => navigate(ROUTES.LOCATION_LIST)}
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

export default LocationFormPage;
