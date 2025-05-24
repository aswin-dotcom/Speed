using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopSpeed.Application.ApplicationConstant;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Data;

namespace TopSpeed.Web.Areas.Admin.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IunitOFWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VehicleTypeController(IunitOFWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<VehicleType> vehicleType = await _unitOfWork.vehicleType.GetAllAsync();
            return View(vehicleType);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleType vehicleType)
        {
           




            if (ModelState.IsValid)
            {

                await _unitOfWork.vehicleType.Create(vehicleType);
                await _unitOfWork.SaveAsync();
                TempData["success"] = CommonMessage.Recordcreated;
                return RedirectToAction(nameof(Index));

            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            VehicleType vehicleType = await _unitOfWork.vehicleType.GetByIdAsync(id);
            return View(vehicleType);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            VehicleType vehicleType = await _unitOfWork.vehicleType.GetByIdAsync(id);
            return View(vehicleType);

        }
        [HttpPost]

        public async Task<IActionResult> Edit(VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.vehicleType.update(vehicleType);
                await _unitOfWork.SaveAsync();
                TempData["Edit"] = CommonMessage.RecordUpdated;
                return RedirectToAction(nameof(Index));
            }

            return View();


        }

        [HttpGet]
        public async Task<IActionResult> Deleteview(Guid id)
        {
            VehicleType vehicleType = await _unitOfWork.vehicleType.GetByIdAsync(id);
            return View(vehicleType);

        }

        [HttpPost]
        public async Task<IActionResult> Deleteview(VehicleType vehicleType)
        {

           
         
                    await _unitOfWork.vehicleType.Delete(vehicleType);
                    await _unitOfWork.SaveAsync();
                    TempData["Delete"] = CommonMessage.RecordDeleted;

                    return RedirectToAction(nameof(Index));
             


        }

    }
}
