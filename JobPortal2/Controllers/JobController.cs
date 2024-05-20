using JobPortal2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobPortal2.Controllers
{
    public class JobController : Controller
    {
        ProjectContext _context;
        public JobController(ProjectContext context) { 
        
              _context = context;
        }
        public async  Task <IActionResult> Index()
        {
            var Email = HttpContext.Session.GetString("email");
            if (Email == null)
            {
                return RedirectToAction("login","Jobprovider");
            }
            else {
                var data = await (from e1 in _context.Jobs join e2 in _context.Providers on
                                e1.EmailId equals e2.EmailId where e1.EmailId==Email select new Job
                                {
                                    EmailId = e1.EmailId,
                                    JobDescription = e1.JobDescription,
                                    category = e1.category,
                                    jobid=e1.jobid,
                                    ExperienceRequired = e1.ExperienceRequired,
                                    ApplyDate = e1.ApplyDate

                                }).ToListAsync();

                                return View(data);
                            }
            
        }
        public IActionResult Create()
        {
            var Email=HttpContext.Session.GetString("email");
            if (Email == null)
            {
                return RedirectToAction("login","JobProvider");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(Job job)
        {
            job.EmailId =HttpContext.Session.GetString("email");
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Edit(int id)
        {
            var email=HttpContext.Session.GetString("email");
            if (email == null)
            {
                return RedirectToAction("login", "JobProvider");
            }
            else
            {
                var data=await _context.Jobs.Where(x=>x.jobid==id).FirstOrDefaultAsync();
                return View(data);
            }
        }
        [HttpPost]
        public async Task<IActionResult>Edit(Job job)
        {
            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Apply(int id)
            {
            var Email=HttpContext.Session.GetString("Email");
            if (Email == null)
            {
                return RedirectToAction("Login", "JobSeeker");
            }

            else
            {
                var data = await _context.Jobs.Where(x => x.jobid == id).FirstOrDefaultAsync();
                Apply aplly = new Apply();
                var existinguser=await _context.Applies.Where(x=>x.Jobid==id && x.JobSeekerEmail==Email).FirstOrDefaultAsync();
                if (existinguser != null)
                {

                    TempData["Error"] = "you have already applied for the job.";
                    return RedirectToAction("Index", "JobSeeker");
                }
                aplly.Jobid = id;
                var jobSeeker=await _context.Seekers.Where(x=>x.JobSeekerEmail==Email).FirstOrDefaultAsync();
                aplly.JobSeekerEmail = Email;
                aplly.DateofApply = DateTime.Now;
                await _context.Applies.AddAsync(aplly);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "JobSeeker");
            }
        
        
        }
        [HttpGet]
        //public async Task<IActionResult>Applicants(int id)
        //{
        //    var email=HttpContext.Session.GetString("email");
        //    if(email == null)
        //    {
        //        return RedirectToAction("login", "JobProvider");
        //    }
        //    else
        //    {
                
        //        var job=await _context.Jobs.Where(x=>x.jobid==id).FirstOrDefaultAsync();    
        //        if(job== null || job.EmailId !=email)
        //        {
        //            return NotFound();
        //        }
        //        var applicants=await _context.Jobs.Where(x=>x.jobid==id && x.EmailId==email).ToListAsync();
        //        return View(applicants);
        //    }
        //}
        public async Task<IActionResult> Appliedperson(int id)
        {
            //  var data=await(from e1 in _context.Applies join e2 in
            //                _context.Jobs on e1.Jobid equals e2.jobid
            //                join e3 in _context.Seekers on e1.JobSeekerEmail equals e3.JobSeekerEmail
            //               select e3





            //                 ).ToListAsync();
            var data = await (from e1 in _context.Applies
                              where e1.Jobid == id

                              select e1).ToListAsync();
            return View(data);

        }



    }
}
