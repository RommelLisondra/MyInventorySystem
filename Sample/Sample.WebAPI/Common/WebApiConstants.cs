namespace Sample.WebAPI.Common
{
    public static class WebApiConstants
    {
        public const string SessionExpired = "Session expired please login again.";
        public const string NoEmployeeNumberHeader = "No user_id in Header";
        public const string NoTokenHeader = "No token in Header";
        public const string EmployeeNumber = "employeeNumber";
        public const string token = "token";
        public const string SuccessfullyUploaded = "Your Profile has been successfully updated.";
        public const string InvalidBase64String = "Invalid Base64 String or File Type";
        public const string SomethingWentWrong = "Something went wrong. Please try again later.";
        public const string InvalidUserNamePassword = "Invalid User or Password";
        public enum MobileStatusCodes
        {
            Success = 100
            , InvalidUserNamePassword = 200
            , Inactive = 300
            , TokenExpired = 401
            , Unauthorized = 402
            , ServerError = 500
            , SapIntegrationError = 501
        }
    }
}