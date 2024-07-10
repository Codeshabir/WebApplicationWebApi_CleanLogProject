using Client.Models;
using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Client.Controllers
{


    public class LandscapingLogController : Controller
    {
        private readonly LandscapingLogsService _landscapinglogsService;
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LandscapingLogController(LandscapingLogsService landscapinglogsService, IWebHostEnvironment hostEnvironment)
        {
            _landscapinglogsService = landscapinglogsService;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            
            var cleaningLogs = await _landscapinglogsService.GetLandscapingLogsAsync();
            foreach (var landscapingLog in cleaningLogs)
            {
                Debug.WriteLine($"landscapingLog ID: {landscapingLog.id}");
            }
            return View(cleaningLogs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var landscapingLog = await _landscapinglogsService.GetLandscapingLogAsync(id);
            if (landscapingLog == null)
            {
                return NotFound();
            }
            return View(landscapingLog);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LandscapingLogsDTO landscapingLog)
        {
            await HandleFileUploads(landscapingLog);

            // Pass landscapingLog to service method
            await _landscapinglogsService.CreateLandscapingLogAsync(landscapingLog);

            return RedirectToAction(nameof(Index));
        }

        private async Task HandleFileUploads(LandscapingLogsDTO log)
        {
            var preWorkPhotoNames = new List<string>();
            var postWorkPhotoNames = new List<string>();

            if (log.preWorkPhotos != null && log.preWorkPhotos.Any())
            {
                foreach (var file in log.preWorkPhotos)
                {
                    var preWorkFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                    var preWorkFilePath = Path.Combine(_hostEnvironment.WebRootPath, "images", preWorkFileName);
                    using (var stream = new FileStream(preWorkFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    preWorkPhotoNames.Add(preWorkFileName);
                }
            }

            if (log.postWorkPhotos != null && log.postWorkPhotos.Any())
            {
                foreach (var file in log.postWorkPhotos)
                {
                    var postWorkFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                    var postWorkFilePath = Path.Combine(_hostEnvironment.WebRootPath, "images", postWorkFileName);
                    using (var stream = new FileStream(postWorkFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    postWorkPhotoNames.Add(postWorkFileName);
                }
            }

            log.preWorkPhotosPath = string.Join(",", preWorkPhotoNames);
            log.postWorkPhotosPath = string.Join(",", postWorkPhotoNames);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var landscapingLog = await _landscapinglogsService.GetLandscapingLogAsync(id);
            if (landscapingLog == null)
            {
                return NotFound();
            }
            return View(landscapingLog);
        }

        // POST: LandscapingLog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LandscapingLogsDTO landscapingLog)
        {
            if (id != landscapingLog.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await HandleFileUploads(landscapingLog);

                await _landscapinglogsService.UpdateLandscapingLogAsync(landscapingLog);
                return RedirectToAction(nameof(Index));
            }

            return View(landscapingLog);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _landscapinglogsService.DeleteAsync(id); // Implement this method in your service
            return RedirectToAction(nameof(Index));
        }
    }
   
    }





  

