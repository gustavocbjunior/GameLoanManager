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
        private readonly IMapper _mapper;

        public LoanedGameService(ILoanedGameRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            var result = await _repository.InsertAsync(entity);

            return new ResultViewModel
            {
                Success = true,
                Message = "empréstimo de jogo criado.",
                Data = _mapper.Map<LoanedGameViewModel>(result)
            };
        }

        public async Task<ResultViewModel> Put(LoanedGameUpdateViewModel loanedGame)
        {
            var entity = _mapper.Map<LoanedGame>(loanedGame);
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