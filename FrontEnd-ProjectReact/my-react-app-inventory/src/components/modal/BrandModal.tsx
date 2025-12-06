import React, { useState } from "react";
import { useBrand } from "../../hooks/useBrand";
import type { Brand } from "../../types/brand";

interface Props {
  show: boolean;
  onClose: () => void;
  onSelect: (brand: Brand) => void;   // UPDATED
}

const BrandModal: React.FC<Props> = ({ show, onClose, onSelect }) => {
    const { brands, loading, searchBrand } = useBrand();
    const [keyword, setKeyword] = useState("");

    const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
      const value = e.target.value;
      setKeyword(value);
      searchBrand(value);
    };

    return (
        <div
            className={`modal fade ${show ? "show d-block" : ""}`}
            tabIndex={-1}
            aria-labelledby="BrandSearchModal"
            aria-hidden={!show}
            style={{ background: show ? "rgba(0,0,0,.5)" : "transparent" }}
          >
          <div className="modal-dialog modal-lg modal-dialog-centered">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Search Brand</h5>
                <button className="btn-close" onClick={onClose}>x</button>
              </div>

              <div className="modal-body">
                <div className="mb-3">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Search brand..."
                    value={keyword}
                    onChange={handleSearch}
                  />
                </div>

                <div className="table-responsive" style={{ maxHeight: "350px" }}>
                  <table className="table table-bordered">
                    <thead>
                      <tr>
                        <th>Brand Code</th>
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

                      {!loading && brands.length === 0 && (
                        <tr>
                          <td colSpan={3} className="text-center">
                            No records found
                          </td>
                        </tr>
                      )}

                      {!loading &&
                        brands.map((brand: Brand) => (
                          <tr key={brand.id}>
                            <td>
                              {brand.id}
                            </td>
                            <td>{brand.brandName}</td>
                            <td>
                              <button
                                className="btn btn-primary btn-sm"
                                onClick={() => {
                                  onSelect?.(brand);
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

export default BrandModal;
