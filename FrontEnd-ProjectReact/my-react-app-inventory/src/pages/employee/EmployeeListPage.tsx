import React from "react";
import { useNavigate } from "react-router-dom";
import { useEmployee } from "../../hooks/useEmployee";
import EmployeeList from "../../components/employee/EmployeeList";
import type { Employee } from "../../types/employee";
import { ROUTES } from "../../constants/routes";

const EmployeeListPage: React.FC = () => {
  const { employees, deleteEmployee, reloadEmployees,searchEmployee } = useEmployee();
  const navigate = useNavigate();

  return (
    <EmployeeList
          employees={employees}
          onDelete={deleteEmployee}
          onEdit={(id) => navigate(ROUTES.EMPLOYEE_EDIT.replace(":id", String(id)))}
          onReload={reloadEmployees} 
          onUpdate={function (_employee: Employee): void {
              throw new Error("Function not implemented.");
          } } 
          onSearch={searchEmployee}   />
  );
};

export default EmployeeListPage;