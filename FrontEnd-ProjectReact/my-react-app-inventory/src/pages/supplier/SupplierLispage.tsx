import React from "react";
import { useNavigate } from "react-router-dom";
import { useSupplier } from "../../hooks/useSupplier";
import SupplierList from "../../components/supplier/SupplierList";
import type { Supplier } from "../../types/supplier";
import { ROUTES } from "../../constants/routes";

const SupplierLispage: React.FC = () => {
  const { suppliers, deleteSupplier, reloadSuppliers,searchSupplier } = useSupplier();
  const navigate = useNavigate();

  return (
    <SupplierList
          suppliers={suppliers}
          onDelete={deleteSupplier}
          onEdit={(id) => navigate(ROUTES.SUPPLIER_EDIT.replace(":id", String(id)))}
          onReload={reloadSuppliers} 
          onUpdate={function (_supplier: Supplier): void {
              throw new Error("Function not implemented.");
          } } 
          onSearch={searchSupplier}   />
  );
};

export default SupplierLispage;