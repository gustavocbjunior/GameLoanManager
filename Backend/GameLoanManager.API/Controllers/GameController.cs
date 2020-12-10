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
            return Ok(await _service.GetAll());
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            return Ok(await _service.Get(id));
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("owner")]
        public async Task<ActionResult> GetAllWithRelationships()
        {
            return Ok(await _service.GetAllWithRelationships());
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("{id}/owner")]
        public async Task<ActionResult> GetByIdWithRelationships(long id)
        {
            return Ok(await _service.GetByIdWithRelationships(id));
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
            return Ok(await _service.GetByIdUser(idUser));
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

            var result = await _service.Post(model);

            return Ok(result);
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
    }
}