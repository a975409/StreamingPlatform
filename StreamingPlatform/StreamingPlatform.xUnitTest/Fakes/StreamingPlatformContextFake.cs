using Microsoft.EntityFrameworkCore;
using StreamingPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.xUnitTest.Fakes
{
    public class StreamingPlatformContextFake : StreamingPlatformContext
    {
        public StreamingPlatformContextFake() :
            base(new DbContextOptionsBuilder<StreamingPlatformContext>()
        .UseInMemoryDatabase(databaseName: $"AppointmentBookingTest-{Guid.NewGuid()}")
        .Options)
        { }
    }
}