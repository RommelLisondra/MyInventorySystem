import React from "react";
import { useNavigate } from "react-router-dom";
import { useEmployeeChecker } from "../../hooks/useEmployeeChecker";
import CheckerList from "../../components/checker/CheckerList";
import type { EmployeeChecker } from "../../types/employeeChecker";
import { ROUTES } from "../../constants/routes";

const CheckerListPage: React.FC = () => {
  const { employeeCheckers, deleteEmployeeChecker, reloadEmployeeCheckers,searchEmployeeChecker } = useEmployeeChecker();
  const navigate = useNavigate();

  return (
    <CheckerList
          employees={employeeCheckers}
          onDelete={deleteEmployeeChecker}
          onEdit={(id) => navigate(ROUTES.EMPLOYEECHECKER_EDIT.replace(":id", String(id)))}
          onReload={reloadEmployeeCheckers} 
          onUpdate={function (_employee: EmployeeChecker): void {
              throw new Error("Function not implemented.");
          } } 
          onSearch={searchEmployeeChecker}   />
  );
};

export default CheckerListPage;