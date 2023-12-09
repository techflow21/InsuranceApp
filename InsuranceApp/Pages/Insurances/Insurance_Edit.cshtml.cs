using InsuranceApp.Data;
using InsuranceApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InsuranceApp.Pages.Insurances
{
    public class Insurance_EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public Insurance_EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Insurance Insurance { get; set; } = new Insurance();

        [BindProperty]
        public List<Motor> Motors { get; set; } = new List<Motor>();


        [BindProperty(SupportsGet = true)]
        public long Id { get; set; }

        /*[BindProperty]
        public Motor Motor { get; set; }*/

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == 0)
            {
                Insurance = new Insurance();

                //Motor = new Motor();
            }
            else
            {
                Insurance = await _context.Insurances
                    .Include(i => i.Motors)
                    .FirstOrDefaultAsync(i => i.Id == Id);

                if (Insurance == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Insurance.Id == 0) // New Insurance
            {
                _context.Insurances.Add(Insurance);
                _context.Motors.AddRange(Insurance.Motors);
            }
            else // Updating existing Insurance
            {
                _context.Attach(Insurance).State = EntityState.Modified;
            }

            // Save Motor details
            foreach (var motor in Insurance.Motors)
            {
                if (motor.Id == 0) // New Motor
                {
                    motor.Insurance = Insurance;
                    _context.Motors.Add(motor);
                }
                else // Updating existing Motor
                {
                    _context.Attach(motor).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Motor Policy saved successfully!";
            return RedirectToPage("/Insurances/Insurance_List");
        }
    }






    /*public class Insurance_EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public Insurance_EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Insurance Insurance { get; set; } = new Insurance();

        [BindProperty(SupportsGet = true)]
        public long Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == 0)
            {
                Insurance = new Insurance();
            }
            else
            {
                Insurance = await _context.Insurances
                    .Include(i => i.Motors)
                    .FirstOrDefaultAsync(i => i.Id == Id);

                if (Insurance == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Insurance.Id == 0) // New Insurance
            {
                _context.Insurances.Add(Insurance);
                _context.Motors.AddRange(Insurance.Motors);
            }
            else // Updating existing Insurance
            {
                _context.Attach(Insurance).State = EntityState.Modified;
            }

            // Save Motor details
            foreach (var motor in Insurance.Motors)
            {
                if (motor.Id == 0) // New Motor
                {
                    motor.Insurance = Insurance;
                    _context.Motors.Add(motor);
                }
                else // Updating existing Motor
                {
                    _context.Attach(motor).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Motor Policy saved successfully!";
            return RedirectToPage("/Insurances/Insurance_List");
        }
    }*/

}
