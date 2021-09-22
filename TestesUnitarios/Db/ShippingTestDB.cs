using Microsoft.EntityFrameworkCore;
using ShippingAPI;
using System;

namespace TestesUnitarios.Db
{
    public class ShippingTestDB : IDisposable
    {
        protected readonly ShippingContext ShippingContext;
        public ShippingTestDB()
        {
            var options = new DbContextOptionsBuilder<ShippingContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            ShippingContext = new ShippingContext(options);
            ShippingContext.Database.EnsureCreated();
            ShippingInitializer.Initialize(ShippingContext);
        }
        public void Dispose()
        {
            ShippingContext.Database.EnsureDeleted();
            ShippingContext.Dispose();
        }
    }
}
