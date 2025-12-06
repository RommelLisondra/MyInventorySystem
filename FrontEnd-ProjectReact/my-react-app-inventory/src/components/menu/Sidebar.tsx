import React, { useState } from "react";
import { Link } from "react-router-dom";
import { ROUTES } from "../../constants/routes";

const Sidebar: React.FC = () => {
  const [openMenu, setOpenMenu] = useState<string>("");

  const toggleMenu = (menu: string) => {
    setOpenMenu(openMenu === menu ? "" : menu);
  };

  return (
    <div className="sidebar" id="sidebar">
      <div className="sidebar-inner slimscroll">
        <div id="sidebar-menu" className="sidebar-menu">
          <ul>
            <li>
              <a href="/">
                <img src="/assets/img/icons/dashboard.svg" alt="img" />
                <span>Dashboard</span>
              </a>
            </li>
            {/* PRODUCT */}
            <li className="submenu">
              <a onClick={() => toggleMenu("product")}>
                <img src="/assets/img/icons/product.svg" alt="img" />
                <span>Product</span>
                <span className="menu-arrow"></span>
              </a>
              <ul>
				          <li className="submenu">
                    <a onClick={() => toggleMenu("item-settings")}>
                      <span>Item Settings</span> <span className="menu-arrow"></span>
                    </a>
                    <ul>
                      <li><Link to={ROUTES.ITEM_LIST}>Item Master</Link></li>
                      <li><Link to={ROUTES.ITEM_DETAIL_LIST}>Ite Detail</Link></li>
                      <li><Link to={ROUTES.ITEM_INVENTORY_LIST}>Item Inventory</Link></li>
                    </ul>
                  </li>
                  <li><Link to={ROUTES.CATEGORY_LIST}>Category</Link></li>
                  <li><Link to={ROUTES.SUB_CATEGORY_LIST}>Sub Category</Link></li>
                  <li><Link to={ROUTES.BRAND_LIST}>Brand</Link></li>
                  <li><Link to={ROUTES.CLASSIFICATION_LIST}>Classification</Link></li>
                  <li><Link to="/importproduct">Import Products</Link></li>
                  <li><Link to="/barcode">Print Barcode</Link></li>
              </ul>
            </li>
            {/* SALES */}
            <li className="submenu">
              <a onClick={() => toggleMenu("sales")}>
                <img src="/assets/img/icons/sales1.svg" alt="img" />
                <span>Sales</span> <span className="menu-arrow"></span>
              </a>
              <ul style={{ display: openMenu === "sales" ? "block" : "none" }}>
                <li><Link to="/saleslist">Sales Order</Link></li>
                <li><Link to="/newsales">Sales Invoice</Link></li>
                <li><Link to="/salesreturnlists">Sales Return</Link></li>
                <li><Link to="/createsalesreturns">Delivery Receipt</Link></li>
                <li><Link to="/pos">Official Receipt</Link></li>
              </ul>
            </li>

            {/* PURCHASE */}
            <li className="submenu">
              <a onClick={() => toggleMenu("purchase")}>
                <img src="/assets/img/icons/purchase1.svg" alt="img" />
                <span>Purchasing</span> <span className="menu-arrow"></span>
              </a>
              <ul style={{ display: openMenu === "purchase" ? "block" : "none" }}>
                <li><Link to="/purchaselist">Purchase Requesition</Link></li>
                <li><Link to="/addpurchase">Purchase Order</Link></li>
                <li><Link to="/importpurchase">Receiving Report</Link></li>
                <li><Link to="/importpurchase">Purchase Return</Link></li>
              </ul>
            </li>
            {/* StockManagement */}
            <li className="submenu">
              <a onClick={() => toggleMenu("stockmanagement")}>
                <img src="/assets/img/icons/expense1.svg" alt="img" />
                <span>Stock Management</span> <span className="menu-arrow"></span>
              </a>
              <ul style={{ display: openMenu === "stockmanagement" ? "block" : "none" }}>
                <li><Link to={ROUTES.WAREHOUSE_LIST}>Warehouse</Link></li>
                <li><Link to={ROUTES.LOCATION_LIST}>Location</Link></li>
              </ul>
            </li>
            {/* EXPENSE */}
            <li className="submenu">
              <a onClick={() => toggleMenu("expense")}>
                <img src="/assets/img/icons/expense1.svg" alt="img" />
                <span> Expense</span> <span className="menu-arrow"></span>
              </a>
              <ul style={{ display: openMenu === "expense" ? "block" : "none" }}>
                <li><Link to={ROUTES.EXPENSE_LIST}>Expense</Link></li>
                <li><Link to={ROUTES.EXPENSE_CREATE}>Add Expense</Link></li>
                <li><Link to={ROUTES.EXPENSE_CATEGORY_LIST}>Expense Category</Link></li>
              </ul>
            </li>
            {/* QUOTATION */}
            <li className="submenu">
              <a onClick={() => toggleMenu("quotation")}>
                <img src="/assets/img/icons/quotation1.svg" alt="img" />
                <span>Quotation</span> <span className="menu-arrow"></span>
              </a>
              <ul style={{ display: openMenu === "quotation" ? "block" : "none" }}>
                <li><Link to="/quotationList">Quotation</Link></li>
              </ul>
            </li>
            {/* TRANSFER */}
            <li className="submenu">
              <a onClick={() => toggleMenu("transfer")}>
                <img src="/assets/img/icons/transfer1.svg" alt="img" />
                <span>Transfer</span> <span className="menu-arrow"></span>
              </a>
              <ul style={{ display: openMenu === "transfer" ? "block" : "none" }}>
                <li><Link to="/transferlist">Transfer List</Link></li>
                <li><Link to="/addtransfer">Add Transfer</Link></li>
                <li><Link to="/importtransfer">Import Transfer</Link></li>
              </ul>
            </li>
            {/* RETURN */}
            <li className="submenu">
              <a onClick={() => toggleMenu("return")}>
                <img src="/assets/img/icons/return1.svg" alt="img" />
                <span> Return</span> <span className="menu-arrow"></span>
              </a>
              <ul style={{ display: openMenu === "return" ? "block" : "none" }}>
                <li><Link to="/salesreturnlist">Sales Return</Link></li>
                <li><Link to="/purchasereturnlist">Purchase Return</Link></li>
              </ul>
            </li>
            {/* PEOPLE */}
            <li className="submenu">
              <a onClick={() => toggleMenu("people")}>
                <img src="/assets/img/icons/users1.svg" alt="img" />
                <span>People</span> <span className="menu-arrow"></span>
              </a>
              <ul>
                {/*General Settings (with submenu) */}
                <li className="submenu">
                  <a onClick={() => toggleMenu("employee-settings")}>
                    <span>Employee Settings</span> <span className="menu-arrow"></span>
                  </a>
                  <ul>
                    <li><Link to={ROUTES.EMPLOYEE_LIST}>Employee</Link></li>
                    <li><Link to={ROUTES.EMPLOYEEAPPROVER_LIST}>Approver</Link></li>
                    <li><Link to={ROUTES.EMPLOYEECHECKER_LIST}>Checker</Link></li>
                    <li><Link to={ROUTES.EMPLOYEEDELIVERER_LIST}>Deliverer</Link></li>
                    <li><Link to={ROUTES.EMPLOYEESALESREF_LIST}>Sales Ref</Link></li>
                  </ul>
                </li>
                {/* Regular menu items */}
                <li><Link to={ROUTES.CUSTOMER_LIST}>Customer</Link></li>
                <li><Link to={ROUTES.ITEMSUPPLIER_LIST}>item Supplier</Link></li>
                <li><Link to={ROUTES.SUPPLIER_LIST}>Supplier</Link></li>
              </ul>
            </li>
            <li className="submenu">
              <a onClick={() => toggleMenu("report")}>
                <img src="/assets/img/icons/time.svg" alt="img" />
                <span> Report</span> <span className="menu-arrow"></span>
              </a>
              <ul>
                <li><Link to="/purchaseorderreport">Purchase order report</Link></li>
                <li><Link to="/inventoryreport">Inventory Report</Link></li>
                <li><Link to="/salesreport">Sales Report</Link></li>
                <li><Link to="/invoicereport">Invoice Report</Link></li>
                <li><Link to="/purchasereport">Purchase Report</Link></li>
                <li><Link to="/supplierreport">Supplier Report</Link></li>
                <li><Link to="/customerreport">Customer Report</Link></li>
              </ul>
            </li>

            {/* USERS */}
            <li className="submenu">
              <a onClick={() => toggleMenu("user")}>
                <img src="/assets/img/icons/users1.svg" alt="img" />
                <span>Users</span> <span className="menu-arrow"></span>
              </a>
              <ul>
                <li><Link to="/newuser">New User</Link></li>
                <li><Link to="/userlist">Users List</Link></li>
              </ul>
            </li>

            <li className="submenu">
              <a onClick={() => toggleMenu("settings")}>
                <img src="/assets/img/icons/settings.svg" alt="img" />
                <span>Settings</span> <span className="menu-arrow"></span>
              </a>
              <ul>
                {/*General Settings (with submenu) */}
                <li className="submenu">
                  <a onClick={() => toggleMenu("general-settings")}>
                    <span>General Settings</span> <span className="menu-arrow"></span>
                  </a>
                  <ul>
                    <li><Link to="/generalsettings/profile">Company Profile</Link></li>
                    <li><Link to="/generalsettings/branches">Branches</Link></li>
                    <li><Link to="/generalsettings/holidays">Holidays</Link></li>
                    <li><Link to="/generalsettings/others">Other Config</Link></li>
                  </ul>
                </li>
                {/* Regular menu items */}
                <li><Link to="/emailsettings">Email Settings</Link></li>
                <li><Link to="/paymentsettings">Payment Settings</Link></li>
                <li><Link to="/currencysettings">Currency Settings</Link></li>
                <li><Link to="/grouppermissions">Group Permissions</Link></li>
                <li><Link to="/taxrates">Tax Rates</Link></li>
                <li><Link to="/taxrates">Document Series</Link></li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Sidebar;
