import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useBrand } from "../../hooks/useBrand";
import type { Brand} from "../../types/brand";
import { ROUTES } from "../../constants/routes";

const BrandFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { brands, addNewBrand, updateBrand } = useBrand();

    const existing = brands.find((e) => e.id === Number(id));

    const [form, setForm] = useState<Brand>(
        existing || {
            id: 0,
            brandName: "",
            description: undefined,
            createdDateTime: new Date(),
            modifiedDateTime: new Date(),
            recStatus: undefined,
            categories: [], // Initialize as empty array
        }
    );

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof Brand, value: any) => {
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSubmit = () => {
        if (!form.brandName) {
            return;
        } 

        if (id) updateBrand(form);
        else addNewBrand(form);
                
        navigate(ROUTES.BRAND_LIST);
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                    <div className="row">
                        <div className="col">
                            <h3 className="page-title">{id ? "Edit Brand" : "Create Brand"}</h3>
                            <ul className="breadcrumb">
                                <li className="breadcrumb-item">
                                    <a
                                        href="#"
                                        onClick={(e) => {
                                            e.preventDefault();
                                            navigate(ROUTES.BRAND_LIST);
                                        }}
                                    >
                                        Brand List
                                    </a>
                                </li>
                                <li className="breadcrumb-item active">
                                    {id ? "Edit Brand" : "Create Brand"}
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <h5 className="card-title">Brand Details</h5>
                            </div>
                            <div className="card-body">
                                <form>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Brand Name</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.brandName}
                                                    onChange={(e) => handleChange("brandName", e.target.value)}
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
                                                onClick={() => navigate(ROUTES.BRAND_LIST)}
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

export default BrandFormPage;
