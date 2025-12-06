import React from "react";
import { useNavigate } from "react-router-dom";
import { useSubCategory } from "../../hooks/useSubCategory";
import SubCategoryList from "../../components/subcategory/SubCategoryList";
import type { SubCategory } from "../../types/subCategory";
import { ROUTES } from "../../constants/routes";

const SubCategoryListPage: React.FC = () => {
    const { subCategory, deleteSubCategory, reloadSubCategorys,searchSubCategory } = useSubCategory();
    const navigate = useNavigate();

    return (
        <SubCategoryList
            subCategory={subCategory}
            onDelete={deleteSubCategory}
            onEdit={(id) => navigate(ROUTES.SUB_CATEGORY_EDIT.replace(":id", String(id)))}
            onReload={reloadSubCategorys} 
            onUpdate={function (_employee: SubCategory): void {
                throw new Error("Function not implemented.");
            } } 
            onSearch={searchSubCategory}   />
    );
};

export default SubCategoryListPage;