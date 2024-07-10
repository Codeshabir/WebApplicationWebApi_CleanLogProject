﻿using Microsoft.AspNetCore.Mvc;
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
    public class SnowLogsAPIController : ControllerBase
    {
        private readonly LogDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public SnowLogsAPIController(LogDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
       
        [HttpGet] // Remove this attribute since the method is commented out
        public async Task<ActionResult<IEnumerable<SnowLogs>>> GetCleaningLogs()
        {
            var list = await _context.snowLogs.ToListAsync();
            return (list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SnowLogs>> GetCleaningLog(int id)
        {
            var houseCleaningLog = await _context.snowLogs.FindAsync(id);
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
        public async Task<ActionResult<SnowLogs>> PostLandscapingLog([FromForm] SnowLogsDTO landscapingLogDto)
        {
            var landscapingLog = new SnowLogs
            {
                Date = landscapingLogDto.date,
                ContractorsName = landscapingLogDto.contractorsName,
                SubContractorsName = landscapingLogDto.subContractorsName,
                WorkDate = landscapingLogDto.workDate,
                PropertyAddress = landscapingLogDto.propertyAddress,
                LocationCoordinates = landscapingLogDto.locationCoordinates,
                WorkStartTime = landscapingLogDto.workStartTime,
                WeatherCondition = landscapingLogDto.weatherCondition,
                WorkCompletionTime = landscapingLogDto.workCompletionTime,
                DescriptionOfWorkPerformed = landscapingLogDto.descriptionOfWorkPerformed,
                DifficultiesOrObstaclesEncountered = landscapingLogDto.difficultiesOrObstaclesEncountered,
                SuppliesUsed = landscapingLogDto.suppliesUsed,
                GeneralCommentsOrObservations = landscapingLogDto.generalCommentsOrObservations,
                ParkingArea = landscapingLogDto.parkingArea,
                ParkingRamp = landscapingLogDto.parkingRamp,
                Sidewalk = landscapingLogDto.sidewalk,
                StairwaysSteps = landscapingLogDto.stairwaysSteps,
                Driveways = landscapingLogDto.driveways,
                Comments = landscapingLogDto.comments,
                PreWorkPhotosPath = string.Empty,
                PostWorkPhotosPath = string.Empty

    };

            // Handle PreWork Photos
            if (landscapingLogDto.preWorkPhotos != null && landscapingLogDto.preWorkPhotos.Any())
            {
                foreach (var file in landscapingLogDto.preWorkPhotos)
                {
                    var preWorkFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var preWorkFilePath = Path.Combine(_hostEnvironment.WebRootPath, "images", preWorkFileName);
                    using (var stream = new FileStream(preWorkFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    landscapingLog.PreWorkPhotosPath += preWorkFileName + ",";
                }
                landscapingLog.PreWorkPhotosPath = landscapingLog.PreWorkPhotosPath.TrimEnd(',');
            }

            // Handle PostWork Photos
            if (landscapingLogDto.postWorkPhotos != null && landscapingLogDto.postWorkPhotos.Any())
            {
                foreach (var file in landscapingLogDto.postWorkPhotos)
                {
                    var postWorkFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var postWorkFilePath = Path.Combine(_hostEnvironment.WebRootPath, "images", postWorkFileName);
                    using (var stream = new FileStream(postWorkFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    landscapingLog.PostWorkPhotosPath += postWorkFileName + ",";
                }
                landscapingLog.PostWorkPhotosPath = landscapingLog.PostWorkPhotosPath.TrimEnd(',');
            }

            _context.snowLogs.Add(landscapingLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSnowLog", new { id = landscapingLog.Id }, landscapingLog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLandscapingLog(int id, [FromForm] SnowLogsDTO landscapingLogDto)
        {
         
            var landscapingLog = await _context.snowLogs.FindAsync(id);
            if (landscapingLog == null)
            {
                return NotFound("Landscaping log not found.");
            }

            // Update properties from the DTO
            landscapingLog.Date = landscapingLogDto.date;
            landscapingLog.ContractorsName = landscapingLogDto.contractorsName;
            landscapingLog.SubContractorsName = landscapingLogDto.subContractorsName;
            landscapingLog.WorkDate = landscapingLogDto.workDate;
            landscapingLog.PropertyAddress = landscapingLogDto.propertyAddress;
            landscapingLog.LocationCoordinates = landscapingLogDto.locationCoordinates;
            landscapingLog.WorkStartTime = landscapingLogDto.workStartTime;
            landscapingLog.WeatherCondition = landscapingLogDto.weatherCondition;
            landscapingLog.WorkCompletionTime = landscapingLogDto.workCompletionTime;
            landscapingLog.DescriptionOfWorkPerformed = landscapingLogDto.descriptionOfWorkPerformed;
            landscapingLog.DifficultiesOrObstaclesEncountered = landscapingLogDto.difficultiesOrObstaclesEncountered;
            landscapingLog.SuppliesUsed = landscapingLogDto.suppliesUsed;
            landscapingLog.GeneralCommentsOrObservations = landscapingLogDto.generalCommentsOrObservations;
            landscapingLog.ParkingArea = landscapingLogDto.parkingArea;
            landscapingLog.ParkingRamp = landscapingLogDto.parkingRamp;
            landscapingLog.Sidewalk = landscapingLogDto.sidewalk;
            landscapingLog.StairwaysSteps = landscapingLogDto.stairwaysSteps;
            landscapingLog.Driveways = landscapingLogDto.driveways;
            landscapingLog.Comments = landscapingLogDto.comments;

            // Handle PreWork Photos if provided
            if (landscapingLogDto.preWorkPhotos != null && landscapingLogDto.preWorkPhotos.Count > 0)
            {
                var preWorkPhotoNames = new List<string>();
                foreach (var file in landscapingLogDto.preWorkPhotos)
                {
                    if (file.Length > 0)
                    {
                        var preWorkFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var preWorkFilePath = Path.Combine(_hostEnvironment.WebRootPath, "images", preWorkFileName);
                        using (var stream = new FileStream(preWorkFilePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        preWorkPhotoNames.Add(preWorkFileName);
                    }
                }
                landscapingLog.PreWorkPhotosPath = string.Join(",", preWorkPhotoNames);
            }

            // Handle PostWork Photos if provided
            if (landscapingLogDto.postWorkPhotos != null && landscapingLogDto.postWorkPhotos.Count > 0)
            {
                var postWorkPhotoNames = new List<string>();
                foreach (var file in landscapingLogDto.postWorkPhotos)
                {
                    if (file.Length > 0)
                    {
                        var postWorkFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var postWorkFilePath = Path.Combine(_hostEnvironment.WebRootPath, "images", postWorkFileName);
                        using (var stream = new FileStream(postWorkFilePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        postWorkPhotoNames.Add(postWorkFileName);
                    }
                }
                landscapingLog.PostWorkPhotosPath = string.Join(",", postWorkPhotoNames);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LandscapingLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCleaningLog(int id)
        {
            var houseCleaningLog = await _context.snowLogs.FindAsync(id);
            if (houseCleaningLog == null)
            {
                return NotFound();
            }

            _context.snowLogs.Remove(houseCleaningLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LandscapingLogExists(int? id)
        {
            return _context.snowLogs.Any(e => e.Id == id);
        }
    }
}