using Jlw.Extensions.Identity.Mock;
using Jlw.Utilities.Data;
using Microsoft.AspNetCore.Identity;

namespace SampleWebApplication
{
    public static class MockedUserExtensions
    {

        public static IServiceCollection AddMockedUsers<TUser>(this IServiceCollection services) where TUser : Jlw.Extensions.Identity.Stores.ModularBaseUser, new()
        {
            int id = 1;
            string temp;

            temp = "testuser@test.org";
            MockUserStore<TUser>.AddMockedUser(
                GetNewUser<TUser>(new
                {
                    Id = id++,
                    UserName = temp,
                    NormalizedUserName = temp.ToUpper(),
                    Email = temp,
                    NormalizedEmail = temp.ToUpper(),
                    PasswordHash = "test",
                    EmailConfirmed = true
                }),
                new IdentityUserClaim<string>[] { }
            );

            temp = "teststaff@test.org";
            MockUserStore<TUser>.AddMockedUser(
                GetNewUser<TUser>(new
                {
                    Id = id++,
                    UserName = temp,
                    NormalizedUserName = temp.ToUpper(),
                    Email = temp,
                    NormalizedEmail = temp.ToUpper(),
                    PasswordHash = "test",
                    EmailConfirmed = true
                }),
                new IdentityUserClaim<string>[] {
                    new IdentityUserClaim<string>()
                    {

                        ClaimType = "Claim1",
                        ClaimValue = DataUtility.GenerateRandom<string>(),

                    }
                }
            );

            temp = "testadmin@test.org";
            MockUserStore<TUser>.AddMockedUser(
                GetNewUser<TUser>(new
                {
                    Id = id++,
                    UserName = temp,
                    NormalizedUserName = temp.ToUpper(),
                    Email = temp,
                    NormalizedEmail = temp.ToUpper(),
                    PasswordHash = "test",
                    EmailConfirmed = true
                }),
                new IdentityUserClaim<string>[] {

                    new IdentityUserClaim<string>()
                    {
                        ClaimType = "Claim1",
                        ClaimValue = DataUtility.GenerateRandom<string>(),
                    },
                }
            );

            temp = "testsuper@test.org";
            MockUserStore<TUser>.AddMockedUser(
                GetNewUser<TUser>(new
                {
                    Id = id++,
                    UserName = temp,
                    NormalizedUserName = temp.ToUpper(),
                    Email = temp,
                    NormalizedEmail = temp.ToUpper(),
                    PasswordHash = "test",
                    EmailConfirmed = true
                }),
                new IdentityUserClaim<string>[] {

                    new IdentityUserClaim<string>()
                    {
                        ClaimType = "Claim1",
                        ClaimValue = DataUtility.GenerateRandom<string>(),
                    },
                    new IdentityUserClaim<string>()
                    {
                        ClaimType = "Claim2",
                        ClaimValue = DataUtility.GenerateRandom<string>(),
                    },

                }
            );

            return services;
        }

        public static T GetNewUser<T>(object o)
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(object) });
            if (constructor is null)
                return default;

            return (T)constructor.Invoke(new[] { o });
        }

    }
}
