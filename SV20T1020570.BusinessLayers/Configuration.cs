using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020570.BusinessLayers
{
    public static class Configuration 
    {
        /// <summary>
        /// Chuỗi kết thông số kết nối đến CSDL
        /// </summary>
        public static string ConnectString { get; private set; } = "";
        /// <summary>
        /// Khởi tạo cấu hình cho BusinessLayer
        /// (Hàm này phải được gọi trước khi ứng dụng chạy)
        /// </summary>
        /// <param name="connectString"></param>
        public static void Initialize(string connectString)
        {
            Configuration.ConnectString = connectString;
        }
    }
}

// static class là gì ? khác với class thường điểm gì ?
