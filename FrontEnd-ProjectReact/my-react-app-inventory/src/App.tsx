import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Layout from "./components/menu/Layout";
import Dashboard from "./components/dashboard/Dashboard";
import { ROUTES } from "./constants/routes";

import CustomerListPage from "./pages/customer/CustomerListPage";
import CustomerFormPage from "./pages/customer/CustomerFormPage";

import EmployeeListPage from "./pages/employee/EmployeeListPage";
import EmployeeFormPage from "./pages/employee/EmployeeFormPage";

import SupplierLispage from "./pages/supplier/SupplierLispage";
import SupplierFormPage from "./pages/supplier/SupplierFormPage";

import ItemSupplierListPage from "./pages/itemSupplier/ItemSupplierListPage";
import ItemSupplierFormPage from "./pages/itemSupplier/ItemSupplierFormPage";

import ApproverFormPage from "./pages/approver/ApproverFormPage";
import ApproverListPage from "./pages/approver/ApproverListPage";

import CheckerFormPage from "./pages/checker/CheckerFormPage";
import CheckerListPage from "./pages/checker/CheckerListPage";

import DelivererFormPage from "./pages/deliverer/DelivererFormPage";
import DelivererListPage from "./pages/deliverer/DelivererListPage";

import SalesRefFormPage from "./pages/salesRef/SalesRefFormPage";
import SalesRefListPage from "./pages/salesRef/SalesRefListPage";

import LocationFormPage from "./pages/location/LocationFormPage";
import LocationListPage from "./pages/location/LocationListPage";

import BrandFormPage from "./pages/brand/BrandFormPage";
import BrandListPage from "./pages/brand/BrandListPage";

import CategoryFormPage from "./pages/category/CategoryFormPage";
import CategoryListPage from "./pages/category/CategoryListPage";

import ClassificationFormPage from "./pages/classification/ClassificationFormPage";
import ClassificationListPage from "./pages/classification/ClassificationListPage";

import ExpenseFormPage from "./pages/expense/ExpenseFormPage";
import ExpenseListPage from "./pages/expense/ExpenseListPage";

import ExpenseCategoryFormPage from "./pages/expenseCategory/ExpenseCategoryFormPage";
import ExpenseCategoryListPage from "./pages/expenseCategory/ExpenseCategoryListPage";

import SubCategoryFormPage from "./pages/subcategory/SubCategoryFormPage";
import SubCategoryListPage from "./pages/subcategory/SubCategoryListPage";

import ItemListPage from "./pages/item/ItemListPage";
import ItemFormPage from "./pages/item/ItemFormPage";

import ItemDetailListPage from "./pages/itemDetail/ItemDetailListPage";
import ItemDetailFormPage from "./pages/itemDetail/ItemDetailFormPage";

import ItemInventoryListPage from "./pages/itemInventory/ItemInventoryListPage";
import ItemInventoryFormPage from "./pages/itemInventory/ItemInventoryFormPage";

import WarehouseListPage from "./pages/warehouse/WarehouseListPage";
import WarehouseFormPage from "./pages/warehouse/WarehouseFormPage";

