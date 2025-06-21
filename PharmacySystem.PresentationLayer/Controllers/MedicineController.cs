#region
using E_Commerce.DomainLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.ApplicationLayer.Services;
using PharmacySystem.DomainLayer.Entities;
#endregion

namespace PharmacySystem.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        #region DBContext
        private readonly IUnitOfWork _unitOfWork;
        private readonly MedicineService medicineService;

        public MedicineController(IUnitOfWork unitOfWork,MedicineService _medicineService)
        {
            _unitOfWork = unitOfWork;
            medicineService = _medicineService; 
        }
        #endregion

        [HttpGet("GetAllMedicines")]
        [EndpointSummary("Get All Medicines")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Medicine>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetAllMedicines()
        {
            var medicines = await _unitOfWork.medicineRepository.GetAllAsync();
            return Ok(medicines);
        }


        [HttpGet("SearchMedicines")]
        [EndpointSummary("Search For Medicines")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Medicine>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Medicine>>> SearchMedicines([FromQuery] string? searchTerm = null)
        {
            var medicines = await _unitOfWork.medicineRepository.SearchMedicinesAsync(searchTerm);
            return Ok(medicines);
        }


        [HttpGet("FilterMedicines")]
        [EndpointSummary("Filter Medicines")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Medicine>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Medicine>>> FilterMedicines(string? desc,string? name,string? sort)
        {
            var medicines = await _unitOfWork.medicineRepository.FilterMedicine(desc, name, sort);
            return Ok(medicines);
        }


        [HttpGet("{id}")]
        [EndpointSummary("Get Medicine")]
        [ProducesResponseType(200, Type = typeof(Medicine))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Medicine>> GetMedicine(int id)
        {
            var medicine = await _unitOfWork.medicineRepository.GetByIdAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return Ok(medicine);
        }


        [HttpPost("CreateMedicine")]
        [EndpointSummary("Create Medicine")]
        [ProducesResponseType(200, Type = typeof(Medicine))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Medicine>> CreateMedicine(Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.medicineRepository.AddAsync(medicine);
            var result = await _unitOfWork.SaveAsync();

            if (!result)
            {
                return BadRequest("Failed to create medicine");
            }

            return CreatedAtAction(nameof(GetMedicine), new { id = medicine.Id }, medicine);
        }


        [HttpPut("{id}")]
        [EndpointSummary("Update Medicine")]
        [ProducesResponseType(200, Type = typeof(Medicine))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateMedicine(int id, Medicine medicine)
        {
            if (id != medicine.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _unitOfWork.medicineRepository.UpdateAsync(medicine);
                var result = await _unitOfWork.SaveAsync();

                if (!result)
                {
                    return BadRequest("Failed to update medicine");
                }
            }
            catch (Exception)
            {
                var exists = await _unitOfWork.medicineRepository.GetByIdAsync(id) != null;
                if (!exists)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        [EndpointSummary("Delete Medicine")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = await _unitOfWork.medicineRepository.GetByIdAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }

            await _unitOfWork.medicineRepository.DeleteAsync(medicine);
            var result = await _unitOfWork.SaveAsync();

            if (!result)
            {
                return BadRequest("Failed to delete medicine");
            }

            return NoContent();
        }

        [HttpGet("ByArea/{areaId:int}")]
        [EndpointSummary("Get Medicines exist in Area")]
        public async Task<IActionResult> GetMedicineStatsByArea(int areaId)
        {
            if (areaId <= 0)
            {
                return BadRequest("Area ID  must be positive integer");
            }
            var result = await medicineService.GetMedicineStatsByAreaAsync(areaId);

            if (result == null || !result.Any())
            {
                return NotFound("No Medicines found ");
            }
            return Ok(result);
        }
        [EndpointSummary("Get Medicines paginated exist in Area")]
        [HttpGet("ByAreaPagination/{areaId:int}")]
        public async Task<IActionResult> GetMedicinesByArea(
     int areaId,
     [FromQuery] int page = 1,
     [FromQuery] int pageSize = 15)
        {
            if (areaId <= 0)
                return BadRequest("Invalid Area ID");

            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;

            var result = await medicineService.GetMedicineStatsByAreaAsync(areaId, page, pageSize);

            return Ok(result);
        }

        [EndpointSummary("Get Medicines paginated exist in Area")]
        [HttpGet("SearchNameByAreaPagination/{areaId:int}")]
        public async Task<IActionResult> GetMedicinesByArea(
  int areaId,
  [FromQuery] int page = 1,
  [FromQuery] int pageSize = 15,
   [FromQuery] string? search = null)
        {
            if (areaId <= 0)
                return BadRequest("Invalid Area ID");

            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;

            var result = await medicineService.GetMedicineStatsByAreaAsync(areaId, page, pageSize,search);

            return Ok(result);
        }
    }
}
