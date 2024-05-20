using JobPortal2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal2.Controllers
{
    public class JobSeekerController : Controller
    {
        ProjectContext _context;
        IWebHostEnvironment _environment;
        public JobSeekerController(ProjectContext context,IWebHostEnvironment environment) { 
        
        
           _context = context;
            _environment = environment;
        }
        public async Task <IActionResult> Index()
        {
            var email = HttpContext.Session.GetString("Email");
            if(email==null)
            {
                return RedirectToAction("login");
            }
           var Expiredate=await _context.Jobs.Where(x=>x.ApplyDate>DateTime.Now).ToListAsync();
            if (TempData["Error"]!=null)
            {
                ViewBag.message = "you cant apply for this job";
            }
            return View(Expiredate);
           
            
        }
        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Register(JobSeeker seeker)
        {
            var Jobseeker=await _context.Seekers.Where(x=>x.JobSeekerEmail==seeker.JobSeekerEmail).FirstOrDefaultAsync();
            if (Jobseeker == null)
            {
                seeker.CV = UploadFile(seeker);
                await _context.Seekers.AddAsync(seeker);
                await _context.SaveChangesAsync();
            }
            else
            {
                return View();
            }
            return RedirectToAction("login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(JobSeeker seeker)
        {
            var Jobseeker = await _context.Seekers.Where(x => x.JobSeekerEmail == seeker.JobSeekerEmail && x.Password == seeker.Password).FirstOrDefaultAsync();
            if(Jobseeker!=null)
            {
                HttpContext.Session.SetString("Email", seeker.JobSeekerEmail);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        string UploadFile(JobSeeker seeker1)
        {
            string FileName = null;
            if(seeker1.UploadCV!=null)
            {
                if (seeker1.UploadCV.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    string Uploadfile = Path.Combine(_environment.WebRootPath, "UploadFile");
                    FileName = Guid.NewGuid().ToString() + seeker1.UploadCV.FileName;
                    string Filepath = Path.Combine(Uploadfile, FileName);
                    using var Filestream = new FileStream(Filepath, FileMode.Create);
                    {
                        seeker1.UploadCV.CopyTo(Filestream);
                    }

                }

            }
            return FileName;
        }
       

    }
}
