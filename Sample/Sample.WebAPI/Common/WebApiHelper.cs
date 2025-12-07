using Microsoft.AspNetCore.Http;
using Sample.WebAPI.Model;
using System;
using System.Linq;

namespace Sample.WebAPI.Common
{
    public static class WebApiHelper
    {
        public static string GetEmployeeNumber(IHeaderDictionary headers)
        {
            if (headers.TryGetValue(WebApiConstants.EmployeeNumber, out var values))
            {
                return values.FirstOrDefault() ?? string.Empty;
            }
            else
            {
                throw new Exception(WebApiConstants.NoEmployeeNumberHeader);
            }
        }

        public static string GetToken(IHeaderDictionary headers)
        {
            if (headers.TryGetValue(WebApiConstants.token, out var values))
            {
                return values.FirstOrDefault() ?? string.Empty;
            }
            else
            {
                throw new Exception(WebApiConstants.NoTokenHeader);
            }
        }

        public static LogStatus BuildMobileException(Exception ex)
        {
            var returnObj = new LogStatus();

            switch (ex.Message)
            {
                case WebApiConstants.SessionExpired:
                    returnObj.status = (int)WebApiConstants.MobileStatusCodes.TokenExpired;
                    returnObj.message = ex.Message;
                    break;

                case WebApiConstants.NoEmployeeNumberHeader:
                case WebApiConstants.NoTokenHeader:
                    returnObj.status = (int)WebApiConstants.MobileStatusCodes.ServerError;
                    returnObj.message = ex.Message;
                    break;

                default:
                    returnObj.status = (int)WebApiConstants.MobileStatusCodes.ServerError;
                    returnObj.message = WebApiConstants.SomethingWentWrong;
                    break;
            }

            return returnObj;
        }
    }
}