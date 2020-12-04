using AutoMapper;

namespace GameLoanManager.Helpers.Mapping
{
    public class MapperConfiguration
    {
        public static IMapper Configure()
        {
            var config = new AutoMapper.MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityToViewModel());
                mc.AddProfile(new ViewModelToEntity());
                
            });

            return config.CreateMapper();
        }
    }
}