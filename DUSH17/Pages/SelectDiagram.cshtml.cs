using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DUSH17.Pages
{

    public class SelectDiagramModel : PageModel
    {
		public int Id;
		public void OnGet(int id)
        {
            Id = id;
        }
    }
}
