using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.AppCode
{
    public class APIStatusCodes
    {
        public const string warning = "warning";
        public const string fail = "failed";
        public const string success = "success";
        public const string token_error = "Token key doesn't matched.";
        public const string invalid = "invalid";
        public const string token_not_found = "There are no permission key in our system software. please contact to your system admin.";
        public const string access_permission= "You need to access permission!";
        public const string error = "error";
    }
}