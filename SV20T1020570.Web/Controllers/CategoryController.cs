using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV20T1020570.BusinessLayers;
using SV20T1020570.DomainModels;
using SV20T1020570.Web.Models;

namespace SV20T1020570.Web.Controllers
{
    [Authorize(Roles = $"{WebUserRoles.Adminstrator},{WebUserRoles.Employee}")]

    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 10;
        private const string CATEGORY_SEARCH = "category_search"; // tên biến để lưu trong session

        public IActionResult Index()
        {
            // Lấy đầu vào tìm kiếm hiện đang lưu lại trong session
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(CATEGORY_SEARCH);

            // Trưởng hợp trong session chưa có điều kiện thì tạo điều kiện mới
            if (input == null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }


            return View(input);
        }
        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new CategorySearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };

            //Lưu lại điều kiện tìm kiếm vào trong session
            ApplicationContext.SetSessionData(CATEGORY_SEARCH, input);

            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung loại hàng";
            Category model = new Category()
            {
                CategoryId = 0
            };
            return View("Edit", model);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin loại hàng";
            Category? model = CommonDataService.GetCategory(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        [HttpPost]
        public IActionResult Save(Category data, IFormFile? uploadPhoto)
        {
            try
            {
                // Kiểm soát đầu vào và đưa các thông báo lỗi vào trong ModelState (nếu có)
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError(nameof(data.CategoryName), "Vui lòng nhập tên loại hàng");
                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError(nameof(data.Description), "Vui lòng nhập mô tả loại hàng");
                

                //Thông qua thuộc tính IsValid của ModelState để kiểm tra xem có tồn tại lỗi hay không
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CategoryId == 0 ? "Bổ sung loại hàng" : "Cập nhật thông tin loại hàng";
                    return View("Edit", data);
                }
                if (uploadPhoto != null)
                {
                    string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}"; // tên file sẽ lưu
                    string folder = Path.Combine(ApplicationContext.HostEnviroment.WebRootPath, "images\\categories"); // đường dẫn đến thư mục lưu file
                    string filePath = Path.Combine(folder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        uploadPhoto.CopyTo(stream);
                    }
                    data.Photo = fileName;
                }
                if (data.CategoryId == 0)
                {
                    int id = CommonDataService.AddCategory(data);
                    
                }
                else
                {
                    bool result = CommonDataService.UpdateCategory(data);
                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Không thể lưu dữ liệu. Vui lòng thử lại!");
                return View("Edit", data);
            }
        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");

            }
            var model = CommonDataService.GetCategory(id);
            if (model == null)
                return RedirectToAction("Index");
            ViewBag.AllowDelete = !CommonDataService.IsUsedCategory(id);

            return View(model);
        }
    }
}
