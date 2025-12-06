import React from "react";
import { useNavigate } from "react-router-dom";
import { useEmployeeDelivered } from "../../hooks/useEmployeeDelivered";
import DelivererList from "../../components/deliverer/DelivererList";
import type { EmployeeDelivered } from "../../types/employeeDelivered";
import { ROUTES } from "../../constants/routes";

const DelivererListPage: React.FC = () => {
  const { employeeDelivered, deleteEmployeeDelivered, reloadEmployeeDelivereds,searchEmployeeDelivered } = useEmployeeDelivered();
  const navigate = useNavigate();

  return (
    <DelivererList
          employees={employeeDelivered}
          onDelete={deleteEmployeeDelivered}
          onEdit={(id) => navigate(ROUTES.EMPLOYEEDELIVERER_EDIT.replace(":id", String(id)))}
          onReload={reloadEmployeeDelivereds} 
          onUpdate={function (_employee: EmployeeDelivered): void {
              throw new Error("Function not implemented.");
          } } 
          onSearch={searchEmployeeDelivered}   />
  );
};

export default DelivererListPage;