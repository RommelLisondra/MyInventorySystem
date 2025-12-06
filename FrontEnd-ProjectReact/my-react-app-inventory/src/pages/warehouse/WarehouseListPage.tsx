import React from "react";
import { useNavigate } from "react-router-dom";
import { useWarehouse } from "../../hooks/useWarehouse";
import WarehouseList from "../../components/warehouse/WarehouseList";
import type { Warehouse } from "../../types/warehouse";
import { ROUTES } from "../../constants/routes";

const WarehouseListPage: React.FC = () => {
    const { warehouses, deleteWarehouse, reloadWarehouses,searchWarehouse } = useWarehouse();
    const navigate = useNavigate();

    return (
        <WarehouseList
            warehouse={warehouses}
            onDelete={deleteWarehouse}
            onEdit={(id) => navigate(ROUTES.WAREHOUSE_EDIT.replace(":id", String(id)))}
            onReload={reloadWarehouses} 
            onUpdate={function (_warehouse: Warehouse): void {
                throw new Error("Function not implemented.");
            } } 
            onSearch={searchWarehouse}
        />
    );
};

export default WarehouseListPage;