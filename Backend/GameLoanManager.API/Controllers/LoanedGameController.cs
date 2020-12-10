using System;
using System.Net;
using System.Threading.Tasks;
using GameLoanManager.Domain.Interfaces;
using GameLoanManager.Domain.ViewModels;
using GameLoanManager.Domain.ViewModels.LoanedGame;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameLoanManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanedGameController : ControllerBase
    {
        private ILoanedGameService _service;

        public LoanedGameController(ILoanedGameService service)
        {
            _service = service;
        }

        [Authorize("Authorization")]
        [HttpGet]
        public async Task<ActionResult> GetAllWithRelationships()
        {
            return Ok(await _service.GetAllWithRelationships());
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetByIdWithRelationships(long id)
        {
            return Ok(await _service.GetByIdWithRelationships(id));
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("returned")]
        public async Task<ActionResult> GetReturned()
        {
            return Ok(await _service.GetReturnedWithRelationships());
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("user/{idUser}")]
        public async Task<ActionResult> GetByUser(long idUser)
        {
            return Ok(await _service.GetByUser(idUser));
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("game/{idGame}")]
        public async Task<ActionResult> GetByGame(long idGame)
        {
            return Ok(await _service.GetByGame(idGame));
        }

        [Authorize("Authorization")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LoanedGameCreateViewModel model)
        {
            if (model.Invalid)
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o empréstimo do jogo",
                    Data = model.Notifications
                });

            var result = await _service.Post(model);

            return Ok(result);
        }

        [Authorize("Authorization")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] LoanedGameUpdateViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível atualizar o empréstimo jogo",
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