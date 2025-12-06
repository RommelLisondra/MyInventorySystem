import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useSubCategory } from "../../hooks/useSubCategory";
import type { SubCategory} from "../../types/subCategory";
import { ROUTES } from "../../constants/routes";
import type { Category } from "../../types/category";

const SubCategoryFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { subCategory, addNewSubCategory, updateSubCategory } = useSubCategory();

    const existing = subCategory.find((e) => e.id === Number(id));

    const [form, setForm] = useState<SubCategory>(
        existing || {
            id: 0,
            subCategoryName: "",
            description: undefined,
            categoryId: 0,
            createdDateTime: new Date(),
            modifiedDateTime: new Date(),
            recStatus: undefined,
            category: {} as Category,
        }
    );

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof SubCategory, value: any) => {
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSubmit = () => {
        if (!form.subCategoryName) {
            return;
        } 

        if (id) updateSubCategory(form);
        else addNewSubCategory(form);
                
        navigate(ROUTES.SUB_CATEGORY_LIST);
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                    <div className="row">
                        <div className="col">
                            <h3 className="page-title">{id ? "Edit SubCategory" : "Create SubCategory"}</h3>
                            <ul className="breadcrumb">
                                <li className="breadcrumb-item">
                                    <a
                                        href="#"
                                        onClick={(e) => {
                                            e.preventDefault();
                                            navigate(ROUTES.SUB_CATEGORY_LIST);
                                        }}
                                    >
                                        SubCategory List
                                    </a>
                                </li>
                                <li className="breadcrumb-item active">
                                    {id ? "Edit SubCategory" : "Create SubCategory"}
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <h5 className="card-title">SubCategory Details</h5>
                            </div>
                            <div className="card-body">
                                <form>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">SubCategory Name</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.subCategoryName}
                                                    onChange={(e) => handleChange("subCategoryName", e.target.value)}
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
                                                onClick={() => navigate(ROUTES.SUB_CATEGORY_LIST)}
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

export default SubCategoryFormPage;
