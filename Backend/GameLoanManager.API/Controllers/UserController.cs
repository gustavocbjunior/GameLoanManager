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

            try
            {
                var result = await _service.Authentication(model);

                if (result.Success)
                    return result;
                else
                    return NotFound(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
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

            try
            {
                var result = await _service.Post(model);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
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

            try
            {
                var result = await _service.Put(model);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Authorization")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Authorization")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            try
            {
                return Ok(await _service.Get(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //[Authorize("Authorization")]
        [HttpGet]
        [Route("{id}/mygames")]
        public async Task<ActionResult> GetMyGames(long id)
        {
            try
            {
                return Ok(await _service.GetMyGames(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
