using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smart_Warehouse.Pages.Account_Management.Password
{
    public class DisplayResetTokenModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Token { get; set; } = string.Empty;

        public void OnGet()
        {
        }
    }
}
