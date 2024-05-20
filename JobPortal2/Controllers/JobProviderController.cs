using JobPortal2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace JobPortal2.Controllers
{
    public class JobProviderController : Controller
    {
        ProjectContext _context;
        IWebHostEnvironment _environment;
      public  JobProviderController(ProjectContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Register(JobProvider provider)
        {
            var jobprovider = await _context.Providers.Where(x => x.EmailId == provider.EmailId).FirstOrDefaultAsync();
            if(jobprovider!=null)
            {
                ViewBag.Message = "User already Exists";
                return View();
            }
            else
            {
                await _context.Providers.AddAsync(provider);
                await _context.SaveChangesAsync();
                return RedirectToAction("login");
            }

        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>login(JobProvider provider)
        {
            var jobprovider=await _context.Providers.Where(x=>x.EmailId==provider.EmailId && x.Password==provider.Password).FirstOrDefaultAsync();
            if(jobprovider!=null)
            {
                HttpContext.Session.SetString("email", provider.EmailId);
                return RedirectToAction("Index", "Job");
            }
            else
            {
                ViewBag.Message = "Wrong Credential";
                return View();
            }
        }
        public async Task<IActionResult>jobseekerlist()
        {
            var email=HttpContext.Session.GetString("email");
            if(email==null)
            {
                return RedirectToAction("login");
            }
            var data=await _context.Seekers.ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> Download(string id)
        {

            var jobseeker = await _context.Seekers.Where(x => x.JobSeekerEmail == id).FirstOrDefaultAsync();

            if (jobseeker == null)
            {
                return NotFound();
            }
            string cv = jobseeker.CV;

            var dowwnloadpath = Path.Combine(_environment.WebRootPath, "UploadFile");
            var filePath = Path.Combine(dowwnloadpath, cv);


            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, "application/pdf", Path.GetFileName(filePath));
        }
     

    }
}
