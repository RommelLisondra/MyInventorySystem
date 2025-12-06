import React from "react";
import { useNavigate } from "react-router-dom";
import { useExpenseCategory } from "../../hooks/useExpenseCategory";
import ExpenseCategoryList from "../../components/expenseCategory/ExpenseCategoryList";
import type { ExpenseCategory } from "../../types/expenseCategory";
import { ROUTES } from "../../constants/routes";

const ExpenseCategoryListPage: React.FC = () => {
    const { expenseCategory, deleteExpenseCategory, reloadExpenseCategorys,searchExpenseCategory } = useExpenseCategory();
    const navigate = useNavigate();

    return (
        <ExpenseCategoryList
            expenseCategory={expenseCategory}
            onDelete={deleteExpenseCategory}
            onEdit={(id) => navigate(ROUTES.EXPENSE_CATEGORY_EDIT.replace(":id", String(id)))}
            onReload={reloadExpenseCategorys} 
            onUpdate={function (_expenseCategory: ExpenseCategory): void {
                throw new Error("Function not implemented.");
            } } 
            onSearch={searchExpenseCategory}   />
    );
};

export default ExpenseCategoryListPage;