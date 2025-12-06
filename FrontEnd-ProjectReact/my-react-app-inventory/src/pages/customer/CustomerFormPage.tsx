import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useCustomer } from "../../hooks/useCustomer";
import type { Customer } from "../../types/customer";
import { ROUTES } from "../../constants/routes";

const CustomerFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { customers, addNewCustomer, updateCustomer } = useCustomer();

    const existing = customers.find((c) => c.id === Number(id));
    const [form, setForm] = useState<Customer>(
      existing || {
        id: 0,
        name: "",
        address1: "",
        address2: "",
        city: "",
        postalCode: "",
        country: "",
        state: "",
        emailAddress: "",
        fax: "",
        mobileNo: "",
        accountNo: "",
        creditCardNo: "",
        creditCardType: "",
        creditCardExpiry: "",
        contactNo: "",
        contactPerson: "",
        creditLimit: 0,
        balance: 0,
      }
    );

    useEffect(() => {
      if (existing) setForm(existing);
    }, [existing]);

      const handleSubmit = () => {
          if (!form.custNo) {
            return;
          }
          
          if (id) updateCustomer(form);
          else addNewCustomer(form);
          
          navigate(ROUTES.CUSTOMER_LIST);
      };

    const handleChange = (field: keyof Customer, value: any) => {
      setForm({ ...form, [field]: value });
    };

    return (
        <div className="cardhead">
          <div className="content container-fluid">
            <div className="page-header">
              <div className="row">
                <div className="col">
                  <h3 className="page-title">{id ? "Edit Customer" : "Create Customer"}</h3>
                  <ul className="breadcrumb">
                    <li className="breadcrumb-item">
                      <a
                        href="#"
                        onClick={(e) => {
                          e.preventDefault();
                          navigate(ROUTES.CUSTOMER_LIST);
                        }}
                      >
                        Customer List
                      </a>
                    </li>
                    <li className="breadcrumb-item active">
                      {id ? "Edit Customer" : "Create Customer"}
                    </li>
                  </ul>
                </div>
              </div>
            </div>
            <div className="row">
              <div className="col-lg-12">
                <div className="card">
                  <div className="card-header">
                    <h5 className="card-title">Customer Details</h5>
                  </div>
                  <div className="card-body">
                    <form>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Name</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.name}
                            onChange={(e) => handleChange("name", e.target.value)}
                            required
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Address 1</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.address1}
                            onChange={(e) => handleChange("address1", e.target.value)}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Address 2</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.address2}
                            onChange={(e) => handleChange("address2", e.target.value)}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">City</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.city}
                            onChange={(e) => handleChange("city", e.target.value)}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Postal Code</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.postalCode}
                            onChange={(e) => handleChange("postalCode", e.target.value)}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Country</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.country}
                            onChange={(e) => handleChange("country", e.target.value)}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">State</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.state}
                            onChange={(e) => handleChange("state", e.target.value)}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Email</label>
                        <div className="col-md-10">
                          <input
                            type="email"
                            className="form-control"
                            value={form.emailAddress}
                            onChange={(e) => handleChange("emailAddress", e.target.value)}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Mobile No</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.mobileNo}
                            onChange={(e) => handleChange("mobileNo", e.target.value)}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Account No</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.accountNo}
                            onChange={(e) => handleChange("accountNo", e.target.value)}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Credit Card No</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.creditCardNo}
                            onChange={(e) => handleChange("creditCardNo", e.target.value)}
                          />
                        </div>
                      </div>

                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Credit Card Type</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.creditCardType}
                            onChange={(e) => handleChange("creditCardType", e.target.value)}
                          />
                        </div>
                      </div>

                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Credit Card Expiry</label>
                        <div className="col-md-10">
                          <input
                            type="month"
                            className="form-control"
                            value={form.creditCardExpiry}
                            onChange={(e) => handleChange("creditCardExpiry", e.target.value)}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Contact No</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.contactNo}
                            onChange={(e) => handleChange("contactNo", e.target.value)}
                          />
                        </div>
                      </div>

                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Contact Person</label>
                        <div className="col-md-10">
                          <input
                            type="text"
                            className="form-control"
                            value={form.contactPerson}
                            onChange={(e) => handleChange("contactPerson", e.target.value)}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Credit Limit</label>
                        <div className="col-md-10">
                          <input
                            type="number"
                            className="form-control"
                            value={form.creditLimit}
                            onChange={(e) => handleChange("creditLimit", Number(e.target.value))}
                          />
                        </div>
                      </div>

                      <div className="form-group row">
                        <label className="col-form-label col-md-2">Balance</label>
                        <div className="col-md-10">
                          <input
                            type="number"
                            className="form-control"
                            value={form.balance}
                            onChange={(e) => handleChange("balance", Number(e.target.value))}
                          />
                        </div>
                      </div>
                      <div className="form-group row mt-3">
                        <div className="col-md-10 offset-md-2 text-end">
                          <button className="btn btn-success me-2" type="submit" onClick={handleSubmit}>
                            {id ? "Update" : "Create"}
                          </button>
                          <button
                            className="btn btn-secondary"
                            type="button"
                            onClick={() => navigate(ROUTES.CUSTOMER_LIST)}
                          >
                            Cancel
                          </button>
                        </div>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
    );
};

export default CustomerFormPage;
