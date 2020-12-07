using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Interfaces;
using GameLoanManager.Domain.ViewModels;
using GameLoanManager.Domain.ViewModels.Game;

namespace GameLoanManager.Service
{
    public class GameService : IGameService
    {
        private IGameRepository _repository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository repository, IMapper mapper)
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
                Message = result == null ? "Jogo não encontrado" : "Jogo removido.",
                Data = _mapper.Map<GameViewModel>(result) ?? new GameViewModel()
            };
        }

        public async Task<GameViewModel> Get(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<GameViewModel>(entity) ?? new GameViewModel();
        }

        public async Task<IEnumerable<GameViewModel>> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GameViewModel>>(list);
        }

        public async Task<IEnumerable<GameViewModel>> GetAllWithRelationships()
        {
            var list = await _repository.GetAllWithRelationships();
            return _mapper.Map<IEnumerable<GameViewModel>>(list);
        }

        public async Task<GameViewModel> GetByIdWithRelationships(long id)
        {
            var entity = await _repository.GetByIdWithRelationships(id);
            return _mapper.Map<GameViewModel>(entity) ?? new GameViewModel();
        }

        public async Task<GameViewModel> GetAvailable(long id)
        {
            var entity = await _repository.GetAvailable(id);
            return _mapper.Map<GameViewModel>(entity) ?? new GameViewModel();
        }

        public async Task<IEnumerable<GameViewModel>> GetAvailables()
        {
            var list = await _repository.GetAvailables();
            return _mapper.Map<IEnumerable<GameViewModel>>(list);
        }

        public async Task<IEnumerable<GameViewModel>> GetByIdUser(long idUser)
        {
            var list = await _repository.GetByIdUser(idUser);
            return _mapper.Map<IEnumerable<GameViewModel>>(list);
        }

        public async Task<ResultViewModel> Post(GameCreateViewModel game)
        {
            var entity = _mapper.Map<Game>(game);
            var result = await _repository.InsertAsync(entity);

            return new ResultViewModel
            {
                Success = true,
                Message = "Jogo criado.",
                Data = _mapper.Map<GameViewModel>(result)
            };
        }

        public async Task<ResultViewModel> Put(GameUpdateViewModel game)
        {
            var entity = _mapper.Map<Game>(game);
            var result = await _repository.UpdateAsync(entity);

            return new ResultViewModel
            {
                Success = true,
                Message = "Jogo modificado.",
                Data = _mapper.Map<GameViewModel>(result)
            };
        }

    }
}