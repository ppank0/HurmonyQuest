using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace ContestService.BLL.Tests.Customizations;
public class CustomAutoDataAttribute : AutoDataAttribute
{
    public CustomAutoDataAttribute() : base(() =>
    {
        var fixture = new Fixture()
            .Customize(new AutoMoqCustomization { ConfigureMembers = true });

        fixture.Customizations.Add(new DateOnlySpecimenBuilder());

        return fixture;
    })
    { }
}
