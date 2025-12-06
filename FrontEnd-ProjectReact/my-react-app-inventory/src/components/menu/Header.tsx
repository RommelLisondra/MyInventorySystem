import React from "react";
import { Link } from "react-router-dom";

const Header: React.FC = () => {
  return (
    <div className="header">
      <div className="header-left active">
        <a href="/" className="logo">
          <img src="/assets/img/logo.png" alt="" />
        </a>
        <a href="/" className="logo-small">
          <img src="/assets/img/logo-small.png" alt="" />
        </a>
        <a id="toggle_btn" href="#">
        </a>
      </div>

      <a id="mobile_btn" className="mobile_btn" href="#sidebar">
        <span className="bar-icon">
          <span></span><span></span><span></span>
        </span>
      </a>

      <ul className="nav user-menu">

        <li className="nav-item">
          <div className="top-nav-search">
            <form>
              <div className="searchinputs">
                <input type="text" placeholder="Search Here..." />
                <div className="search-addon">
                  <span>
                    <img src="/assets/img/icons/closes.svg" alt="" />
                  </span>
                </div>
              </div>
              <button className="btn" id="searchdiv">
                <img src="/assets/img/icons/search.svg" alt="" />
              </button>
            </form>
          </div>
        </li>

        <li className="nav-item dropdown has-arrow flag-nav">
          <a className="nav-link dropdown-toggle" data-bs-toggle="dropdown">
            <img src="/assets/img/flags/us1.png" alt="" height="20" />
          </a>
          <div className="dropdown-menu dropdown-menu-right">
            <a className="dropdown-item"><img src="/assets/img/flags/us.png" height="16" /> English</a>
            <a className="dropdown-item"><img src="/assets/img/flags/fr.png" height="16" /> French</a>
            <a className="dropdown-item"><img src="/assets/img/flags/es.png" height="16" /> Spanish</a>
            <a className="dropdown-item"><img src="/assets/img/flags/de.png" height="16" /> German</a>
          </div>
        </li>

        <li className="nav-item dropdown">
          <a className="dropdown-toggle nav-link" data-bs-toggle="dropdown">
            <img src="/assets/img/icons/notification-bing.svg" alt="" />
            <span className="badge rounded-pill">4</span>
          </a>
          <div className="dropdown-menu notifications">
            <div className="topnav-dropdown-header">
              <span className="notification-title">Notifications</span>
              <a className="clear-noti">Clear All</a>
            </div>
            <div className="noti-content">
              <ul className="notification-list">
                <li className="notification-message">
                  <a>
                    <div className="media d-flex">
                      <span className="avatar flex-shrink-0">
                        <img src="/assets/img/profiles/avatar-02.jpg" alt="" />
                      </span>
                      <div className="media-body flex-grow-1">
                        <p><span>John Doe</span> added new task</p>
                        <p className="noti-time">4 mins ago</p>
                      </div>
                    </div>
                  </a>
                </li>
              </ul>
            </div>
            <div className="topnav-dropdown-footer">
              <a>View all Notifications</a>
            </div>
          </div>
        </li>

        <li className="nav-item dropdown has-arrow main-drop">
          <a className="dropdown-toggle nav-link userset" data-bs-toggle="dropdown">
            <span className="user-img">
              <img src="/assets/img/profiles/avator1.jpg" alt="" />
            </span>
          </a>
          <div className="dropdown-menu menu-drop-user">
            <div className="profilename">
              <div className="profileset">
                <span className="user-img">
                  <img src="/assets/img/profiles/avator1.jpg" alt="" />
                </span>
                <div className="profilesets">
                  <h6>John Doe</h6>
                  <h5>Admin</h5>
                </div>
              </div>
              <hr className="m-0" />
              <Link className="dropdown-item" to="/profile">
                <i className="me-2" data-feather="user"></i>
                My Profile
              </Link>

              <Link className="dropdown-item" to="/settings">
                <i className="me-2" data-feather="settings"></i>
                Settings
              </Link>

              <hr className="m-0" />

              <Link className="dropdown-item logout pb-0" to="/login">
                <img
                  src="/assets/img/icons/log-out.svg"
                  className="me-2"
                  alt="logout"
                />
                Logout
              </Link>
            </div>
          </div>
        </li>

      </ul>
    </div>
  );
};

export default Header;
