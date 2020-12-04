using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Interfaces;
using GameLoanManager.Domain.ViewModels;
using GameLoanManager.Domain.ViewModels.User;

namespace GameLoanManager.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        private ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, ITokenService tokenService, IMapper mapper)
        {
            _repository = repository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<ResultViewModel> Authentication(LoginViewModel loginVM)
        {
            var user = new User();
            var result = new ResultViewModel
            {
                Success = false,
                Message = "Login ou senha inválido.",
                Data = loginVM
            };

            if (loginVM != null && !string.IsNullOrWhiteSpace(loginVM.Login))
            {
                user = await _repository.FindForAuthentication(loginVM.Login, loginVM.Password);

                if (user != null)
                {
                    var token = _tokenService.GenerateToken(user);

                    result.Success = true;
                    result.Message = "Usuário autenticado.";
                    result.Data = token;
                }
            }

            return result;
        }

        public async Task<ResultViewModel> Delete(long id)
        {
            var result = await _repository.DeleteAsync(id);

            return new ResultViewModel
            {
                Success = result == null ? false : true,
                Message = result == null ? "Usuário não encontrado" : "Usuário removido.",
                Data = _mapper.Map<UserViewModel>(result) ?? new UserViewModel()
            };
        }

        public async Task<UserViewModel> Get(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<UserViewModel>(entity) ?? new UserViewModel();
        }

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserViewModel>>(list);
        }

        public async Task<UserViewModel> GetMyGames(long id)
        {
            var entity = await _repository.GetMyGames(id);
            return _mapper.Map<UserViewModel>(entity) ?? new UserViewModel();
        }

        public async Task<ResultViewModel> Post(UserCreateViewModel user)
        {
            var entity = _mapper.Map<User>(user);
            var result = await _repository.InsertAsync(entity);

            return new ResultViewModel
            {
                Success = true,
                Message = "Usuário criado.",
                Data = _mapper.Map<UserViewModel>(result)
            };
        }

        public async Task<ResultViewModel> Put(UserUpdateViewModel user)
        {
            var entity = _mapper.Map<User>(user);
            var result = await _repository.UpdateAsync(entity);

            return new ResultViewModel
            {
                Success = true,
                Message = "Usuário modificado.",
                Data = _mapper.Map<UserViewModel>(result)
            };
        }
    }
}
