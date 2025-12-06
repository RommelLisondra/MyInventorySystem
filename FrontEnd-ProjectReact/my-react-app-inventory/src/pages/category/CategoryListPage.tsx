import React from "react";
import { useNavigate } from "react-router-dom";
import { useCategory } from "../../hooks/useCategory";
import CategoryList from "../../components/category/CategoryList";
import type { Category } from "../../types/category";
import { ROUTES } from "../../constants/routes";

const CategoryListPage: React.FC = () => {
    const { category, deleteCategory, reloadCategorys,searchCategory } = useCategory();
    const navigate = useNavigate();

    return (
        <CategoryList
            category={category}
            onDelete={deleteCategory}
            onEdit={(id) => navigate(ROUTES.CATEGORY_EDIT.replace(":id", String(id)))}
            onReload={reloadCategorys} 
            onUpdate={function (_employee: Category): void {
                throw new Error("Function not implemented.");
            } } 
            onSearch={searchCategory}   />
    );
};

export default CategoryListPage;