using JobPortal2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal2.Controllers
{
    public class ApplyController : Controller
    {
        ProjectContext _context;
        public ApplyController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        
           
        
      
        //public async Task<IActionResult>Create()
        //{
        //    var email= HttpContext.Session.GetString("Email");
        //    Apply apply = new Apply();
            
        //    apply.JobSeekerEmail = email;
        //    var seekerdetails = await _context.Seekers.Where(x => x.JobSeekerEmail == email).FirstOrDefaultAsync();
        //    var jobdetails=await  _context.Jobs.Where(x => x.jobid==id).FirstOrDefaultAsync();
            
        //    apply.DateofApply = DateTime.Now;
        //    await _context.Applies.AddAsync(apply);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index","JobSeeker");
        //}
      
    }
}
