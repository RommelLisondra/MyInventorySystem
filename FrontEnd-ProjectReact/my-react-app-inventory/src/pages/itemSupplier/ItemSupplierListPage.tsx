import React from "react";
import { useNavigate } from "react-router-dom";
import { useItemSupplier } from "../../hooks/useItemSupplier";
import ItemSupplierList from "../../components/itemSupplier/ItemSupplierList";
import type { ItemSupplier } from "../../types/itemSupplier";
import { ROUTES } from "../../constants/routes";

const ItemSupplierListPage: React.FC = () => {
  const { itemSuppliers, deleteItemSupplier, reloadItemSuppliers,searchItemSupplier } = useItemSupplier();
  const navigate = useNavigate();

  return (
    <ItemSupplierList
          itemsuppliers={itemSuppliers}
          onDelete={deleteItemSupplier}
          onEdit={(id) => navigate(ROUTES.ITEMSUPPLIER_EDIT.replace(":id", String(id)))}
          onReload={reloadItemSuppliers} 
          onUpdate={function (_itemsupplier: ItemSupplier): void {
              throw new Error("Function not implemented.");
          } } 
          onSearch={searchItemSupplier}   />
  );
};

export default ItemSupplierListPage;