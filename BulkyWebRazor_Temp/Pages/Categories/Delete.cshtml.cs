using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int id)
        {
            Category = _context.Categories.Find(id); 
        }

        public IActionResult OnPost(int? id)
        {
            var model = _context.Categories.Find(id);
            if (model is null)
                return NotFound();
            _context.Categories.Remove(model);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
