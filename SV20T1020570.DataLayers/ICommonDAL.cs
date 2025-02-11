﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020570.DataLayers
{
    /// <summary>
    /// Mô tả các phép xử lý dữ liệu chung
    /// </summary>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page"> Trang cần hiển thị </param>
        /// <param name="pageSize"> Số dòng dữ liệu trên mỗi trang (bằng 0 nếu không phân trang) </param>
        /// <param name="searchValue"> Giá trị cần tìm kiếm (chuỗi rỗng nếu lấy toàn bộ dữ liệu) </param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, string searchValue = "");

        /// <summary>
        /// Đếm số dòng dữ liệu tìm được
        /// </summary>
        /// <param name="serchValue"> Giá trị cần tìm kiếm (chuỗi rỗng nếu lấy toàn bộ dữ liệu) </param>
        /// <returns></returns>
        int Count(string searchValue = "");

        /// <summary>
        /// Bổ sung dữ liệu vào csdl. Hàm trả về ID của dữ liệu bổ sung 
        /// (trả về giá trị 0 nếu việc bổ sung không thành công
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(T data);

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);

        /// <summary>
        /// Xóa dữ liệu dựa trên ID
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// Lẫy bảng ghi dữ liệu dựa vào Id (trả về null nếu dữ liệu không tồn tại)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T? Get(int id);

        /// <summary>
        /// Kiểm trả xem bản ghi dữ liệu có mã id hiện đang có được sử dụng bởi các dữ liệu khác hay không
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsUsed(int id);
    }
}
