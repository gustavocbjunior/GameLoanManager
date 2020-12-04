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
        [Route("{id}")]
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
        [Route("returned")]
        public async Task<ActionResult> GetReturned()
        {
            try
            {
                return Ok(await _service.GetReturnedWithRelationships());
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("user/{idUser}")]
        public async Task<ActionResult> GetByUser(long idUser)
        {
            try
            {
                return Ok(await _service.GetByUser(idUser));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Authorization")]
        [HttpGet]
        [Route("game/{idGame}")]
        public async Task<ActionResult> GetByGame(long idGame)
        {
            try
            {
                return Ok(await _service.GetByGame(idGame));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Authorize("Authorization")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LoanedGameCreateViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o empréstimo do jogo",
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