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
  

    public class CleaningLogsController : Controller
    {
        private readonly CleaningLogsService _cleaninglogsService;
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CleaningLogsController(CleaningLogsService cleaninglogsService, IWebHostEnvironment hostEnvironment)
        {
            _cleaninglogsService = cleaninglogsService;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var cleaningLogs = await _cleaninglogsService.GetCleaningLogsAsync();
            foreach (var house in cleaningLogs)
            {
                Debug.WriteLine($"House ID: {house.id}");
            }
            return View(cleaningLogs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var house = await _cleaninglogsService.GetCleaningLogsAsync(id);
            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CleaningLogsDTO house)
        {
            //if (ModelState.IsValid)
            //{
                var photoFileNames = new List<string>();

                if (house.photoFiles != null && house.photoFiles.Any())
                {
                    foreach (var file in house.photoFiles)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                            var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            photoFileNames.Add(fileName); // Add file name to the list
                        }
                    }

                    house.photos = string.Join(",", photoFileNames); // Join the file names into a comma-separated string
                }

                // Pass house to service method
                await _cleaninglogsService.CreateCleaningLogsAsync(house);

                return RedirectToAction(nameof(Index));
            //}

            return View(house);
        }

        // Implement Edit and Delete actions similar to Create

        public async Task<IActionResult> Edit(int id)
        {
            var house = await _cleaninglogsService.GetCleaningLogsAsync(id);
            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CleaningLogsDTO house)
        {
            if (id != house.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var photoFileNames = new List<string>();

                if (house.photoFiles != null && house.photoFiles.Any())
                {
                    foreach (var file in house.photoFiles)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                            var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            photoFileNames.Add(fileName); // Add file name to the list
                        }
                    }

                    house.photos = string.Join(",", photoFileNames); // Join the file names into a comma-separated string
                }

                await _cleaninglogsService.UpdateCleaningLogsAsync(house); // Implement this method in your service
                return RedirectToAction(nameof(Index));
            }

            return View(house);
        }

        public async Task<IActionResult> Delete(int id)
            {
            await _cleaninglogsService.DeleteAsync(id); // Implement this method in your service
            return RedirectToAction(nameof(Index));
            }

            


    }
}
