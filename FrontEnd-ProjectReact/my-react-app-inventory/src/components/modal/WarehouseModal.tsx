import React, { useState } from "react";
import { useWarehouse } from "../../hooks/useWarehouse";
import type { Warehouse } from "../../types/warehouse";

interface Props {
  show: boolean;
  onClose: () => void;
  onSelect: (Warehouse: Warehouse) => void;   // UPDATED
}

const WarehouseModal: React.FC<Props> = ({ show, onClose, onSelect }) => {
    const { warehouses, loading, searchWarehouse } = useWarehouse();
    const [keyword, setKeyword] = useState("");

    const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
      const value = e.target.value;
      setKeyword(value);
      searchWarehouse(value);
    };

    return (
        <div
            className={`modal fade ${show ? "show d-block" : ""}`}
            tabIndex={-1}
            aria-labelledby="WarehouseSearchModal"
            aria-hidden={!show}
            style={{ background: show ? "rgba(0,0,0,.5)" : "transparent" }}
          >
          <div className="modal-dialog modal-lg modal-dialog-centered">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Search Warehouse</h5>
                <button className="btn-close" onClick={onClose}>x</button>
              </div>

              <div className="modal-body">
                <div className="mb-3">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Search warehouse..."
                    value={keyword}
                    onChange={handleSearch}
                  />
                </div>

                <div className="table-responsive" style={{ maxHeight: "350px" }}>
                  <table className="table table-bordered">
                    <thead>
                      <tr>
                        <th>Warehouse Code</th>
                        <th>Name</th>
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

                      {!loading && warehouses.length === 0 && (
                        <tr>
                          <td colSpan={3} className="text-center">
                            No records found
                          </td>
                        </tr>
                      )}

                      {!loading &&
                        warehouses.map((warehouse: Warehouse) => (
                          <tr key={warehouse.id}>
                            <td>
                              {warehouse.wareHouseCode}
                            </td>
                            <td>{warehouse.name}</td>
                            <td>
                              <button
                                className="btn btn-primary btn-sm"
                                onClick={() => {
                                  onSelect?.(warehouse);
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

              {/* <div className="modal-footer">
                <button className="btn btn-secondary" onClick={onClose}>
                  Close
                </button>
              </div> */}

            </div>
          </div>
        </div>
    );
};

export default WarehouseModal;
