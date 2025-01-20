using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Namespace;  // Ensure correct namespace
using System.Collections.Generic;
using System.Linq;

namespace MyApp.Namespace
{
    public class BlogModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        public BlogModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public required List<BlogPost> Posts { get; set; }

        public void OnGet()
        {
            Posts = _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToList();
        }

        public IActionResult OnPost(string Title, string Content)
        {
            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Content))
            {
                _context.BlogPosts.Add(new BlogPost { Title = Title, Content = Content });
                _context.SaveChanges();
            }
            return RedirectToPage();
        }
    }
}
