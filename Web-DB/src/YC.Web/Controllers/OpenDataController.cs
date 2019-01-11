using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YC.Database;

namespace YC.Web.Controllers
{
    public class OpenDataController : Controller
    {
        private OpenDataDbContext db;
        public OpenDataController()
        {
            db = new OpenDataDbContext();
        }

        public IActionResult Index(string sortOrder)
        {
            OpenDataDbContext db = new OpenDataDbContext();

            //List<YC.Models.OpenData> models = db.OpenData.ToList();
            
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "日期" : "";
           
            ViewBag.NameSortParm = sortOrder == "訓練項目" ? "訓練部位" : "訓練項目";
            var item = from s in db.OpenData
                           select s;
            switch (sortOrder)
            {
                case "日期":
                    item = item.OrderByDescending(s => s.日期);
                    break;
                case "訓練項目":
                    item = item.OrderBy(s => s.訓練項目);
                    break;
                case "訓練部位":
                    item = item.OrderByDescending(s => s.訓練部位);
                    break;
                default:
                    item = item.OrderBy(s => s.日期);
                    break;
            }
            return View(item.ToList());
            //return View(models);
        }
        //網頁顯示
        [HttpGet]
        public IActionResult Details(int id,String name)
        {
            var model = db.OpenData.Find(id);
            return View(model);

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = db.OpenData.Find(id);
            return View(model);

        }
        [HttpPost]
        public IActionResult Edit(YC.Models.OpenData model)
        {
            var upDataModel = db.OpenData.Find(model.id);

             upDataModel.訓練項目 = model.訓練項目;
             upDataModel.訓練部位 = model.訓練部位;
             upDataModel.組數 = model.組數;
             upDataModel.每組次數 = model.每組次數;
             upDataModel.日期 = model.日期;

             db.SaveChanges();
             var updated = db.OpenData.Find(model.id);
            //return View(updated);
            return RedirectToAction("Index", "OpenData", null);



        }
        [HttpPost]
        public IActionResult Insert(YC.Models.OpenData model)
        {
            db.OpenData.Add(model);
            db.SaveChanges();
            return View();

        }

        [HttpGet]
        public IActionResult Insert()
        {
            
            return View();

        }

        [HttpPost]
        public IActionResult Delete(YC.Models.OpenData model)
        {
             var upDataModel = db.OpenData.Find(model.id);
            db.OpenData.Remove(upDataModel);
            db.SaveChanges();
            
            var updated = db.OpenData.Find(model.id);
            //return View(updated);
            //return View("Index", updated);
            return RedirectToAction("Index", "OpenData", null);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = db.OpenData.Find(id);
            return View(model);
           
            
        }


        
        public IActionResult   Search(string search)
        {
            List<YC.Models.OpenData> model = db.OpenData.Where(x=>x.日期 == search || x.訓練項目 == search || x.訓練部位 == search).ToList();
            //List<YC.Models.OpenData> model = db.OpenData.ToList();
            //var model = db.OpenData.Where(x => x.訓練項目 == search).ToList;
            
            return View(model);

        }

    }
}