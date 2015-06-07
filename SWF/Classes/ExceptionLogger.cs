using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SWF.Classes
{
    public class ExceptionLogger
    {
        string errorContent, filePath;

        public ExceptionLogger(string argModuleName, string argMethodName, Exception objException)
        {
            errorContent = string.Empty;
            filePath = AppDomain.CurrentDomain.BaseDirectory + "Errors/ApplicationErrors.txt";

            errorContent += "*******************************************************************************************************************" + Environment.NewLine;
            errorContent += "                                          An error has occurred               " + Environment.NewLine;
            errorContent += "*******************************************************************************************************************" + Environment.NewLine;

            errorContent += " Date:             " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat) + Environment.NewLine;
            errorContent += " Module Name:      " + argModuleName + Environment.NewLine;
            errorContent += " Method Name:      " + argMethodName + Environment.NewLine;
            errorContent += " Error:            " + objException.Message + Environment.NewLine;
            errorContent += " Stack Trace:      " + objException.StackTrace + Environment.NewLine;

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(errorContent);
                writer.Close();
            }
        }
    }
}