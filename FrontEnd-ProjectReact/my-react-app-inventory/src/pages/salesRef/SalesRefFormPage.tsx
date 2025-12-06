import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useEmployeeSalesRef } from "../../hooks/useEmployeeSalesRef";
import type { EmployeeSalesRef} from "../../types/employeeSalesRef";
import { ROUTES } from "../../constants/routes";
import type { Employee } from "../../types/employee";

import EmployeeModal from "../../components/modal/EmployeeModal";

const SalesRefFormPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { employeeSalesRef, addNewEmployeeSalesRef, updateEmployeeSalesRef } = useEmployeeSalesRef();

    const existing = employeeSalesRef.find((e) => e.id === Number(id));

    const [form, setForm] = useState<EmployeeSalesRef>(
      existing || {
        id: 0,
        empIdno: "",
        employeeNavigation: {} as Employee, // initialize as empty object casted as Employee
      }
    );

    const [showEmployeeModal, setShowEmployeeModal] = useState(false);


    useEffect(() => {
      if (existing) setForm(existing);
    }, [existing]);

    const handleChange = (field: keyof EmployeeSalesRef, value: any) => {
      setForm((prev) => ({ ...prev, [field]: value }));
    };

    const handleSelectEmployee = (employee: Employee) => {
        setForm((prev) => ({
          ...prev,
          empIdno: employee.empIdno,
          employeeNavigation: employee,
        }));
        setShowEmployeeModal(false);
      };

    const handleSubmit = () => {
        if (!form.empIdno) {
          return;
        }
    
        if (id) updateEmployeeSalesRef(form);
        else addNewEmployeeSalesRef(form);
    
        navigate(ROUTES.EMPLOYEESALESREF_LIST);
    };

    return (
        <div className="cardhead">
            <div className="content container-fluid">
                <div className="page-header">
                  <div className="row">
                    <div className="col">
                        <h3 className="page-title">{id ? "Edit SalesRef" : "Create SalesRef"}</h3>
                        <ul className="breadcrumb">
                            <li className="breadcrumb-item">
                              <a
                                  href="#"
                                  onClick={(e) => {
                                      e.preventDefault();
                                      navigate(ROUTES.EMPLOYEESALESREF_LIST);
                                  }}
                              >
                                  SalesRef List
                              </a>
                          </li>
                          <li className="breadcrumb-item active">
                            {id ? "Edit SalesRef" : "Create SalesRef"}
                          </li>
                        </ul>
                  </div>
                </div>
              </div>
              <div className="row">
                <div className="col-lg-12">
                  <div className="card">
                    <div className="card-header">
                      <h5 className="card-title">Approver Details</h5>
                    </div>
                    <div className="card-body">
                      <form>
                        <div className="form-group row">
                            <label className="col-form-label col-md-2">Employee Id</label>
                            <div className="col-md-10">
                              <div className="d-flex align-items-center">
                                <input
                                  type="text"
                                  className="form-control"
                                  value={form.empIdno}
                                  onChange={(e) => handleChange("empIdno", e.target.value)}
                                  style={{ maxWidth: "500px" }}   /* optional */
                                />
                                <button
                                  type="button"
                                  className="btn btn-primary"
                                  style={{ marginLeft: "6px" }}
                                  onClick={() => setShowEmployeeModal(true)}
                                >
                                  Find
                                </button>
                              </div>
                            </div>
                        </div>

                        <div className="form-group row">
                          <label className="col-form-label col-md-2">Name</label>
                          <div className="col-md-10">
                            <input
                              type="text"
                              className="form-control"
                              value={
                                form.employeeNavigation
                                  ? `${form.employeeNavigation.lastName || ""}, ${
                                      form.employeeNavigation.firstName || ""
                                    } ${form.employeeNavigation.middleName || ""}.`
                                  : ""
                              }
                              readOnly
                            />
                          </div>
                        </div>
                        <div className="form-group row">
                          <label className="col-form-label col-md-2">Address</label>
                          <div className="col-md-10">
                            <input
                              type="text"
                              className="form-control"
                              value={form.employeeNavigation.address || ""}
                              readOnly
                            />
                          </div>
                        </div>
                        <div className="form-group row">
                          <label className="col-form-label col-md-2">Country</label>
                          <div className="col-md-10">
                            <input
                              type="text"
                              className="form-control"
                              value={form.employeeNavigation.country || ""}
                              readOnly
                            />
                          </div>
                        </div>
                        <div className="form-group row">
                          <label className="col-form-label col-md-2">State</label>
                          <div className="col-md-10">
                            <input
                              type="text"
                              className="form-control"
                              value={form.employeeNavigation.state || ""}
                              readOnly
                            />
                          </div>
                        </div>

                        <div className="form-group row">
                          <label className="col-form-label col-md-2">City</label>
                          <div className="col-md-10">
                            <input
                              type="text"
                              className="form-control"
                              value={form.employeeNavigation.city || ""}
                              readOnly
                            />
                          </div>
                        </div>
                        <div className="form-group row">
                          <label className="col-form-label col-md-2">Postal Code</label>
                          <div className="col-md-10">
                            <input
                              type="text"
                              className="form-control"
                              value={form.employeeNavigation.postalCode || ""}
                              readOnly
                            />
                          </div>
                        </div>
                        <div className="form-group row">
                          <label className="col-form-label col-md-2">Email</label>
                          <div className="col-md-10">
                            <input
                              type="email"
                              className="form-control"
                              value={form.employeeNavigation.emailAddress || ""}
                              readOnly
                            />
                          </div>
                        </div>
                        <div className="form-group row">
                          <label className="col-form-label col-md-2">Mobile No</label>
                          <div className="col-md-10">
                            <input
                              type="text"
                              className="form-control"
                              value={form.employeeNavigation.mobileNo || ""}
                              readOnly
                            />
                          </div>
                        </div>
                        <div className="form-group row">
                          <label className="col-form-label col-md-2">Contact No</label>
                          <div className="col-md-10">
                            <input
                              type="text"
                              className="form-control"
                              value={form.employeeNavigation.contactNo || ""}
                              readOnly
                            />
                          </div>
                        </div>
                        <div className="form-group row">
                          <label className="col-form-label col-md-2">Department</label>
                          <div className="col-md-10">
                            <input
                              type="text"
                              className="form-control"
                              value={form.employeeNavigation.department || ""}
                              readOnly
                            />
                          </div>
                        </div>
                        <div className="form-group row">
                          <label className="col-form-label col-md-2">Position</label>
                          <div className="col-md-10">
                            <input
                              type="text"
                              className="form-control"
                              value={form.employeeNavigation.position || ""}
                              readOnly
                            />
                          </div>
                        </div>
                        <div className="form-group row mt-3">
                          <div className="col-md-10 offset-md-2 text-end">
                            <button 
                              className="btn btn-success me-2" 
                              type="submit"
                              onClick={handleSubmit}
                            >
                              {id ? "Update" : "Create"}
                            </button>
                            <button
                              className="btn btn-secondary"
                              type="button"
                              onClick={() => navigate(ROUTES.EMPLOYEESALESREF_LIST)}
                            >
                              Cancel
                            </button>
                          </div>
                        </div>

                      </form>
                      {showEmployeeModal && (
                                    <EmployeeModal
                                            onSelect={handleSelectEmployee}
                                            onClose={() => setShowEmployeeModal(false)} 
                                            show={showEmployeeModal}                                
                                    />
                                )}
                    </div>
                  </div>
                </div>
              </div>
            </div>
        </div>
    );
};

export default SalesRefFormPage;
