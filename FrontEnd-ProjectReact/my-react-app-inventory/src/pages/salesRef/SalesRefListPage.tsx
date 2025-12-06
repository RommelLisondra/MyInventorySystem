import React from "react";
import { useNavigate } from "react-router-dom";
import { useEmployeeSalesRef } from "../../hooks/useEmployeeSalesRef";
import SalesRefList from "../../components/salesRef/SalesRefList";
import type { EmployeeSalesRef } from "../../types/employeeSalesRef";
import { ROUTES } from "../../constants/routes";

const SalesRefListPage: React.FC = () => {
  const { employeeSalesRef, deleteEmployeeSalesRef, reloadEmployeeSalesRefs,searchEmployeeSalesRef } = useEmployeeSalesRef();
  const navigate = useNavigate();

  return (
    <SalesRefList
          employeesalesref={employeeSalesRef}
          onDelete={deleteEmployeeSalesRef}
          onEdit={(id) => navigate(ROUTES.EMPLOYEESALESREF_EDIT.replace(":id", String(id)))}
          onReload={reloadEmployeeSalesRefs} 
          onUpdate={function (_employee: EmployeeSalesRef): void {
              throw new Error("Function not implemented.");
          } } 
          onSearch={searchEmployeeSalesRef}   />
  );
};

export default SalesRefListPage;