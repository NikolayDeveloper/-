using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAppWheels5.Controllers
{
    public class HomeController : Controller
    {
        // Добавление типов кузова на страницу
        public ActionResult Index()
        {
            IEnumerable<TypeOfVehicle> typeOfVehicle = null;
            using (ConectionContext db = new ConectionContext())
            {
                typeOfVehicle = db.TypeOfVehicles.ToList();
            }
                return View(typeOfVehicle);
        }
        // Создание навигационного меню
        [HttpPost]
        public ActionResult AddCityAndMarkka(int IdTypeOfVehicle = 0, int IdMarkka = 0)
        {
            IEnumerable<City> city = null;
            IEnumerable<Markka> markka = null;
            IEnumerable<Model> model = null;
            if (IdTypeOfVehicle != 0 || IdMarkka == 0)
            {
                using (ConectionContext db = new ConectionContext())
                {
                    city = db.Cities.ToList();
                    markka = db.Markkas.Where(m => m.IdTypeOfVehicle == IdTypeOfVehicle).ToList();
                }
                ViewBag.city = city;
                return PartialView("_AddCityAndMarkka", markka);
            }
            else if(IdTypeOfVehicle == 0 || IdMarkka != 0)
            {
                using (ConectionContext db = new ConectionContext())
                {
                    model=db.Models.Where(m=>m.IdMarkka==IdMarkka).ToList();
                    
                    return PartialView("_AddModel",model);
                }
            }
            return new HttpNotFoundResult();
        }
        // Нахождение и добавление результатов поиска на страницу по заданным критериям
        [HttpPost]
        public ActionResult SelectListOfVehicle(int IdTypeOfVehicle = 0, int IdMarkka = 0, int IdModel = 0, int IdCity = 0)
        {
            IEnumerable<listOfVehicle> list = null;
            using (ConectionContext db = new ConectionContext())
            {
                if (IdTypeOfVehicle != 0 && IdCity == 0 && IdMarkka == 0 && IdModel == 0)
                {
                     list = db.listOfVehicles.Include("ImageOfCars").Where(m => m.IdTypeOfVehicle == IdTypeOfVehicle).OrderByDescending(m=>m.DateAdvert).ToList();
                }
                else if (IdTypeOfVehicle != 0 && IdCity != 0 && IdMarkka == 0 && IdModel == 0)
                {
                    list = db.listOfVehicles.Include("ImageOfCars").Where(m => m.IdTypeOfVehicle == IdTypeOfVehicle && m.IdCity == IdCity).OrderByDescending(m => m.DateAdvert).ToList();
                }
                else if (IdTypeOfVehicle != 0 && IdCity != 0 && IdMarkka != 0 && IdModel == 0)
                {
                    list = db.listOfVehicles.Include("ImageOfCars").Where(m => m.IdCity == IdCity && m.IdMarkka == IdMarkka).OrderByDescending(m => m.DateAdvert).ToList();
                }
                else if (IdTypeOfVehicle != 0 && IdCity != 0 && IdMarkka != 0 && IdModel != 0)
                {
                    list = db.listOfVehicles.Include("ImageOfCars").Where(m => m.IdCity == IdCity && m.IdModel == IdModel).OrderByDescending(m => m.DateAdvert).ToList();
                }
                else if (IdTypeOfVehicle != 0 && IdCity == 0 && IdMarkka != 0 && IdModel == 0)
                {
                    list = db.listOfVehicles.Include("ImageOfCars").Where(m => m.IdMarkka == IdMarkka).OrderByDescending(m => m.DateAdvert).ToList();
                }
                else if (IdTypeOfVehicle != 0 && IdCity == 0 && IdMarkka != 0 && IdModel != 0)
                {
                    list = db.listOfVehicles.Include("ImageOfCars").Where(m => m.IdModel == IdModel).OrderByDescending(m => m.DateAdvert).ToList();
                }
            }
            return PartialView("_SelectListOfVehicle",list);
        }
        // Страница для выбранного объявления
        [HttpGet]
        public ActionResult CurrentAdvert(int IdListOfVehicle=0)
        {
            GetListOfVehicle advert = null;
            IEnumerable<ImageOfCar> img = null;
            using (ConectionContext db = new ConectionContext())
            {
                advert = db.GetListOfVehicles.Where(m => m.Id == IdListOfVehicle).FirstOrDefault();
                img = db.ImageOfCars.Where(m => m.IdlistOfVehicle == IdListOfVehicle).ToList();
            }
            ViewBag.img = img;

            return View(advert);
        }
     }
}


