import React, { useState, useMemo } from "react";
import { useNavigate } from "react-router-dom";
import type { Warehouse } from "../../types/warehouse";
import { ROUTES } from "../../constants/routes";

interface Props {
    warehouse: Warehouse[];
    onDelete: (id: number) => void;
    onEdit: (id: number) => void;
    onUpdate: (warehouse: Warehouse) => void;
    onReload: () => void;
    onSearch?: (query: string) => void;
}

const WarehouseList: React.FC<Props> = ({warehouse,onDelete,onEdit,onReload,onSearch}) => {
    const [searchTerm, setSearchTerm] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(10);

    const navigate = useNavigate();

    // filter customers by search term
    const filteredWarehouse = useMemo(() => {
      if (!searchTerm) return warehouse;
      return warehouse.filter((c) =>
        c.name.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }, [warehouse, searchTerm]);

    // pagination calculations
    const totalPages = Math.ceil(filteredWarehouse.length / rowsPerPage);
    const currentWarehouse = filteredWarehouse.slice(
      (currentPage - 1) * rowsPerPage,
      currentPage * rowsPerPage
    );

    const handleSearch = () => {
      if (onSearch) onSearch(searchTerm.trim());
      setCurrentPage(1); // reset page
    };

    const goToPage = (page: number) => {
      if (page < 1 || page > totalPages) return;
      setCurrentPage(page);
    };

    return (
        <div className="content">
          <div className="page-header">
            <div className="page-title">
              <h4>Warehouse List</h4>
              <h6>Manage your Warehouse</h6>
            </div>
            <div className="page-btn d-flex gap-2">
              <a
                href="#"
                className="btn btn-added d-flex align-items-center"
                onClick={(e) => {
                  e.preventDefault();
                  navigate(ROUTES.WAREHOUSE_CREATE);
                }}
              >
                <img src="/assets/img/icons/plus.svg" alt="add" className="me-1" />
                Add Warehouse
              </a>
              <a
                href="#"
                className="btn btn-added d-flex align-items-center"
                onClick={(e) => {
                  e.preventDefault();
                  onReload();
                }}
              >
                <img src="/assets/img/icons/reverse.svg" alt="reload" className="me-1" />
                Reload
              </a>
            </div>
          </div>
          <div className="card">
            <div className="card-body">
              <div className="table-top d-flex justify-content-between mb-3">
                <div className="search-input d-flex">
                  <input
                    type="text"
                    className="form-control form-control-sm me-2"
                    placeholder="Search warehouse..."
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    onKeyDown={(e) => e.key === "Enter" && handleSearch()}
                  />
                  <button className="btn btn-primary btn-sm" onClick={handleSearch}>
                    Search
                  </button>
                </div>

                <div className="d-flex align-items-center gap-2">
                  <label className="mb-0">Show per page:</label>
                  <select
                    className="form-select form-select-sm"
                    style={{ width: "80px" }}
                    value={rowsPerPage}
                    onChange={(e) => {
                      setRowsPerPage(Number(e.target.value));
                      setCurrentPage(1); 
                    }}
                  >
                    {[5, 10, 20, 50].map((num) => (
                      <option key={num} value={num}>
                        {num}
                      </option>
                    ))}
                  </select>
                </div>
              </div>

              {/* TABLE */}
              <div className="table-responsive">
                <table className="table table-bordered table-striped datanew">
                  <thead>
                    <tr>
                      <th>Code</th>
                      <th>Customer Name</th>
                      <th>modifiedDateTime</th>
                      <th>createdDateTime</th>
                      <th>Status</th>
                      <th style={{ width: "150px" }}>Action</th>
                    </tr>
                  </thead>
                  <tbody>
                    {currentWarehouse.length > 0 ? (
                      currentWarehouse.map((c) => (
                        <tr key={c.id}>
                          <td className="productimgname">
                            <a className="product-img">
                              <img src="/assets/img/customer/customer1.jpg" alt="img" />
                            </a>
                            <a>{c.wareHouseCode}</a>
                          </td>
                          <td>{c.name}</td>
                          <td>{c.modifiedDateTime instanceof Date ? c.modifiedDateTime.toLocaleString() : c.modifiedDateTime}</td>
                          <td>{c.createdDateTime instanceof Date ? c.createdDateTime.toLocaleString() : c.createdDateTime}</td>
                          <td>{c.status}</td>
                          <td>
                            <button
                              className="btn btn-warning btn-sm me-2"
                              onClick={() => onEdit(c.id)}
                            >
                              ✏️
                            </button>
                            <button
                              className="btn btn-danger btn-sm"
                              onClick={() => onDelete(c.id)}
                            >
                              🗑️
                            </button>
                          </td>
                        </tr>
                      ))
                    ) : (
                      <tr>
                        <td colSpan={7} className="text-center py-3 text-muted">
                          No Records Found
                        </td>
                      </tr>
                    )}
                  </tbody>
                </table>
              </div>
                {/* PAGINATION */}
                {totalPages > 1 && (
                    <div className="d-flex justify-content-between align-items-center mt-3">
                        <div>
                            Page {currentPage} of {totalPages}
                        </div>
                        <div className="d-flex align-items-center gap-1">
                        <button
                            className="btn btn-sm btn-outline-primary"
                            onClick={() => goToPage(currentPage - 1)}
                            disabled={currentPage === 1}
                        >
                            «
                        </button>
                        {currentPage > 3 && (
                            <>
                            <button
                                className={`btn btn-sm ${
                                currentPage === 1 ? "btn-primary" : "btn-outline-primary"
                                }`}
                                onClick={() => goToPage(1)}
                            >
                                1
                            </button>
                            <span className="mx-1">...</span>
                            </>
                        )}
                        {Array.from({ length: totalPages }, (_, i) => i + 1)
                            .filter(
                            (page) =>
                                page === currentPage ||
                                page === currentPage - 1 ||
                                page === currentPage + 1
                            )
                            .map((page) => (
                                <button
                                    key={page}
                                    className={`btn btn-sm ${
                                    page === currentPage ? "btn-primary" : "btn-outline-primary"
                                    }`}
                                    onClick={() => goToPage(page)}
                                >
                                    {page}
                                </button>
                            ))}
                            {currentPage < totalPages - 2 && (
                                <>
                                <span className="mx-1">...</span>
                                <button
                                    className={`btn btn-sm ${
                                    currentPage === totalPages
                                        ? "btn-primary"
                                        : "btn-outline-primary"
                                    }`}
                                    onClick={() => goToPage(totalPages)}
                                >
                                    {totalPages}
                                </button>
                                </>
                            )}
                            <button
                                className="btn btn-sm btn-outline-primary"
                                onClick={() => goToPage(currentPage + 1)}
                                disabled={currentPage === totalPages}
                            >
                                »
                            </button>
                        </div>
                    </div>
                )}
            </div>
          </div>
        </div>
    );
};

export default WarehouseList;
