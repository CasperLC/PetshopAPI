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

            string password = "1234";
            byte[] passwordHashUserOne, passwordSaltUserOne, passwordHashUserTwo, passwordSaltUserTwo;

            CreatePasswordHash(password, out passwordHashUserOne, out passwordSaltUserOne);
            CreatePasswordHash(password, out passwordHashUserTwo, out passwordSaltUserTwo);
            

            var user1 = ctx.Users.Add(new User()
            {
                Username = "Non-AdminUser",
                PasswordHash = passwordHashUserOne,
                PasswordSalt = passwordSaltUserOne,
                IsAdmin = false,
                Owner = owner1,
                OwnerRef = owner1.Id
            }).Entity;

            var user2 = ctx.Users.Add(new User()
            {
                Username = "AdminUser",
                PasswordHash = passwordHashUserTwo,
                PasswordSalt = passwordSaltUserTwo,
                IsAdmin = true,
                Owner = owner2,
                OwnerRef = owner2.Id
            }).Entity;

            ctx.SaveChanges();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
