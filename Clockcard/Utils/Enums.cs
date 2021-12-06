using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Clockcard.Utils
{
    public class Enums : PageModel
    {
        public enum Active   // Enum for ISACTIVE 
        {
            No,
            Yes,
        }
        public enum EmployeeRole // Enum for ROLE
        {
            None,
            Employee,
            Manager,
            Admin
        }
        public static string getSessionValues(string key, HttpContext context)  // Code to Limit the Employees shown 
        {
            var keySession = new Byte[20];

            bool keyExist = context.Session.TryGetValue(key, out keySession);
            string value = "";
            if (keyExist)
            {
                value = System.Text.Encoding.UTF8.GetString(keySession);

            }
            return value;
        }

    }

}
