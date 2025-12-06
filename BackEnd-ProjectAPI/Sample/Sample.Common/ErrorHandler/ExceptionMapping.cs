using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sample.Common.ErrorHandler
{
    public static class ExceptionMapping
    {
        public static CustomException Map(Exception error)
        {
            var customException = new CustomException();
            var serverMessage = error.InnerException?.ToString() ?? error.Message;
            var clientMessage = string.Empty;
            var defaultError = true;

            // Load error definitions safely
            var errorDefinitions = LoadErrorDefinitions();
            if (errorDefinitions == null || errorDefinitions.Count == 0)
            {
                // If JSON file missing or invalid
                customException.Message = "An unexpected error occurred.";
                customException.Default = true;
                customException.ExceptionHandled = true;
                return customException;
            }

            // Find matching error
            foreach (var definition in errorDefinitions)
            {
                var serverErrorContains = definition["ServerErrorContains"]?.ToString();
                var message = definition["Message"]?.ToString();

                if (string.IsNullOrWhiteSpace(serverErrorContains))
                    continue;

                if (serverErrorContains == "ErrorNotFound")
                {
                    clientMessage = message;
                    continue;
                }

                if (!string.IsNullOrEmpty(serverMessage) && serverMessage.Contains(serverErrorContains, StringComparison.OrdinalIgnoreCase))
                {
                    clientMessage = message;
                    defaultError = false;
                    break;
                }
            }

            customException.Message = clientMessage ?? "An unexpected error occurred.";
            customException.Default = defaultError;
            customException.ExceptionHandled = true;

            return customException;
        }

        private static List<JObject> LoadErrorDefinitions()
        {
            try
            {
                // Path resolution compatible with .NET Core / .NET 6+
                var baseDir = AppContext.BaseDirectory;
                var jsonPath = Path.Combine(baseDir, "ErrorHandler", "errordata.json");

                if (!File.Exists(jsonPath))
                    return new List<JObject>();

                var json = File.ReadAllText(jsonPath);
                var root = JObject.Parse(json);

                // Collect all array values from JSON structure
                return root.Properties()
                           .SelectMany(p => (p.Value as JArray)?.OfType<JObject>() ?? Enumerable.Empty<JObject>())
                           .ToList();
            }
            catch
            {
                return new List<JObject>();
            }
        }
    }
}