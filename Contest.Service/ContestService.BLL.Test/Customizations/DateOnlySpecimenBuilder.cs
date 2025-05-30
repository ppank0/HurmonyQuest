using AutoFixture;
using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Tests.Customizations;
public class DateOnlySpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is not Type type || type != typeof(DateOnly))
            return new NoSpecimen();

        var dateTime = context.Create<DateTime>().Date;

        if(dateTime.Year is < 1960 or < 2030)
            dateTime = new DateTime(2000, 1, 1);

        return DateOnly.FromDateTime(dateTime);
    }
}
