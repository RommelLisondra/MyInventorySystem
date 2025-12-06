import React, { useState } from "react";
import { useItemDetail } from "../../hooks/useItemDetail";
import type { ItemDetail } from "../../types/itemDetail";

interface Props {
  show: boolean;
  onClose: () => void;
  onSelect: (item: ItemDetail) => void;   // UPDATED
}

const ItemModal: React.FC<Props> = ({ show, onClose, onSelect }) => {
    const { itemDetails, loading, searchItemDetail } = useItemDetail();
    const [keyword, setKeyword] = useState("");

    const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
      const value = e.target.value;
      setKeyword(value);
      searchItemDetail(value);
    };

    return (
        <div
            className={`modal fade ${show ? "show d-block" : ""}`}
            tabIndex={-1}
            aria-labelledby="itemSearchModal"
            aria-hidden={!show}
            style={{ background: show ? "rgba(0,0,0,.5)" : "transparent" }}
          >
          <div className="modal-dialog modal-lg modal-dialog-centered">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Search Item</h5>
                <button className="btn-close" onClick={onClose}>x</button>
              </div>

              <div className="modal-body">
                <div className="mb-3">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Search item..."
                    value={keyword}
                    onChange={handleSearch}
                  />
                </div>

                <div className="table-responsive" style={{ maxHeight: "350px" }}>
                  <table className="table table-bordered">
                    <thead>
                      <tr>
                        <th>Brand Name</th>
                        <th>Quantity</th>
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

                      {!loading && itemDetails.length === 0 && (
                        <tr>
                          <td colSpan={3} className="text-center">
                            No records found
                          </td>
                        </tr>
                      )}

                      {!loading &&
                        itemDetails.map((item: ItemDetail) => (
                          <tr key={item.id}>
                            <td>
                              {item.Item?.ItemName}
                            </td>
                            <td>{item.Unitprice}</td>
                            <td>
                              <button
                                className="btn btn-primary btn-sm"
                                onClick={() => {
                                  onSelect?.(item);
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

export default ItemModal;
