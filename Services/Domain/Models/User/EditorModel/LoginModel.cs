using System.ComponentModel.DataAnnotations;
using Core.ViewService.Models;

namespace Services.Domain.Models.User.EditorModel
{
    public class LoginModel : BaseEditorModel
    {
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string api_key { get; set; }
    }
}