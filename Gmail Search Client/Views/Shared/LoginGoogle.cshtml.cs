using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gmail_Search_Client.Controllers.GoogleUseCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Gmail_Search_Client.Views.Shared
{
    public class LoginModel : PageModel
    {
        public LoginModel()
        {
         
        }

        public IActionResult OnGet(string code)
        {
            return Page();
        }
    }
}
