import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useExpense } from "../../hooks/useExpense";
import type { Expense} from "../../types/expense";
import { ROUTES } from "../../constants/routes";
import type { ExpenseCategory } from "../../types/expenseCategory";

const ExpenseFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { expenses, addNewExpense, updateExpense } = useExpense();

    const existing = expenses.find((e) => e.id === Number(id));

    const [form, setForm] = useState<Expense>(
        existing || {
            id: 0,
            expenseNo: "",
            expenseDate: new Date(),
            expenseCategoryId: 0,
            amount: 0,
            vendor: undefined,
            referenceNo: undefined,
            notes: undefined,
            attachmentPath: undefined,
            createdDate: new Date(),
            isDeleted: false,
            expenseCategory: {} as ExpenseCategory,
        }
    );

    useEffect(() => {
        if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof Expense, value: any) => {
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSubmit = () => {
        if (!form.expenseNo) {
            return;
        } 

        if (id) updateExpense(form);
        else addNewExpense(form);
                
        navigate(ROUTES.EXPENSE_LIST);
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                    <div className="row">
                        <div className="col">
                            <h3 className="page-title">{id ? "Edit Expense" : "Create Expense"}</h3>
                            <ul className="breadcrumb">
                                <li className="breadcrumb-item">
                                    <a
                                        href="#"
                                        onClick={(e) => {
                                            e.preventDefault();
                                            navigate(ROUTES.EXPENSE_LIST);
                                        }}
                                    >
                                        Expense List
                                    </a>
                                </li>
                                <li className="breadcrumb-item active">
                                    {id ? "Edit Expense" : "Create Expense"}
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="card">
                            <div className="card-header">
                                <h5 className="card-title">Expense Details</h5>
                            </div>
                            <div className="card-body">
                                <form>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Vendor</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.vendor}
                                                    onChange={(e) => handleChange("vendor", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">ReferenceNo</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.referenceNo}
                                                    onChange={(e) => handleChange("referenceNo", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                            </div>
                                        </div>
                                    </div>
                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">Notes</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.notes}
                                                    onChange={(e) => handleChange("notes", e.target.value)}
                                                    style={{ maxWidth: "500px" }}   /* optional */
                                                />
                                            </div>
                                        </div>
                                    </div>

                                    <div className="form-group row">
                                        <label className="col-form-label col-md-2">AttachmentPath</label>
                                        <div className="col-md-10">
                                            <div className="d-flex align-items-center">
                                                <input
                                                    type="text"
                                                    className="form-control"
                                                    value={form.attachmentPath}
                                                    onChange={(e) => handleChange("attachmentPath", e.target.value)}
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
                                                onClick={() => navigate(ROUTES.EXPENSE_LIST)}
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

export default ExpenseFormPage;
