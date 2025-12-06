import React from "react";
import { useCustomer } from "../../hooks/useCustomer";
import CustomerList from "../../components/customer/CustomerList";

const CustomerContainer: React.FC = () => {
  const {
    customers,
    loading,
    error,
    deleteCustomer,
    updateCustomer,
    reloadCustomers,
    searchCustomer,
    getCustomerById
  } = useCustomer();

  const handleDelete = (id: number) => {
    deleteCustomer(id);
  };

  const handleUpdate = (customer: any) => {
    const updated = { ...customer, name: customer.name + " (Edited)" };
    updateCustomer(updated);
  };

  const handleEdit = (id: number) => {
    getCustomerById(id);
  };

  const handleSearch = (query: string) => {
    if (query) searchCustomer(query);
    else reloadCustomers();
  };

  if (loading) return <p>Loading customers...</p>;
  if (error) return <p>Error: {error}</p>;

  return (
    <CustomerList
      customers={customers}
      onEdit={handleEdit}
      onUpdate={handleUpdate}
      onDelete={handleDelete}
      onReload={reloadCustomers}
      onSearch={handleSearch}
    />
  );
};

export default CustomerContainer;
