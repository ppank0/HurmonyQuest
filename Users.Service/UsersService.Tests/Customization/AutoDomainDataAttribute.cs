using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using AutoMapper;
using UsersService.Tests.Mapping;

namespace UsersService.Tests.Customization
{
    public class AutoDomainDataAttribute : AutoDataAttribute
    {
        public AutoDomainDataAttribute() : base(() =>
        {
            var fixture = new Fixture()
                .Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UsersProfileTest>());

            fixture.Inject<IMapper>(mapper.CreateMapper());

            return fixture;
        })
        { }
    }
}
