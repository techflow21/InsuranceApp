using InsuranceApp.Data;
using InsuranceApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApp.Pages.Insurances
{
    public class Insurance_ListModel : PageModel
    {
        private readonly AppDbContext _context;

        public Insurance_ListModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<Insurance> Insurances { get; set; }

        [BindProperty(SupportsGet = true)]
        public long Id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Insurances = await _context.Insurances
                .Include(i => i.Motors)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            var insurance = await _context.Insurances.FindAsync(id);

            if (insurance != null)
            {
                _context.Insurances.Remove(insurance);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Insurances/Insurance_List");
        }
    }
}
