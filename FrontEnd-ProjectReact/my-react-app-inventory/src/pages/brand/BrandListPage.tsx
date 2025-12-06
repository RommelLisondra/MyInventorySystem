import React from "react";
import { useNavigate } from "react-router-dom";
import { useBrand } from "../../hooks/useBrand";
import BrandList from "../../components/brand/BrandList";
import type { Brand } from "../../types/brand";
import { ROUTES } from "../../constants/routes";

const ApproverListPage: React.FC = () => {
    const { brands, deleteBrand, reloadBrands,searchBrand } = useBrand();
    const navigate = useNavigate();

    return (
        <BrandList
            brand={brands}
            onDelete={deleteBrand}
            onEdit={(id) => navigate(ROUTES.BRAND_EDIT.replace(":id", String(id)))}
            onReload={reloadBrands} 
            onUpdate={function (_brand: Brand): void {
                throw new Error("Function not implemented.");
            } } 
            onSearch={searchBrand}   />
    );
};

export default ApproverListPage;