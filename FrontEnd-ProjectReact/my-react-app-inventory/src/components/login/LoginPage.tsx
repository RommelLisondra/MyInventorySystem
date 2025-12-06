import { useNavigate } from "react-router-dom";

const LoginPage = () => {
  const navigate = useNavigate();

  const handleLogin = () => {
    // In real system → Validate API here
    navigate("/app/dashboard");
  };

  return (
    <div className="main-wrapper account-page">
      <div className="account-content">
        <div className="login-wrapper">
          <div className="login-content">
            <div className="login-userset">
              <div className="login-logo">
                <img src="/assets/img/logo.png" alt="img" />
              </div>

              <div className="login-userheading">
                <h3>Sign In</h3>
                <h4>Please login to your account</h4>
              </div>

              <div className="form-login">
                <label>Email</label>
                <div className="form-addons">
                  <input type="text" placeholder="Enter your email address" />
                  <img src="/assets/img/icons/mail.svg" alt="mail" />
                </div>
              </div>

              <div className="form-login">
                <label>Password</label>
                <div className="pass-group">
                  <input type="password" className="pass-input" placeholder="Enter your password" />
                  <span className="fas toggle-password fa-eye-slash"></span>
                </div>
              </div>

              <div className="form-login">
                <div className="alreadyuser">
                  <h4>
                    <a href="#" className="hover-a">Forgot Password?</a>
                  </h4>
                </div>
              </div>

              <div className="form-login">
                <button className="btn btn-login w-100" onClick={handleLogin}>
                  Sign In
                </button>
              </div>

              <div className="signinform text-center">
                <h4>
                  Don’t have an account?{" "}
                  <a href="#" className="hover-a">Sign Up</a>
                </h4>
              </div>

              <div className="form-setlogin">
                <h4>Or sign up with</h4>
              </div>

              <div className="form-sociallink">
                <ul>
                  <li>
                    <a href="#">
                      <img src="/assets/img/icons/google.png" className="me-2" alt="google" />
                      Sign Up using Google
                    </a>
                  </li>
                  <li>
                    <a href="#">
                      <img src="/assets/img/icons/facebook.png" className="me-2" alt="fb" />
                      Sign Up using Facebook
                    </a>
                  </li>
                </ul>
              </div>

            </div>
          </div>

          <div className="login-img">
            <img src="/assets/img/login.jpg" alt="img" />
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
