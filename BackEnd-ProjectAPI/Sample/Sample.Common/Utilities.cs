using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Utilities
{
    /// <summary>
    /// Helper
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Get modified fields(value)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static T GetModifiedValues<T>(T origin, T update)
        {
            var updated = (T)origin;
            var typeProperty = origin.GetType().GetProperties();
            for (var i = 0; i < typeProperty.Count(); i++)
            {
                if (!typeProperty[i].GetMethod.IsVirtual && typeProperty[i].Name.ToLower() != "id" && typeProperty[i].Name.ToLower() != "created_by" && typeProperty[i].Name.ToLower() != "date_created"
                    && typeProperty[i].Name.ToLower() != "deleted" && typeProperty[i].Name.ToLower() != "deleted_by" && typeProperty[i].Name.ToLower() != "date_deleted")
                {
                    typeProperty[i].SetValue(origin,
                        typeProperty[i].GetValue(origin) != null ?
                        typeProperty[i].GetValue(origin).Equals(typeProperty[i].GetValue(update)) ?
                        typeProperty[i].GetValue(origin) : typeProperty[i].GetValue(update)
                        : typeProperty[i].GetValue(update));
                }
            }
            return updated;
        }

        public static string LongMonthDayYearFormat(this DateTime returnObj)
        {
            return returnObj.ToString("MMMM dd, yyyy");
        }
    }

    /// <summary>
    /// Constants
    /// </summary>
    //public static class Constant
    //{
    //    #region API
    //    //SapApi
    //    public const string SapApiGetEmployeeProfile = "/RESTAdapter/EAMS/EMPLPROF";
    //    public const string SapApiGetLeaveBalance = "/RESTAdapter/EAMS/LEAVEBAL";
    //    public const string SapApiGetHierarchy = "/RESTAdapter/EAMS/HIERARCHY";
    //    public const int SapIdMaxLength = 8;

    //    //EamsApi
    //    public const string EamsApiGetLeaveBalance = "/svc/eamsservices/getleavebalance";
    //    public const string EamsGetWeeklyScheduleDescription = "/svc/eamsservices/GetWeeklyScheduleDetails";
    //    #endregion

    //    public const string sync = "Sync";
    //    public const string success = "success";
    //    public const string failed = "failed";
    //    public const string hasDuplicate = "Has Duplicate";
    //    public const string inUse = "In Use";
    //    public const string DefaultAccessNumber = "00000";
    //    public const string EmployeeNumberHasDuplicate = "Employee Number has duplicate";
    //    public const int ItemsToGet = 100;


    //    #region Enumerables
    //    public enum AuthenticationResult
    //    {
    //        Success,
    //        Failed,
    //        InActive,
    //        NotFound,
    //        NotRegistered,
    //        ForRegistration,
    //        RegistryForApproval,
    //        RegistryDenied
    //    }

    //    public enum RegistrationStatus
    //    {
    //        ForApproval,
    //        Completed,
    //        Denied
    //    }

    //    public enum ValidateEmployeeNumberResult
    //    {
    //        ActiveDirectory,
    //        NonActiveDirectory,
    //        Registered,
    //        ForApproval,
    //        ActiveDirectoryNotMatch,
    //        ActiveDirectoryNotRegistered,
    //        InActive,
    //        SignatoryNotFound,
    //        SignatoryNotRegistered,
    //        NotFound
    //    }

    //    public enum TransferAccountResult
    //    {
    //        Success,
    //        Failed,
    //        InvalidEmployeeNumber,
    //        DataMismatched
    //    }

    //    public enum EmployeeStatus
    //    {
    //        Active = 1,
    //        InActive = 0
    //    }

    //    public enum ApiHttpVerb
    //    {
    //        GET,
    //        POST,
    //        PUT,
    //        DELETE
    //    }

    //    public enum Gender
    //    {
    //        Male = 1,
    //        Female = 2
    //    }

    //    public enum EmploymentStatus
    //    {
    //        Regular = 1,
    //        Probationary = 2,
    //        Contractual = 3,
    //        ContractualWAP = 4
    //    }

    //    public enum ApiConfig
    //    {
    //        Sap,
    //        Eams
    //    }

    //    public enum ApplicationAccessStatus
    //    {
    //        Approved,
    //        Denied,
    //        ForApproval
    //    }

    //    #endregion
    //}

    //public static class WebConfig
    //{
    //    public static int ApplicationId = int.Parse(ConfigurationManager.AppSettings["ApplicationId"]);
    //    public static int BuItRoleId = int.Parse(ConfigurationManager.AppSettings["BuItRoleId"]);
    //    public static int AdminRoleId = int.Parse(ConfigurationManager.AppSettings["AdminRoleId"]);
    //    public static int DefaultRoleId = int.Parse(ConfigurationManager.AppSettings["DefaultRoleId"]);
    //}
}