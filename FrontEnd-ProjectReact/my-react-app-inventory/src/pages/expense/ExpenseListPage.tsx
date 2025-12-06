import React from "react";
import { useNavigate } from "react-router-dom";
import { useExpense } from "../../hooks/useExpense";
import ExpenseList from "../../components/expense/ExpenseList";
import type { Expense } from "../../types/expense";
import { ROUTES } from "../../constants/routes";

const ExpenseListPage: React.FC = () => {
    const { expenses, deleteExpense, reloadExpenses,searchExpense } = useExpense();
    const navigate = useNavigate();

    return (
        <ExpenseList
            expense={expenses}
            onDelete={deleteExpense}
            onEdit={(id) => navigate(ROUTES.EMPLOYEEAPPROVER_EDIT.replace(":id", String(id)))}
            onReload={reloadExpenses} 
            onUpdate={function (_expense: Expense): void {
                throw new Error("Function not implemented.");
            } } 
            onSearch={searchExpense}   />
    );
};

export default ExpenseListPage;