using Application.Services.AutoMapper;
using AutoMapper;

namespace CommomTestsUtilities.Mapper
{
    public static class MapperBuilder
    {
        public static IMapper Build()
        {
            return new AutoMapper.MapperConfiguration(opt =>
            {
                opt.AddProfile(new AutoMapping());
            }).CreateMapper();
        }
    }
}
