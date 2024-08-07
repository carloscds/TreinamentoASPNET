using Microsoft.AspNetCore.Mvc;

namespace API.Services.Controllers
{
    public class ControllerBaseAPI : ControllerBase
    {
        public string LoggedUser
        {
            get
            {
                return HttpContext.User.Claims.FirstOrDefault(w => w.Type.Contains("sub"))?.Value;
            }
        } 
    }
}
