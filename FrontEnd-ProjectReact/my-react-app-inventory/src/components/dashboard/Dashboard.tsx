
import { ROUTES } from "../../constants/routes";
import { useNavigate } from "react-router-dom";

const Dashboard = () => {
  // Sample data for recently added products
  const recentProducts = [
    {
      id: 1,
      name: "Apple Earpods",
      price: "$891.2",
      image: "/assets/img/product/product22.jpg"
    },
    {
      id: 2,
      name: "iPhone 11",
      price: "$668.51",
      image: "/assets/img/product/product23.jpg"
    },
    {
      id: 3,
      name: "samsung",
      price: "$522.29",
      image: "/assets/img/product/product24.jpg"
    },
    {
      id: 4,
      name: "Macbook Pro",
      price: "$291.01",
      image: "/assets/img/product/product6.jpg"
    }
  ];

  // Sample data for expired products
  const expiredProducts = [
    {
      id: 1,
      code: "IT0001",
      name: "Orange",
      brand: "N/D",
      category: "Fruits",
      expiryDate: "12-12-2022",
      image: "/assets/img/product/product2.jpg"
    },
    {
      id: 2,
      code: "IT0002",
      name: "Pineapple",
      brand: "N/D",
      category: "Fruits",
      expiryDate: "25-11-2022",
      image: "/assets/img/product/product3.jpg"
    },
    {
      id: 3,
      code: "IT0003",
      name: "Stawberry",
      brand: "N/D",
      category: "Fruits",
      expiryDate: "19-11-2022",
      image: "/assets/img/product/product4.jpg"
    },
    {
      id: 4,
      code: "IT0004",
      name: "Avocat",
      brand: "N/D",
      category: "Fruits",
      expiryDate: "20-11-2022",
      image: "/assets/img/product/product5.jpg"
    }
  ];

  const navigate = useNavigate();

  return (
    <div>
      <div className="content">
        <div className="row">
          <div className="col-lg-3 col-sm-6 col-12">
            <div className="dash-widget">
              <div className="dash-widgetimg">
                <span>
                  <img src="/assets/img/icons/dash1.svg" alt="img" />
                </span>
              </div>
              <div className="dash-widgetcontent">
                <h5>
                  $<span className="counters" data-count="307144.00">
                    $307,144.00
                  </span>
                </h5>
                <h6>Total Purchase Due</h6>
              </div>
            </div>
          </div>

          {/* Total Sales Due */}
          <div className="col-lg-3 col-sm-6 col-12">
            <div className="dash-widget dash1">
              <div className="dash-widgetimg">
                <span>
                  <img src="/assets/img/icons/dash2.svg" alt="img" />
                </span>
              </div>
              <div className="dash-widgetcontent">
                <h5>
                  $<span className="counters" data-count="4385.00">
                    $4,385.00
                  </span>
                </h5>
                <h6>Total Sales Due</h6>
              </div>
            </div>
          </div>

          {/* Total Sale Amount */}
          <div className="col-lg-3 col-sm-6 col-12">
            <div className="dash-widget dash2">
              <div className="dash-widgetimg">
                <span>
                  <img src="/assets/img/icons/dash3.svg" alt="img" />
                </span>
              </div>
              <div className="dash-widgetcontent">
                <h5>
                  $<span className="counters" data-count="385656.50">
                    385,656.50
                  </span>
                </h5>
                <h6>Total Sale Amount</h6>
              </div>
            </div>
          </div>

          {/* Another Total Sale Amount */}
          <div className="col-lg-3 col-sm-6 col-12">
            <div className="dash-widget dash3">
              <div className="dash-widgetimg">
                <span>
                  <img src="/assets/img/icons/dash4.svg" alt="img" />
                </span>
              </div>
              <div className="dash-widgetcontent">
                <h5>
                  $<span className="counters" data-count="40000.00">
                    400.00
                  </span>
                </h5>
                <h6>Total Sale Amount</h6>
              </div>
            </div>
          </div>

          {/* Count Widgets */}
          <div className="col-lg-3 col-sm-6 col-12 d-flex">
            <div className="dash-count">
              <div className="dash-counts">
                <h4>100</h4>
                <h5>Customers</h5>
              </div>
              <div className="dash-imgs">
                <i data-feather="user"></i>
              </div>
            </div>
          </div>

          <div className="col-lg-3 col-sm-6 col-12 d-flex">
            <div className="dash-count das1">
              <div className="dash-counts">
                <h4>100</h4>
                <h5>Suppliers</h5>
              </div>
              <div className="dash-imgs">
                <i data-feather="user-check"></i>
              </div>
            </div>
          </div>

          <div className="col-lg-3 col-sm-6 col-12 d-flex">
            <div className="dash-count das2">
              <div className="dash-counts">
                <h4>100</h4>
                <h5>Purchase Invoice</h5>
              </div>
              <div className="dash-imgs">
                <i data-feather="file-text"></i>
              </div>
            </div>
          </div>

          <div className="col-lg-3 col-sm-6 col-12 d-flex">
            <div className="dash-count das3">
              <div className="dash-counts">
                <h4>105</h4>
                <h5>Sales Invoice</h5>
              </div>
              <div className="dash-imgs">
                <i data-feather="file"></i>
              </div>
            </div>
          </div>
        </div>

        {/* Charts and Recent Products Row */}
        <div className="row">
          {/* Purchase & Sales Chart */}
          <div className="col-lg-7 col-sm-12 col-12 d-flex">
            <div className="card flex-fill">
              <div className="card-header pb-0 d-flex justify-content-between align-items-center">
                <h5 className="card-title mb-0">Purchase & Sales</h5>
                <div className="graph-sets">
                  <ul>
                    <li>
                      <span>Sales</span>
                    </li>
                    <li>
                      <span>Purchase</span>
                    </li>
                  </ul>
                  <div className="dropdown">
                    <button
                      className="btn btn-white btn-sm dropdown-toggle"
                      type="button"
                      id="dropdownMenuButton"
                      data-bs-toggle="dropdown"
                      aria-expanded="false"
                    >
                      2022 <img src="/assets/img/icons/dropdown.svg" alt="img" className="ms-2" />
                    </button>
                    <ul className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                      <li>
                        <a href="javascript:void(0);" className="dropdown-item">
                          2022
                        </a>
                      </li>
                      <li>
                        <a href="javascript:void(0);" className="dropdown-item">
                          2021
                        </a>
                      </li>
                      <li>
                        <a href="javascript:void(0);" className="dropdown-item">
                          2020
                        </a>
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
              <div className="card-body">
                <div id="sales_charts"></div>
              </div>
            </div>
          </div>

          {/* Recently Added Products */}
          <div className="col-lg-5 col-sm-12 col-12 d-flex">
            <div className="card flex-fill">
              <div className="card-header pb-0 d-flex justify-content-between align-items-center">
                <h4 className="card-title mb-0">Recently Added Products</h4>
                <div className="dropdown">
                  <a
                    href="javascript:void(0);"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                    className="dropset"
                  >
                    <i className="fa fa-ellipsis-v"></i>
                  </a>
                  <ul className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <li>
                        <a
                            href="#"
                            className="dropdown-item"
                            onClick={(e) => {
                              e.preventDefault();
                              navigate(ROUTES.ITEM_LIST); 
                          }}
                        >
                            Product List
                        </a>
                    </li>
                    <li>
                      {/* <a href="addproduct.html" className="dropdown-item">
                        Product Add
                      </a> */}
                      <a
                            href="#"
                            className="dropdown-item"
                            onClick={(e) => {
                              e.preventDefault();
                              navigate(ROUTES.ITEM_CREATE); 
                          }}
                        >
                            Product Add
                        </a>
                    </li>
                  </ul>
                </div>
              </div>
              <div className="card-body">
                <div className="table-responsive dataview">
                  <table className="table datatable">
                    <thead>
                      <tr>
                        <th>Sno</th>
                        <th>Products</th>
                        <th>Price</th>
                      </tr>
                    </thead>
                    <tbody>
                      {recentProducts.map((product) => (
                        <tr key={product.id}>
                          <td>{product.id}</td>
                          <td className="productimgname">
                            <a href="productlist.html" className="product-img">
                              <img src={product.image} alt="product" />
                            </a>
                            <a href="productlist.html">{product.name}</a>
                          </td>
                          <td>{product.price}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* Expired Products Table */}
        <div className="card mb-0">
          <div className="card-body">
            <h4 className="card-title">Expired Products</h4>
            <div className="table-responsive dataview">
              <table className="table datatable">
                <thead>
                  <tr>
                    <th>SNo</th>
                    <th>Product Code</th>
                    <th>Product Name</th>
                    <th>Brand Name</th>
                    <th>Category Name</th>
                    <th>Expiry Date</th>
                  </tr>
                </thead>
                <tbody>
                  {expiredProducts.map((product) => (
                    <tr key={product.id}>
                      <td>{product.id}</td>
                      <td>
                        <a href="javascript:void(0);">{product.code}</a>
                      </td>
                      <td className="productimgname">
                        <a className="product-img" href="productlist.html">
                          <img src={product.image} alt="product" />
                        </a>
                        <a href="productlist.html">{product.name}</a>
                      </td>
                      <td>{product.brand}</td>
                      <td>{product.category}</td>
                      <td>{product.expiryDate}</td>
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

export default Dashboard;