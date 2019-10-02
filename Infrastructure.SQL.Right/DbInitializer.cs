using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.SQL.Right
{
    public class DbInitializer
    {
        public static void SeedDB(PetAppContext ctx)
        {
            //ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var owner1 = ctx.Owners.Add(new Owner()
            {
                Name = "OwnerPerson1"
            }).Entity;

            var owner2 = ctx.Owners.Add(new Owner()
            {
                Name = "OwnerPerson2"
            }).Entity;

            var pet1 = ctx.Pets.Add(new Pet()
            {
                BirthDate = DateTime.Now,
                Price = 1,
                SoldDate = DateTime.MinValue,
                Name = "Dyr",
                Color = "Black",
                Type = "Dog",
                PreviousOwners = new List<PetOwner>
                {
                    new PetOwner()
                    {
                        Owner = owner1
                    },
                    new PetOwner()
                    {
                        Owner = owner2
                    }
                }
            }).Entity;

            ctx.SaveChanges();
        }
    }
}
