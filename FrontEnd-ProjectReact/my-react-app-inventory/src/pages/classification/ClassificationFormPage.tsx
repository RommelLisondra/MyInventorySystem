import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useClassification } from "../../hooks/useClassification";
import type { Classification} from "../../types/classification";
import { ROUTES } from "../../constants/routes";

const ClassificationFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { classification, addNewClassification, updateClassification } = useClassification();

    const existing = classification.find((e) => e.id === Number(id));

    const [form, setForm] = useState<Classification>(
        existing || {
            id: 0,
            name: "",
            description: "",
            createdDateTime: new Date(),
            modifiedDateTime: new Date(),
            recStatus: "Active",
            items: [],
        }
    );

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof Classification, value: any) => {
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSubmit = () => {
        if (!form.name) {
            return;
        } 

        if (id) updateClassification(form);
        else addNewClassification(form);
                
        navigate(ROUTES.CLASSIFICATION_LIST);
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                    <div className="row">
                        <div className="col">
                            <h3 className="page-title">{id ? "Edit Classification" : "Create Classification"}</h3>
                            <ul className="breadcrumb">
                                <li className="breadcrumb-item">
                                    <a
                                        href="#"
                                        onClick={(e) => {
                                            e.preventDefault();
                                            navigate(ROUTES.CLASSIFICATION_LIST);
                                        }}
                                    >
                                        Classification List
                                    </a>
                                </li>
                                <li className="breadcrumb-item active">
                                    {id ? "Edit Classification" : "Create Classification"}
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <h5 className="card-title">Classification Details</h5>
                            </div>
                            <div className="card-body">
                                <form>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Name</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.name}
                                                    onChange={(e) => handleChange("name", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                            </div>
                                        </div>
                                    </div>

                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Description</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.description}
                                                    onChange={(e) => handleChange("description", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
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
                                                onClick={() => navigate(ROUTES.CLASSIFICATION_LIST)}
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

export default ClassificationFormPage;
