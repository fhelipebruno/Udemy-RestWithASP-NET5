using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy_RestWithASP_NET5.Business;
using Udemy_RestWithASP_NET5.Data.Converter.VO;

namespace Udemy_RestWithASP_NET5.Controllers {
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    
    public class AuthController : ControllerBase {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness) {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user) {
            if (user == null) return BadRequest("Request inválida");
            var token = _loginBusiness.ValidateCredentials(user);
            if (token == null) {
                return Unauthorized();
            }
            return Ok(token);

        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null) return BadRequest("Request inválida");

            var token = _loginBusiness.ValidateCredentials(tokenVO);

            if (token == null)
            {
                return BadRequest("Request inválida");
            }
            return Ok(token);

        }
    }
}
