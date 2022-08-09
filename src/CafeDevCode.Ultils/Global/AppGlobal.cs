using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Ultils.Global
{
    public class AppGlobal
    {
        public static DateTime SysDateTime => DateTime.Now;

        public static string InvalidUserName => "Tên đăng nhập không hợp lệ";
        public static string InvalidPassWord => "Mật khẩu không hợp lệ";
        public static string DefaultSuccessMessage => "Thao tác thành công";
    }
}
