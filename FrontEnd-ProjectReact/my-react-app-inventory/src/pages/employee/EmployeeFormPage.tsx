import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useEmployee } from "../../hooks/useEmployee";
import type { Employee } from "../../types/employee";
import { ROUTES } from "../../constants/routes";

const EmployeeFormPage: React.FC = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { employees, addNewEmployee, updateEmployee } = useEmployee();

  const existing = employees.find((e) => e.id === Number(id));

  const [form, setForm] = useState<Employee>(
    existing || {
        id: 0,
        empId: undefined,
        empIdno: "",
        lastName: "",
        firstName: "",
        middleName: "",
        address: undefined,
        dateOfBirth: undefined,
        age: undefined,
        gender: undefined,
        mstatus: undefined,
        religion: undefined,
        eduAttentment: undefined,
        dateHired: undefined,
        department: undefined,
        position: undefined,
        contactNo: undefined,
        postalCode: undefined,
        country: undefined,
        state: undefined,
        emailAddress: undefined,
        fax: undefined,
        mobileNo: undefined,
        city: undefined,
        modifiedDateTime: new Date(),  // required, init with current date
        createdDateTime: new Date(),   // required, init with current date
        recStatus: undefined,
    }
  );


  useEffect(() => {
    if (existing) setForm(existing);
  }, [existing]);

  const handleChange = (field: keyof Employee, value: any) => {
    setForm((prev) => ({ ...prev, [field]: value }));
  };

//   const handleDateChange = (field: keyof Employee, value: string) => {
//     setForm((prev) => ({
//       ...prev,
//       [field]: value ? new Date(value) : undefined,
//     }));
//   };

  const handleSubmit = () => {
    if (!form.empIdno) {
      return;
    }
    
    if (id) updateEmployee(form);
    else addNewEmployee(form);
    
    navigate(ROUTES.EMPLOYEE_LIST);
  };

  return (
    <div className="cardhead">
      <div className="content container-fluid">
        <div className="page-header">
          <div className="row">
            <div className="col">
                <h3 className="page-title">{id ? "Edit Employee" : "Create Employee"}</h3>
                <ul className="breadcrumb">
                  <li className="breadcrumb-item">
                    <a
                        href="#"
                        onClick={(e) => {
                            e.preventDefault();
                            navigate(ROUTES.EMPLOYEE_LIST);
                        }}
                    >
                        Employee List
                    </a>
                  </li>
                  <li className="breadcrumb-item active">
                    {id ? "Edit Employee" : "Create Employee"}
                  </li>
                </ul>
            </div>
          </div>
        </div>
        <div className="row">
          <div className="col-lg-12">
            <div className="card">
              <div className="card-header">
                <h5 className="card-title">Employee Details</h5>
              </div>
              <div className="card-body">
                <form>
                  <div className="form-group row">
                    <label className="col-form-label col-md-2">First Name</label>
                    <div className="col-md-10">
                      <input
                        type="text"
                        className="form-control"
                        value={form.firstName}
                        onChange={(e) => handleChange("firstName", e.target.value)}
                      />
                    </div>
                  </div>

                  {/* Last Name */}
                  <div className="form-group row">
                    <label className="col-form-label col-md-2">Last Name</label>
                    <div className="col-md-10">
                      <input
                        type="text"
                        className="form-control"
                        value={form.lastName}
                        onChange={(e) => handleChange("lastName", e.target.value)}
                      />
                    </div>
                  </div>

                  {/* Middle Name */}
                  <div className="form-group row">
                    <label className="col-form-label col-md-2">Middle Name</label>
                    <div className="col-md-10">
                      <input
                        type="text"
                        className="form-control"
                        value={form.middleName}
                        onChange={(e) => handleChange("middleName", e.target.value)}
                      />
                    </div>
                  </div>

                  {/* Address */}
                  <div className="form-group row">
                    <label className="col-form-label col-md-2">Address</label>
                    <div className="col-md-10">
                      <input
                        type="text"
                        className="form-control"
                        value={form.address}
                        onChange={(e) => handleChange("address", e.target.value)}
                      />
                    </div>
                  </div>

                  {/* Country */}
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

                  {/* State */}
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

                  {/* City */}
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

                  {/* Postal Code */}
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

                  {/* Email */}
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

                  {/* Mobile No */}
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

                  {/* Contact No */}
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

                  {/* Department */}
                  <div className="form-group row">
                    <label className="col-form-label col-md-2">Department</label>
                    <div className="col-md-10">
                      <input
                        type="text"
                        className="form-control"
                        value={form.department}
                        onChange={(e) => handleChange("department", e.target.value)}
                      />
                    </div>
                  </div>

                  {/* Position */}
                  <div className="form-group row">
                    <label className="col-form-label col-md-2">Position</label>
                    <div className="col-md-10">
                      <input
                        type="text"
                        className="form-control"
                        value={form.position}
                        onChange={(e) => handleChange("position", e.target.value)}
                      />
                    </div>
                  </div>

                  {/* Buttons */}
                  <div className="form-group row mt-3">
                    <div className="col-md-10 offset-md-2 text-end">
                      <button className="btn btn-success me-2" type="submit" onClick={handleSubmit}>
                        {id ? "Update" : "Create"}
                      </button>
                      <button
                        className="btn btn-secondary"
                        type="button"
                        onClick={() => navigate(ROUTES.EMPLOYEE_LIST)}
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

export default EmployeeFormPage;
