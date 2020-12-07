using System;
using System.Net;
using System.Threading.Tasks;
using GameLoanManager.Domain.Interfaces;
using GameLoanManager.Domain.ViewModels;
using GameLoanManager.Domain.ViewModels.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLoanManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private IGameService _service;
        private IUserService _serviceUser;
        private readonly IHttpContextAccessor _accessor;

        public GameController(IGameService service, IUserService serviceUser, IHttpContextAccessor accessor)
        {
            _service = service;
            _serviceUser = serviceUser;
            _accessor = accessor;
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

        [Authorize("Authorization")]
        [HttpGet]
        [Route("owner")]
        public async Task<ActionResult> GetAllWithRelationships()
        {
            try
            {
                return Ok(await _service.GetAllWithRelationships());
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("{id}/owner")]
        public async Task<ActionResult> GetByIdWithRelationships(long id)
        {
            try
            {
                return Ok(await _service.GetByIdWithRelationships(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("owner/{idUser}")]
        /// <summary>
        /// all a user's games
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetByIdUser(long idUser)
        {
            try
            {
                return Ok(await _service.GetByIdUser(idUser));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Authorization")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GameCreateViewModel model)
        {
            if (model.IdOwner == 0)
            {
                string authenticatedUserName = _accessor.HttpContext.User.Identity.Name;
                var user = await _serviceUser.GetByName(authenticatedUserName);
                model.IdOwner = user.Id;
            }

            if (model.Invalid)
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o jogo",
                    Data = model.Notifications
                });

            try
            {
                var result = await _service.Post(model);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Authorization")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] GameUpdateViewModel model)
        {
            if (model.IdOwner == 0)
            {
                string authenticatedUserName = _accessor.HttpContext.User.Identity.Name;
                var user = await _serviceUser.GetByName(authenticatedUserName);
                model.IdOwner = user.Id;
            }
            
            if (model.Invalid)
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível atualizar o jogo",
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
    }
}