using DAL;
using Microsoft.AspNetCore.Mvc;
using Model;
using Portal.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //var data = new DAL.StudenHelper().GetDataAsync();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public  async Task<ActionResult> GetDataAsync()
        {
            DataTableMetaDataDTO dt = new Model.DataTableMetaDataDTO();
            dt.Data = await  new StudenHelper().GetClassDataAsync();
            return Json(dt); 
            //CompanyDBEntitiesobj = new CompanyDBEntities();
            //var contacts = obj.Emp_Information.Select(x => new
            //{
            //    Id = x.EMP_ID,
            //    Name = x.Name,taT
            //    ProjectName = x.ProjectName,
            //    ManagerName = x.ManagerName,
            //    city = x.City
            //}).ToList();
            //return Json(contacts, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetStudentDataAsync(TableDataDTO dtoItem)
        {
            TableDataDTO dt = new TableDataDTO();
            dt.Data = await new StudenHelper().GetStudentDataAsync(dtoItem.Id);
            return Json(dt);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateClassRecord(ClassTeacherDto data)
        {
            bool val = await new StudenHelper().UpdateClassRecord(data);
            return Json(val);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteClassRecord(int id)
        {
            bool val = await new StudenHelper().DeleteClassRecord(id);
            return Json(val);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
