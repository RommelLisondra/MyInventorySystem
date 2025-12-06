import React from "react";
import { useNavigate } from "react-router-dom";
import { useEmployeeApprover } from "../../hooks/useEmployeeApprover";
import ApproverList from "../../components/approver/ApproverList";
import type { EmployeeApprover } from "../../types/employeeApprover";
import { ROUTES } from "../../constants/routes";

const ApproverListPage: React.FC = () => {
    const { employeeApprover, deleteEmployeeApprover, reloadEmployeeApprovers,searchEmployeeApprover } = useEmployeeApprover();
    const navigate = useNavigate();

    return (
        <ApproverList
            employees={employeeApprover}
            onDelete={deleteEmployeeApprover}
            onEdit={(id) => navigate(ROUTES.EMPLOYEEAPPROVER_EDIT.replace(":id", String(id)))}
            onReload={reloadEmployeeApprovers} 
            onUpdate={function (_employee: EmployeeApprover): void {
                throw new Error("Function not implemented.");
            } } 
            onSearch={searchEmployeeApprover}   />
    );
};

export default ApproverListPage;