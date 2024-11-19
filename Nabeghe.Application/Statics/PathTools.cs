using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.Statics
{
   public class PathTools
    {
        #region Course

        public static string CourseImagePath => "/Content/Images/Course/";
        public static string CourseFilePath => "/Content/Files/Course/";

        #endregion

        #region User

        public static string UserAvatarPath => "wwwroot/Avatars";
        public static string GetAvatarPath => "/Avatars/";

        #endregion

        #region Blog

        public static string BlogImagePath => "/Content/Images/Blog/";

        #endregion
    }
}
