using System.Linq;
using AutoMapper;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.ViewModels.Game;
using GameLoanManager.Domain.ViewModels.LoanedGame;
using GameLoanManager.Domain.ViewModels.User;

namespace GameLoanManager.Helpers.Mapping
{
    public class EntityToViewModel : Profile
    {
        public EntityToViewModel()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Games, opts => opts.MapFrom(x => x.MyGames));
            CreateMap<User, UserCreateViewModel>()
                .ForPath(dest => dest.Email.Address, opts => opts.MapFrom(x => x.Email));
            CreateMap<User, UserUpdateViewModel>()
                .ForPath(dest => dest.Email.Address, opts => opts.MapFrom(x => x.Email));

            CreateMap<Game, GameViewModel>()
                .ForPath(dest => dest.IdOwner, opts => opts.MapFrom(x => x.IdUser))
                .ForPath(dest => dest.Owner, opts => opts.MapFrom(x => x.User.Name));
            CreateMap<Game, GameCreateViewModel>()
                .ForPath(dest => dest.IdOwner, opts => opts.MapFrom(x => x.IdUser)); 
            CreateMap<Game, GameUpdateViewModel>()
                .ForPath(dest => dest.IdOwner, opts => opts.MapFrom(x => x.IdUser));

            CreateMap<LoanedGame, LoanedGameViewModel>()
                .ForMember(dest => dest.Game, opts => opts.MapFrom(x => x.Game.Name))
                .ForMember(dest => dest.User, opts => opts.MapFrom(x => x.User.Name)); 
            CreateMap<LoanedGame, LoanedGameCreateViewModel>();
            CreateMap<LoanedGame, LoanedGameUpdateViewModel>();
            
        }
    }
}