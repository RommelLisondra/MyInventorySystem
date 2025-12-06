import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useItem } from "../../hooks/useItem";
import type { Item} from "../../types/item";
import type { Brand} from "../../types/brand";
import type { Category} from "../../types/category";
import type { ItemDetail} from "../../types/itemDetail";
import type { Classification} from "../../types/classification";
import { ROUTES } from "../../constants/routes";

import CategoryModal from "../../components/modal/CategoryModal";
import BrandModal from "../../components/modal/BrandModal";
import ClassificationModal from "../../components/modal/ClassificationModal";

const ItemFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { items, addNewItem, updateItem } = useItem();

    const existing = items.find((e) => e.id === Number(id));

    const [form, setForm] = useState<Item>(
        existing || {
            id: 0,
            ItemCode: "",
            ItemName: "",
            Desc: "",
            BrandId: 0,
            Model: "",
            CategoryId: 0,
            ClassificationId: 0,
            CreatedDateTime: new Date().toISOString(), // initialize to now
            ModifiedDateTime: new Date().toISOString(),
            RecStatus: undefined, // optional
            Brand: {} as Brand, // placeholder object
            Category: {} as Category, // placeholder object
            ItemDetails: [] as ItemDetail[], // optional array initialized empty
        }
    );

    const [showCategoryModal, setShowCategoryModal] = useState(false);
    const [showBrandModal, setShowBrandModal] = useState(false);
    const [showClassificationModal, setShowClassificationModal] = useState(false);

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof Item, value: any) => {
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSelectBrand = (brand: Brand) => {
        setForm((prev) => ({
          ...prev,
          id: brand.id,
          brand: brand,
        }));
        setShowBrandModal(false);
    };

    const handleSelectCategory = (category: Category) => {
        setForm((prev) => ({
          ...prev,
          id: category.id,
          category: category,
        }));
        setShowCategoryModal(false);
    };
    
    const handleSelectClassification = (classification: Classification) => {
        setForm((prev) => ({
          ...prev,
          id: classification.id,
          classification: classification,
        }));
        setShowClassificationModal(false);
    };

    const handleSubmit = () => {
        if (!form.ItemCode) {
            return;
        } 

        if (id) updateItem(form);
        else addNewItem(form);
                
        navigate(ROUTES.ITEM_LIST);
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                    <div className="row">
                        <div className="col">
                            <h3 className="page-title">{id ? "Edit Item" : "Create Item"}</h3>
                            <ul className="breadcrumb">
                                <li className="breadcrumb-item">
                                    <a
                                        href="#"
                                        onClick={(e) => {
                                            e.preventDefault();
                                            navigate(ROUTES.ITEM_LIST);
                                        }}
                                    >
                                        Item List
                                    </a>
                                </li>
                                <li className="breadcrumb-item active">
                                    {id ? "Edit Item" : "Create Item"}
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <h5 className="card-title">Item Details</h5>
                            </div>
                            <div className="card-body">
                                <form>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">ItemCode</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.ItemCode}
                                                    onChange={(e) => handleChange("ItemCode", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">ItemName</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.ItemName}
                                                    onChange={(e) => handleChange("ItemName", e.target.value)}
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
                                                    value={form.Desc}
                                                    onChange={(e) => handleChange("Desc", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Model</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.Model}
                                                    onChange={(e) => handleChange("Model", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Category</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.CategoryId}
                                                    onChange={(e) => handleChange("CategoryId", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                                <button
                                                    type="button"
                                                    className="btn btn-primary"
                                                    style={{ marginLeft: "6px" }}
                                                    onClick={() => setShowCategoryModal(true)}
                                                    >
                                                    Find
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Brand</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.BrandId}
                                                    onChange={(e) => handleChange("BrandId", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                                <button
                                                    type="button"
                                                    className="btn btn-primary"
                                                    style={{ marginLeft: "6px" }}
                                                    onClick={() => setShowBrandModal(true)}
                                                    >
                                                    Find
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Classification</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.ClassificationId}
                                                    onChange={(e) => handleChange("ClassificationId", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                                <button
                                                    type="button"
                                                    className="btn btn-primary"
                                                    style={{ marginLeft: "6px" }}
                                                    onClick={() => setShowClassificationModal(true)}
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
                                {showCategoryModal && (
                                    <CategoryModal
                                        onSelect={handleSelectCategory}
                                        onClose={() => setShowCategoryModal(false)} 
                                        show={showCategoryModal}
                                    />
                                )}
                                {showBrandModal && (
                                    <BrandModal
                                        onSelect={handleSelectBrand}
                                        onClose={() => setShowBrandModal(false)} 
                                        show={showBrandModal}
                                    />
                                )}
                                {showClassificationModal && (
                                    <ClassificationModal
                                        onSelect={handleSelectClassification}
                                        onClose={() => setShowClassificationModal(false)} 
                                        show={showClassificationModal}
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

export default ItemFormPage;
