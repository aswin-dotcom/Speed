using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopSpeed.Application.ApplicationConstant;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Data;

namespace TopSpeed.Web.Controllers
{
    public class BrandController : Controller
    {
        private readonly IunitOFWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(IunitOFWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<Brand> Brands  =await _unitOfWork.brand.GetAllAsync();
            return View(Brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand brand) 
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
            
                    await _unitOfWork.brand.Create(brand);
                      await _unitOfWork.SaveAsync();
                    TempData["success"] = CommonMessage.Recordcreated;
                    return RedirectToAction(nameof(Index));

                }
            return View();
           
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            Brand brand  = await _unitOfWork.brand.GetByIdAsync(id);
            return View(brand);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Brand brand = await _unitOfWork.brand.GetByIdAsync(id);
            return View(brand);

        }
        [HttpPost]

        public async Task<IActionResult> Edit(Brand brand)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0 )
            {
                string newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images\brand");
                var extension = Path.GetExtension(file[0].FileName);
                var objfrom =  await _unitOfWork.brand.GetByIdAsync(brand.Id);
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
               await _unitOfWork.brand.Update(brand); 
                await _unitOfWork.SaveAsync();
                TempData["Edit"] = CommonMessage.RecordUpdated;
                return RedirectToAction(nameof(Index));
            }

            return View();

           
        }

        [HttpGet]
        public async Task<IActionResult> Deleteview(Guid id)
        {
            Brand brand =await  _unitOfWork.brand.GetByIdAsync(id);
            return View(brand);

        }

        [HttpPost]
        public async Task<IActionResult> Deleteview(Brand brand)
        {

            string webRootPath = _webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(brand.BrandLogo))


            {
                var FileLocation = Path.Combine(webRootPath, brand.BrandLogo.Trim('/'));
                if (System.IO.File.Exists(FileLocation))
                {
                    System.IO.File.Delete(FileLocation);
                }
                var obj = await _unitOfWork.brand.GetByIdAsync(brand.Id);
                if (obj != null)
                {
                    await _unitOfWork.brand.Delete(brand);
                    await _unitOfWork.SaveAsync();
                    TempData["Delete"] = CommonMessage.RecordDeleted;

                    return RedirectToAction(nameof(Index));
                }

            }
            
            return View();
           

        }

    }
}
