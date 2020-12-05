using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Interfaces;
using GameLoanManager.Domain.ViewModels;
using GameLoanManager.Domain.ViewModels.LoanedGame;

namespace GameLoanManager.Service
{
    public class LoanedGameService : ILoanedGameService
    {
        private ILoanedGameRepository _repository;
        private IGameRepository _repositoryGame;
        private readonly IMapper _mapper;

        public LoanedGameService(ILoanedGameRepository repository, IMapper mapper, IGameRepository repositoryGame)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryGame = repositoryGame;
        }

        public async Task<ResultViewModel> Delete(long id)
        {
            var result = await _repository.DeleteAsync(id);

            return new ResultViewModel
            {
                Success = result == null ? false : true,
                Message = result == null ? "empréstimo de jogo não encontrado" : "empréstimo de jogo removido.",
                Data = _mapper.Map<LoanedGameViewModel>(result) ?? new LoanedGameViewModel()
            };
        }

        public async Task<LoanedGameViewModel> Get(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<LoanedGameViewModel>(entity) ?? new LoanedGameViewModel();
        }

        public async Task<IEnumerable<LoanedGameViewModel>> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<LoanedGameViewModel>>(list);
        }

        public async Task<IEnumerable<LoanedGameViewModel>> GetAllWithRelationships()
        {
            var list = await _repository.GetAllWithRelationshipsAsync();
            return _mapper.Map<IEnumerable<LoanedGameViewModel>>(list);
        }

        public async Task<LoanedGameViewModel> GetByIdWithRelationships(long id)
        {
            var entity = await _repository.GetByIdWithRelationshipsAsync(id);
            return _mapper.Map<LoanedGameViewModel>(entity) ?? new LoanedGameViewModel();
        }

        public async Task<IEnumerable<LoanedGameViewModel>> GetReturnedWithRelationships()
        {
            var list = await _repository.GetReturnedWithRelationshipsAsync();
            return _mapper.Map<IEnumerable<LoanedGameViewModel>>(list);
        }

        public async Task<IEnumerable<LoanedGameViewModel>> GetByUser(long idUser)
        {
            var list = await _repository.GetByUserAsync(idUser);
            return _mapper.Map<IEnumerable<LoanedGameViewModel>>(list);
        }

        public async Task<IEnumerable<LoanedGameViewModel>> GetByGame(long idGame)
        {
            var list = await _repository.GetByGameAsync(idGame);
            return _mapper.Map<IEnumerable<LoanedGameViewModel>>(list);
        }

        public async Task<ResultViewModel> Post(LoanedGameCreateViewModel loanedGame)
        {
            var entity = _mapper.Map<LoanedGame>(loanedGame);

            entity.Game = await _repositoryGame.GetByIdAsync(entity.IdGame);

            var result = new ResultViewModel
            {
                Success = false,
                Message = "Não foi possível criar o empréstimo."
            };

            if (!entity.IsValid())
            {
                result.Message += " Jogo indisponivel";
            }
            else
            {
                var insert = await _repository.InsertAsync(entity);

                entity.Game.Available = false;
                await _repositoryGame.UpdateAsync(entity.Game);

                result.Success = true;
                result.Message = "empréstimo de jogo criado.";
                result.Data = _mapper.Map<LoanedGameViewModel>(insert);
            }

            return result;
        }

        public async Task<ResultViewModel> Put(LoanedGameUpdateViewModel loanedGame)
        {
            var entity = _mapper.Map<LoanedGame>(loanedGame);
            var entityDB = await _repository.GetByIdWithRelationshipsAsync(loanedGame.Id);

            if (loanedGame.Returned && !entityDB.Returned)
            {
                entityDB.Game.Available = true;
                await _repositoryGame.UpdateAsync(entityDB.Game);
            }

            var result = await _repository.UpdateAsync(entity);

            return new ResultViewModel
            {
                Success = true,
                Message = "empréstimo de jogo modificado.",
                Data = _mapper.Map<LoanedGameViewModel>(result)
            };
        }

    }
}