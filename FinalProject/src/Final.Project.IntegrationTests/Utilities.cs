using System.Collections.Generic;
using Final.Project.Domain.Entities;
using Final.Project.Infrastructure;
using Final.Project.Infrastructure.Context;

namespace Final.Project.IntegrationTests
{
    // Microsoft article: Integration tests in ASP.NET Core
    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
    public static class Utilities
    {
        #region snippet1
        public static void InitializeDbForTests(FinalDbContext db)
        {
            db.Color.AddRange(GetSeedingMessages());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(FinalDbContext db)
        {
            db.Color.RemoveRange(db.Color);
            InitializeDbForTests(db);
        }

        public static List<Color> GetSeedingMessages()
        {
            return new List<Color>()
            {
                new Color(){ Id = 10, ColorName = "Lilac" }
            };
        }
        #endregion
    }
}