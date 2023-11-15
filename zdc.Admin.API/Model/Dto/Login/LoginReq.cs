using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Dto.Login
{
    public class LoginReq
    {
        [Required]
        //[DefaultValue("admin")]
        public string Uid { get; set; }
        [Required]
        //[DefaultValue("123456")]
        public string Pwd { get; set; }
    }
}