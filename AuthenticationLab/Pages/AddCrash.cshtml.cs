using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationLab.Infrastructure;
using AuthenticationLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationLab.Pages
{
    public class AddCrashModel : PageModel
    {
        private ICrashRepository repo { get; set; }
        //public Basket basket { get; set; }
        public string ReturnUrl { get; set; }

        public AddCrashModel(ICrashRepository temp /*Basket b*/)
        {
            repo = temp;
            //basket = b;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int crashId, string returnUrl)
        {
            Crash c = repo.mytable.FirstOrDefault(x => x.CRASH_ID == crashId);


            //basket.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            //basket.RemoveItem(basket.Items.First(x => x.Book.BookId == bookId).Book);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
