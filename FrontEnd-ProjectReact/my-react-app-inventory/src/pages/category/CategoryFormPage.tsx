import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useCategory } from "../../hooks/useCategory";
import type { Category} from "../../types/category";
import { ROUTES } from "../../constants/routes";
import type { Brand } from "../../types/brand";

//import EmployeeModal from "../../components/modal/EmployeeModal";

const CategoryFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { category, addNewCategory, updateCategory } = useCategory();

    const existing = category.find((e) => e.id === Number(id));

    const [form, setForm] = useState<Category>(
        existing || {
            id: 0,
            categoryName: "",
            description: undefined,
            brandId: 0,
            createdDateTime: new Date(),
            modifiedDateTime: new Date(),
            recStatus: undefined,
            brand: {} as Brand,
            subCategories: [], // Initialize as empty array
        }
    );

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof Category, value: any) => {
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSubmit = () => {
        if (!form.categoryName) {
            return;
        } 

        if (id) updateCategory(form);
        else addNewCategory(form);
                
        navigate(ROUTES.CATEGORY_LIST);
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                    <div className="row">
                        <div className="col">
                            <h3 className="page-title">{id ? "Edit Category" : "Create Category"}</h3>
                            <ul className="breadcrumb">
                                <li className="breadcrumb-item">
                                    <a
                                        href="#"
                                        onClick={(e) => {
                                            e.preventDefault();
                                            navigate(ROUTES.CATEGORY_LIST);
                                        }}
                                    >
                                        Category List
                                    </a>
                                </li>
                                <li className="breadcrumb-item active">
                                    {id ? "Edit Category" : "Create Category"}
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <h5 className="card-title">Category Details</h5>
                            </div>
                            <div className="card-body">
                                <form>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Category Name</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.categoryName}
                                                    onChange={(e) => handleChange("categoryName", e.target.value)}
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
                                                    onChange={(e) => handleChange("categoryName", e.target.value)}
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
                                                onClick={() => navigate(ROUTES.CATEGORY_LIST)}
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

export default CategoryFormPage;
