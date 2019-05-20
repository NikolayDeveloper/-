using MyAppWheels5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyAppWheels5.Controllers
{
    // Контроллер для подачи объявления
    [Authorize]
    public class AdvertController : Controller
    {
        [HttpGet]
        public ActionResult CreateAdvert()
        {
            SelectList listOfVehicle = null;
            using (ConectionContext db = new ConectionContext())
            {
                listOfVehicle = new SelectList(db.TypeOfVehicles.ToList(), "Id", "NameVehicle");
            }
            ViewBag.typesOfCar = listOfVehicle;
            return View();
        }
        // Добавление списка Марок и Моделей на страницу
        [HttpPost]
        public PartialViewResult CreateAdvert(int IdTypeOfVehicle = 0, int IdMarkka = 0)
        {
            string IdList = "";
            string firstChildOfList = "";
            SelectList list = null;
            IEnumerable<Markka> typeOfMarkkas = null;
            IEnumerable<Model> typeOfModels = null;
            using (ConectionContext db = new ConectionContext())
            {
                if (IdTypeOfVehicle != 0)
                {
                    typeOfMarkkas = db.Markkas.Where(m => m.IdTypeOfVehicle == IdTypeOfVehicle).ToList();
                    list = new SelectList(typeOfMarkkas, "Id", "NameMarkka");
                    IdList = "NameMarkka";
                    firstChildOfList = "Выбирите марку";
                }
                else if (IdMarkka != 0)
                {
                    typeOfModels = db.Models.Where(m => m.IdMarkka == IdMarkka).ToList();
                    list = new SelectList(typeOfModels, "Id", "NameModel");
                    IdList = "NameModel";
                    firstChildOfList = "Выбирите модель";
                }
            }
            ViewBag.List = list;
            ViewBag.IdList = IdList;
            ViewBag.MakeChoise = firstChildOfList;
            return PartialView("_AddTermsToTypeOfVehicle");
        }
        // Добавление списка опций 1-го уровня
        [HttpPost]
        public ActionResult AddCheckBox(int IdTypeOfVehicle = 0)
        {
            if (IdTypeOfVehicle == 1)
            {
                OptionOfCarModel optionOfCar = new OptionOfCarModel();
                using (ConectionContext db = new ConectionContext())
                {
                    optionOfCar.OptionListOfCar = db.OptionsOfCars.ToList();
                }
                return PartialView("_AddCheckBox", optionOfCar);
            }
            if (IdTypeOfVehicle == 2)
            {
                IEnumerable<OptionsOfTruck> optionsOfTruck = null;
                using (ConectionContext db = new ConectionContext())
                {
                    optionsOfTruck = db.OptionsOfTrucks.ToList();
                }
                return PartialView("_AddOptionsOfTruck",optionsOfTruck);
            }
            return new HttpNotFoundResult();
        }

        // Добавление списка опций 2-го уровня
        [HttpPost]
        public ActionResult AddOptions(int IdTypeOfVehicle = 0)
        {
            ViewBag.time = DateTime.Now.Year;  // Для выбора списка годов
            SelectList list = null;
            if (IdTypeOfVehicle !=0)
            {
                using (ConectionContext db = new ConectionContext())
                {
                    list = new SelectList(db.Cities.ToList(), "Id", "NameCity");
                }
                ViewBag.cities = list;
                return PartialView("_OptionOfCar");
            }
            return new HttpNotFoundResult();
        }
        // Обработка  и запись фотографий в базу данных
        [HttpPost]
        public void ProcessImages(HttpPostedFileBase[] Images = null)
        {
            byte[] imageData = null;
            ImageOfCar img;
            int IdCurrentAdvert = 0;                   // Получение Id текущего объявления
            string sqlExpression = "GetLastId";       // Хранимая процедура 
            string connectionString = ConfigurationManager.ConnectionStrings["database_P_conection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@LastId", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                IdCurrentAdvert = Convert.ToInt32(command.Parameters["@LastId"].Value);
                connection.Close();
            }
            if (IdCurrentAdvert != 0)
            {
                using (ConectionContext db = new ConectionContext())
                {
                    if (Images != null)
                    {
                        img = new ImageOfCar();
                        for (int i = 0; i < Images.Length; i++)
                        {
                            using (var binaryReader = new BinaryReader(Images[i].InputStream))
                            {
                                imageData = binaryReader.ReadBytes(Images[i].ContentLength);
                            }
                            img.ImageData = imageData;
                            img.IdlistOfVehicle = IdCurrentAdvert;
                            db.ImageOfCars.Add(img);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
        // Добавление в таблицу listOfVehicle данных
        [HttpPost]
        public ActionResult PostAdvert(OptionOfCarModel option=null, int NameVehicle = 0, int NameMarkka = 0,
           int NameModel = 0, string text = null,int NameCity=0,int year=0,int Price=0,string RadioButton=null)
        {
            string strCheckBoxes = null;             // Формирование строки из выбранных чекбоксов пользователем 
            string UserEmail= null;                  // Email текущего пользователя
            string sqlExpression = null;            // Хранимая процедура для вставки данных в listOfVehicle
            UserEmail = User.Identity.Name;
            if (text=="")
            {
                text = null;
            }
            for (int i=0;i<option.OptionListOfCar.Count;i++)
                {
                    if(option.OptionListOfCar[i].IsChecked)
                    {
                        strCheckBoxes += option.OptionListOfCar[i].NameInRussion+", ";
                    }
                }
            if (strCheckBoxes!=null)
                {
                    int lastIndex = strCheckBoxes.Length - 2;
                    strCheckBoxes = strCheckBoxes.Remove(lastIndex);
                    strCheckBoxes += ".";
                }
            sqlExpression = "InsertAdvert";
            string connectionString = ConfigurationManager.ConnectionStrings["database_P_conection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;
                // Создаю массив из названий параметров
                string[] ParameterName = { "@UserName", "@IdTypeOfVehicle", "@IdMarkka", "@IdModel", "@IdCity", "@YearOfCar",
                                                            "@Price","@DateAdvert","@Discribe","@OptionOfVehicle","@OptionOfTruck"};
                // Создаю массив значений для параметров
                object[] Value = { UserEmail , NameVehicle, NameMarkka, NameModel, NameCity,year,
                                                    Price,DateTime.Now,text,strCheckBoxes,RadioButton };
                for(int i=0;i<ParameterName.Length;i++)
                {
                  SqlParameter  parametr = new SqlParameter(ParameterName[i], Value[i]);
                  command.Parameters.Add(parametr);
                }
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index","Home");
        }
    }
}