const App = () => {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path={ROUTES.CUSTOMER_LIST} element={<CustomerListPage />} />
          <Route path={ROUTES.CUSTOMER_CREATE} element={<CustomerFormPage />} />
          <Route path={ROUTES.CUSTOMER_EDIT} element={<CustomerFormPage />} />
          
          <Route path={ROUTES.EMPLOYEE_LIST} element={<EmployeeListPage />} />
          <Route path={ROUTES.EMPLOYEE_CREATE} element={<EmployeeFormPage />} />
          <Route path={ROUTES.EMPLOYEE_EDIT} element={<EmployeeFormPage />} />

          <Route path={ROUTES.SUPPLIER_LIST} element={<SupplierLispage />} />
          <Route path={ROUTES.SUPPLIER_CREATE} element={<SupplierFormPage />} />
          <Route path={ROUTES.SUPPLIER_EDIT} element={<SupplierFormPage />} />

          <Route path={ROUTES.ITEMSUPPLIER_LIST} element={<ItemSupplierListPage />} />
          <Route path={ROUTES.ITEMSUPPLIER_CREATE} element={<ItemSupplierFormPage />} />
          <Route path={ROUTES.ITEMSUPPLIER_EDIT} element={<ItemSupplierFormPage />} />

          <Route path={ROUTES.EMPLOYEEAPPROVER_LIST} element={<ApproverListPage />} />
          <Route path={ROUTES.EMPLOYEEAPPROVER_CREATE} element={<ApproverFormPage />} />
          <Route path={ROUTES.EMPLOYEEAPPROVER_EDIT} element={<ApproverFormPage />} />

          <Route path={ROUTES.EMPLOYEECHECKER_LIST} element={<CheckerListPage />} />
          <Route path={ROUTES.EMPLOYEECHECKER_CREATE} element={<CheckerFormPage />} />
          <Route path={ROUTES.EMPLOYEECHECKER_EDIT} element={<CheckerFormPage />} />

          <Route path={ROUTES.EMPLOYEEDELIVERER_LIST} element={<DelivererListPage />} />
          <Route path={ROUTES.EMPLOYEEDELIVERER_CREATE} element={<DelivererFormPage />} />
          <Route path={ROUTES.EMPLOYEEDELIVERER_EDIT} element={<DelivererFormPage />} />

          <Route path={ROUTES.EMPLOYEESALESREF_LIST} element={<SalesRefListPage />} />
          <Route path={ROUTES.EMPLOYEESALESREF_CREATE} element={<SalesRefFormPage />} />
          <Route path={ROUTES.EMPLOYEESALESREF_EDIT} element={<SalesRefFormPage />} />

          <Route path={ROUTES.WAREHOUSE_LIST} element={<WarehouseListPage />} />
          <Route path={ROUTES.WAREHOUSE_CREATE} element={<WarehouseFormPage />} />
          <Route path={ROUTES.WAREHOUSE_EDIT} element={<WarehouseFormPage />} />

          <Route path={ROUTES.LOCATION_LIST} element={<LocationListPage />} />
          <Route path={ROUTES.LOCATION_CREATE} element={<LocationFormPage />} />
          <Route path={ROUTES.LOCATION_EDIT} element={<LocationFormPage />} />

          <Route path={ROUTES.BRAND_LIST} element={<BrandListPage />} />
          <Route path={ROUTES.BRAND_CREATE} element={<BrandFormPage />} />
          <Route path={ROUTES.BRAND_EDIT} element={<BrandFormPage />} />

          <Route path={ROUTES.CATEGORY_LIST} element={<CategoryListPage />} />
          <Route path={ROUTES.CATEGORY_CREATE} element={<CategoryFormPage />} />
          <Route path={ROUTES.CATEGORY_EDIT} element={<CategoryFormPage />} />

          <Route path={ROUTES.CLASSIFICATION_LIST} element={<ClassificationListPage />} />
          <Route path={ROUTES.CLASSIFICATION_CREATE} element={<ClassificationFormPage />} />
          <Route path={ROUTES.CLASSIFICATION_EDIT} element={<ClassificationFormPage />} />

          <Route path={ROUTES.EXPENSE_LIST} element={<ExpenseListPage />} />
          <Route path={ROUTES.EXPENSE_CREATE} element={<ExpenseFormPage />} />
          <Route path={ROUTES.EXPENSE_EDIT} element={<ExpenseFormPage />} />

          <Route path={ROUTES.EXPENSE_CATEGORY_LIST} element={<ExpenseCategoryListPage />} />
          <Route path={ROUTES.EXPENSE_CATEGORY_CREATE} element={<ExpenseCategoryFormPage />} />
          <Route path={ROUTES.EXPENSE_CATEGORY_EDIT} element={<ExpenseCategoryFormPage />} />

          <Route path={ROUTES.SUB_CATEGORY_LIST} element={<SubCategoryListPage />} />
          <Route path={ROUTES.SUB_CATEGORY_CREATE} element={<SubCategoryFormPage />} />
          <Route path={ROUTES.SUB_CATEGORY_EDIT} element={<SubCategoryFormPage />} />

          <Route path={ROUTES.ITEM_LIST} element={<ItemListPage />} />
          <Route path={ROUTES.ITEM_CREATE} element={<ItemFormPage />} />
          <Route path={ROUTES.ITEM_EDIT} element={<ItemFormPage />} />

          <Route path={ROUTES.ITEM_DETAIL_LIST} element={<ItemDetailListPage />} />
          <Route path={ROUTES.ITEM_DETAIL_CREATE} element={<ItemDetailFormPage />} />
          <Route path={ROUTES.ITEM_DETAIL_EDIT} element={<ItemDetailFormPage />} />

          <Route path={ROUTES.ITEM_INVENTORY_LIST} element={<ItemInventoryListPage />} />
          <Route path={ROUTES.ITEM_INVENTORY_CREATE} element={<ItemInventoryFormPage />} />
          <Route path={ROUTES.ITEM_INVENTORY_EDIT} element={<ItemInventoryFormPage />} />
        </Routes>
      </Layout>
    </Router>
  );
};

export default App;


