import React, { useState } from "react";
import { useEmployeeChecker } from "../../hooks/useEmployeeChecker";
import type { EmployeeChecker } from "../../types/employeeChecker";

interface Props {
  show: boolean;
  onClose: () => void;
  onSelect: (employee: EmployeeChecker) => void;   // UPDATED
}

const CheckerModal: React.FC<Props> = ({ show, onClose, onSelect }) => {
    const { employeeCheckers, loading, searchEmployeeChecker } = useEmployeeChecker();
    const [keyword, setKeyword] = useState("");

    const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
      const value = e.target.value;
      setKeyword(value);
      searchEmployeeChecker(value);
    };

    return (
        <div
            className={`modal fade ${show ? "show d-block" : ""}`}
            tabIndex={-1}
            aria-labelledby="employeeSearchModal"
            aria-hidden={!show}
            style={{ background: show ? "rgba(0,0,0,.5)" : "transparent" }}
          >
          <div className="modal-dialog modal-lg modal-dialog-centered">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Search Employee</h5>
                <button className="btn-close" onClick={onClose}>x</button>
              </div>

              <div className="modal-body">
                <div className="mb-3">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Search employee..."
                    value={keyword}
                    onChange={handleSearch}
                  />
                </div>

                <div className="table-responsive" style={{ maxHeight: "350px" }}>
                  <table className="table table-bordered">
                    <thead>
                      <tr>
                        <th>Name</th>
                        <th>Position</th>
                        <th>Action</th>
                      </tr>
                    </thead>

                    <tbody>
                      {loading && (
                        <tr>
                          <td colSpan={3} className="text-center">
                            Loading...
                          </td>
                        </tr>
                      )}

                      {!loading && employeeCheckers.length === 0 && (
                        <tr>
                          <td colSpan={3} className="text-center">
                            No records found
                          </td>
                        </tr>
                      )}

                      {!loading &&
                        employeeCheckers.map((employee: EmployeeChecker) => (
                          <tr key={employee.id}>
                            <td>
                              {employee.employeeNavigation.lastName}, {employee.employeeNavigation.firstName} {employee.employeeNavigation.middleName}
                            </td>
                            <td>{employee.employeeNavigation.position}</td>
                            <td>
                              <button
                                className="btn btn-primary btn-sm"
                                onClick={() => {
                                  onSelect?.(employee);
                                  onClose();
                                }}
                              >
                                Select
                              </button>
                            </td>
                          </tr>
                        ))}
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </div>
    );
};

export default CheckerModal;
