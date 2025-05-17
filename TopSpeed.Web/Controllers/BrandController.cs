using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopSpeed.Application.ApplicationConstant;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Data;

namespace TopSpeed.Web.Controllers
{
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {

            List<Brand> Brands  = _dbContext.Brand.ToList();
            return View(Brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Brand brand) 
        {
             string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count>0)
            {
                string newFileName =  Guid.NewGuid().ToString();
                var  upload  =  Path.Combine(webRootPath, @"images\brand");
                var extension  =  Path.GetExtension(file[0].FileName);
                using (var fileStream =  new FileStream(Path.Combine(upload,newFileName+extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }


                brand.BrandLogo = @"/images/brand/" + newFileName + extension; 
            }




                if (ModelState.IsValid )
                {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Log or debug here
                }
                _dbContext.Brand.Add(brand);
                    _dbContext.SaveChanges();
                    TempData["success"] = CommonMessage.Recordcreated;
                    return RedirectToAction(nameof(Index));

                }
            return View();
           
        }
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            Brand brand  = _dbContext.Brand.FirstOrDefault(item=>item.Id == id);
            return View(brand);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Brand brand = _dbContext.Brand.FirstOrDefault(item => item.Id == id);
            return View(brand);

        }
        [HttpPost]

        public IActionResult Edit(Brand brand)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0 )
            {
                string newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images\brand");
                var extension = Path.GetExtension(file[0].FileName);
                var objfrom = _dbContext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);
                if (objfrom.BrandLogo!=null)
                {
                    var FileLocationExists = Path.Combine(webRootPath, objfrom.BrandLogo.Trim('/'));
                    if (System.IO.File.Exists(FileLocationExists))
                    {
                        System.IO.File.Delete(FileLocationExists);
                    }
                }
                
               
                using(var filestream =  new FileStream(Path.Combine(upload, newFileName+ extension), FileMode.Create))
                {
                    file[0].CopyTo(filestream);  
                }
                brand.BrandLogo = @"\images\brand\" + newFileName + extension;

            }
            if (ModelState.IsValid)
            {
                var updateObj = _dbContext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);
                updateObj.Name =  brand.Name;
                updateObj.EstablishedYear = brand.EstablishedYear;
                if (updateObj.BrandLogo != null)
                {
                    updateObj.BrandLogo = brand.BrandLogo;
                }

                _dbContext.Brand.Update(updateObj);
                _dbContext.SaveChanges(); 
                TempData["Edit"] = CommonMessage.RecordUpdated;
                return RedirectToAction(nameof(Index));
            }

            return View();

           
        }

        [HttpGet]
        public IActionResult Deleteview(Guid id)
        {
            Brand brand = _dbContext.Brand.FirstOrDefault(item => item.Id == id);
            return View(brand);

        }

        [HttpPost]
        public IActionResult Deleteview(Brand brand)
        {

            string webRootPath = _webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(brand.BrandLogo))


            {
                var FileLocation = Path.Combine(webRootPath, brand.BrandLogo.Trim('/'));
                if (System.IO.File.Exists(FileLocation))
                {
                    System.IO.File.Delete(FileLocation);
                }
                var obj = _dbContext.Brand.AsNoTracking().FirstOrDefault(item => item.Id == brand.Id);
                if (obj != null)
                {
                    _dbContext.Brand.Remove(obj);
                    _dbContext.SaveChanges();
                    TempData["Delete"] = CommonMessage.RecordDeleted;

                    return RedirectToAction(nameof(Index));
                }

            }
            
            return View();
           

        }

    }
}
