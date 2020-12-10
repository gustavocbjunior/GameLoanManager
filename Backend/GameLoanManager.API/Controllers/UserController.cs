using System;
using System.Net;
using System.Threading.Tasks;
using GameLoanManager.Domain.Interfaces;
using GameLoanManager.Domain.ViewModels;
using GameLoanManager.Domain.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameLoanManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authentication")]
        public async Task<Object> Login([FromBody] LoginViewModel model)
        {
            if (model.Invalid)
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = "Login ou senha inválidos"
                });

            var result = await _service.Authentication(model);

            if (result.Success)
                return result;
            else
                return NotFound(result);

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserCreateViewModel model)
        {
            if (model.Invalid)
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o usuário",
                    Data = model.Notifications
                });

            var result = await _service.Post(model);

            return Ok(result);
        }

        [Authorize("Authorization")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserUpdateViewModel model)
        {
            if (model.Invalid)
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível atualizar o usuário",
                    Data = model.Notifications
                });

            var result = await _service.Put(model);

            return Ok(result);

        }

        [Authorize("Authorization")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            return Ok(await _service.Delete(id));
        }

        [Authorize("Authorization")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            return Ok(await _service.Get(id));
        }

        //[Authorize("Authorization")]
        [HttpGet]
        [Route("{id}/mygames")]
        public async Task<ActionResult> GetMyGames(long id)
        {
            return Ok(await _service.GetMyGames(id));
        }
    }
}
