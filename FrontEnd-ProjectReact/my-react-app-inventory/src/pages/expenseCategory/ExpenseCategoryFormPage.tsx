import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useExpenseCategory } from "../../hooks/useExpenseCategory";
import type { ExpenseCategory} from "../../types/expenseCategory";
import { ROUTES } from "../../constants/routes";

const ExpenseCategoryFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { expenseCategory, addNewExpenseCategory, updateExpenseCategory } = useExpenseCategory();

    const existing = expenseCategory.find((e) => e.id === Number(id));

    const [form, setForm] = useState<ExpenseCategory>(
        existing || {
            id: 0,
            code: "",
            name: "",
            description: undefined,
            isActive: true,
            createdDate: new Date(),
            modifiedDate: undefined,
            expenses: [], // Initialize as empty array
        }
    );

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof ExpenseCategory, value: any) => {
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSubmit = () => {
        if (!form.code) {
            return;
        } 

        if (id) updateExpenseCategory(form);
        else addNewExpenseCategory(form);
                
        navigate(ROUTES.EXPENSE_CATEGORY_LIST);
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                    <div className="row">
                        <div className="col">
                            <h3 className="page-title">{id ? "Edit ExpenseCategory" : "Create ExpenseCategory"}</h3>
                            <ul className="breadcrumb">
                                <li className="breadcrumb-item">
                                    <a
                                        href="#"
                                        onClick={(e) => {
                                            e.preventDefault();
                                            navigate(ROUTES.EXPENSE_CATEGORY_LIST);
                                        }}
                                    >
                                        ExpenseCategory List
                                    </a>
                                </li>
                                <li className="breadcrumb-item active">
                                    {id ? "Edit ExpenseCategory" : "Create ExpenseCategory"}
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <h5 className="card-title">ExpenseCategory Details</h5>
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
                                                onClick={() => navigate(ROUTES.EXPENSE_CATEGORY_LIST)}
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

export default ExpenseCategoryFormPage;
