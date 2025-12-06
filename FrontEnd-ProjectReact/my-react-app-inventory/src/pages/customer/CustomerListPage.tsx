import React from "react";
import { useNavigate } from "react-router-dom";
import { useCustomer } from "../../hooks/useCustomer";
import CustomerList from "../../components/customer/CustomerList";
import type { Customer } from "../../types/customer";
import { ROUTES } from "../../constants/routes";

const CustomerListPage: React.FC = () => {
  const { customers, deleteCustomer, reloadCustomers,searchCustomer } = useCustomer();
  const navigate = useNavigate();

  return (
    <CustomerList
          customers={customers}
          onDelete={deleteCustomer}
          onEdit={(id) => navigate(ROUTES.CUSTOMER_EDIT.replace(":id", String(id)))}
          onReload={reloadCustomers} 
          onUpdate={function (_customer: Customer): void {
              throw new Error("Function not implemented.");
          } } 
          onSearch={searchCustomer}   />
  );
};

export default CustomerListPage;