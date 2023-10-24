using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontApp
{
    public class GlobalProperties
    {
        public static string UserToken { get; set; }
        public static string UserLogin { get; set; }
        public static bool IsLogged { get; set; }
        public static double SesionExpiration { get; set; }
        public static string ApiUri { get; set; }
        public static Dictionary<string, object> PassParameters { get; set; } = new();

    }
}
