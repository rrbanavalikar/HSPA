using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
    private readonly IUnitOfWork uow;

    public AccountController(IUnitOfWork uow)
    {
      this.uow = uow;
    }

//api/account/loginn
  [HttpPost("login")]
    public async Task<IActionResult> Login(LoginReqDto loginReq)
    {
      var user = await uow.UserRepository.Authenticate(loginReq.UserName, loginReq.Password);

      if(user == null)
      {
        return Unauthorized();
      }

      var loginRes = new LoginResDto();
      loginRes.UserName = user.Username;
      loginRes.Token = "Token to be generated";
      return Ok(loginRes);
    }

    }


}
