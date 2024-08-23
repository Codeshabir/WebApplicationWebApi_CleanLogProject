using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Models;
using Client.Model;
using Client.Data;
using Microsoft.Extensions.Hosting;
using Client.Services;
namespace Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CleaningLogsAPIController : ControllerBase
    {
        private readonly LogDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


		private readonly ILogger<CleaningLogsAPIController> _logger;

		public CleaningLogsAPIController(LogDbContext context, IWebHostEnvironment hostEnvironment, ILogger<CleaningLogsAPIController> logger)
		{
			_context = context;
			_hostEnvironment = hostEnvironment;
			_logger = logger;
		}


		[HttpGet] // Remove this attribute since the method is commented out
        public async Task<ActionResult<IEnumerable<CleaningLog>>> GetCleaningLogs()
        {
            var list = await _context.houseCleaningLogs.ToListAsync();
            return (list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CleaningLog>> GetCleaningLog(int id)
        {
            var houseCleaningLog = await _context.houseCleaningLogs.FindAsync(id);

            if (houseCleaningLog == null)
            {
                return NotFound();
            }

            return houseCleaningLog;
        }

        //[HttpPost]
        //public async Task<ActionResult<CleaningLog>> PostCleaningLog(CleaningLog CleaningLog)
        //{
        //    _context.houseCleaningLogs.Add(CleaningLog);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCleaningLog", new { id = CleaningLog.Id }, CleaningLog);
        //}

        [HttpPost]
        public async Task<ActionResult<CleaningLog>> PostCleaningLog([FromForm] CleaningLogsDTO cleaningLogDto)
        {
            var cleaningLog = new CleaningLog
            {
                Date = cleaningLogDto.date,
                ContractorsName = cleaningLogDto.contractorsName,
                EmployeeName = cleaningLogDto.employeeName,
                WorkDate = cleaningLogDto.workDate,
                PropertyAddress = cleaningLogDto.propertyAddress,
                LocationCoordinates = cleaningLogDto.locationCoordinates,
                WorkStartTime = cleaningLogDto.workStartTime,
                WeatherCondition = cleaningLogDto.weatherCondition,
                WorkCompletionTime = cleaningLogDto.workCompletionTime,
                DescriptionOfWorkPerformed = cleaningLogDto.descriptionOfWorkPerformed,
                DifficultiesOrObstaclesEncountered = cleaningLogDto.difficultiesOrObstaclesEncountered,
                GeneralCommentsOrObservations = cleaningLogDto.generalCommentsOrObservations,
                Comments = cleaningLogDto.comments,
                Photos = string.Empty // Initialize as empty string
            };

            var photoFileNames = new List<string>();
            if (cleaningLogDto.photoFiles != null && cleaningLogDto.photoFiles.Any())
            {
                foreach (var file in cleaningLogDto.photoFiles)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        photoFileNames.Add(fileName); // Add the file name to the list
                    }
                }

                cleaningLog.Photos = string.Join(",", photoFileNames); // Join the file names into a comma-separated string
            }

            _context.houseCleaningLogs.Add(cleaningLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCleaningLog", new { id = cleaningLog.Id }, cleaningLog);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCleaningLog(int? id, [FromForm] CleaningLogsDTO cleaningLogDto)
        //{

        //    var cleaningLog = await _context.houseCleaningLogs.FindAsync(id);
        //    if (cleaningLog == null)
        //    {
        //        return NotFound();
        //    }

        //    // Update properties from the DTO
        //    cleaningLog.Date = cleaningLogDto.date;
        //    cleaningLog.ContractorsName = cleaningLogDto.contractorsName;
        //    cleaningLog.EmployeeName = cleaningLogDto.employeeName;
        //    cleaningLog.WorkDate = cleaningLogDto.workDate;
        //    cleaningLog.PropertyAddress = cleaningLogDto.propertyAddress;
        //    cleaningLog.LocationCoordinates = cleaningLogDto.locationCoordinates;
        //    cleaningLog.WorkStartTime = cleaningLogDto.workStartTime;
        //    cleaningLog.WeatherCondition = cleaningLogDto.weatherCondition;
        //    cleaningLog.WorkCompletionTime = cleaningLogDto.workCompletionTime;
        //    cleaningLog.DescriptionOfWorkPerformed = cleaningLogDto.descriptionOfWorkPerformed;
        //    cleaningLog.DifficultiesOrObstaclesEncountered = cleaningLogDto.difficultiesOrObstaclesEncountered;
        //    cleaningLog.GeneralCommentsOrObservations = cleaningLogDto.generalCommentsOrObservations;
        //    cleaningLog.Comments = cleaningLogDto.comments;

        //    // Handle photos if any
        //    var photoFileNames = new List<string>();
        //    if (cleaningLogDto.photoFiles != null && cleaningLogDto.photoFiles.Any())
        //    {
        //        foreach (var file in cleaningLogDto.photoFiles)
        //        {
        //            if (file.Length > 0)
        //            {
        //                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //                var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);

        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(stream);
        //                }

        //                photoFileNames.Add(fileName);
        //            }
        //        }
        //        cleaningLog.Photos = string.Join(",", photoFileNames);
        //    }

        //    _context.Entry(cleaningLog).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CleaningLogExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCleaningLog(int id, [FromForm] CleaningLogsDTO cleaningLogDto)
        {
            // Validate ID
            if (id <= 0)
            {
                _logger.LogWarning("Update attempt with invalid ID");
                return BadRequest("Invalid ID");
            }

            // Fetch the existing cleaning log
            var cleaningLog = await _context.houseCleaningLogs.FindAsync(id);
            if (cleaningLog == null)
            {
                _logger.LogWarning("Cleaning Log with ID {id} not found", id);
                return NotFound("Cleaning log not found.");
            }

            // Update properties from the DTO
            cleaningLog.ContractorsName = cleaningLogDto.contractorsName;
            cleaningLog.EmployeeName = cleaningLogDto.employeeName;
            cleaningLog.WorkDate = cleaningLogDto.workDate;
            cleaningLog.PropertyAddress = cleaningLogDto.propertyAddress;
            cleaningLog.LocationCoordinates = cleaningLogDto.locationCoordinates;
            cleaningLog.WorkStartTime = cleaningLogDto.workStartTime;
            cleaningLog.WeatherCondition = cleaningLogDto.weatherCondition;
            cleaningLog.WorkCompletionTime = cleaningLogDto.workCompletionTime;
            cleaningLog.DescriptionOfWorkPerformed = cleaningLogDto.descriptionOfWorkPerformed;
            cleaningLog.DifficultiesOrObstaclesEncountered = cleaningLogDto.difficultiesOrObstaclesEncountered;
            cleaningLog.GeneralCommentsOrObservations = cleaningLogDto.generalCommentsOrObservations;
            cleaningLog.Comments = cleaningLogDto.comments;

            // Handle photo files
            var photoFileNames = new List<string>();
            if (cleaningLogDto.photoFiles != null && cleaningLogDto.photoFiles.Count > 0)
            {
                foreach (var file in cleaningLogDto.photoFiles)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        photoFileNames.Add(fileName);
                    }
                }
                cleaningLog.Photos = string.Join(",", photoFileNames);
            }

            // Mark entity as modified and save changes
            _context.Entry(cleaningLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully updated Cleaning Log with ID {id}", id);
                return Ok(new { message = "Cleaning log updated successfully." });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency exception while updating Cleaning Log with ID {id}", id);
                if (!CleaningLogExists(id))
                {
                    return NotFound("Cleaning log not found.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Cleaning Log with ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the cleaning log.");
            }
        }

        private bool CleaningLogExists(int id)
        {
            return _context.houseCleaningLogs.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCleaningLog(int id)
        {
            var houseCleaningLog = await _context.houseCleaningLogs.FindAsync(id);
            if (houseCleaningLog == null)
            {
                return NotFound();
            }

            _context.houseCleaningLogs.Remove(houseCleaningLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CleaningLogExists(int? id)
        {
            return _context.houseCleaningLogs.Any(e => e.Id == id);
        }
    }
}