using System.Linq;
using AutoMapper;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.ViewModels.Game;
using GameLoanManager.Domain.ViewModels.LoanedGame;
using GameLoanManager.Domain.ViewModels.User;

namespace GameLoanManager.Helpers.Mapping
{
    public class ViewModelToEntity : Profile
    {
        public ViewModelToEntity()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<UserViewModel, User>()
                .ForMember(x => x.MyGames, x => x.MapFrom(y => y.Games));
            CreateMap<UserCreateViewModel, User>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(x => x.Email.Address));
            CreateMap<UserUpdateViewModel, User>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(x => x.Email.Address));
            
            CreateMap<GameViewModel, Game>()
                .ForPath(dest => dest.IdUser, opts => opts.MapFrom(x => x.IdOwner));
                //.ForPath(dest => dest.Name, opts => opts.MapFrom(x => x.Owner));  
            CreateMap<GameCreateViewModel, Game>()
                .ForPath(dest => dest.IdUser, opts => opts.MapFrom(x => x.IdOwner)); 
            CreateMap<GameUpdateViewModel, Game>()
                .ForPath(dest => dest.IdUser, opts => opts.MapFrom(x => x.IdOwner)); 

            CreateMap<LoanedGameViewModel, LoanedGame>()
                .ForPath(dest => dest.Game.Name, opts => opts.MapFrom(x => x.Game))
                .ForPath(dest => dest.User.Name, opts => opts.MapFrom(x => x.User)); 
            CreateMap<LoanedGameCreateViewModel, LoanedGame>();
            CreateMap<LoanedGameUpdateViewModel, LoanedGame>();
        }
    }
}