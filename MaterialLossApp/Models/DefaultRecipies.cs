using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MaterialLossApp.Models
{
    public static class DefaultRecipies
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            List<Ingredient> Ingredients = new List<Ingredient>()
            {
                new Ingredient {Id = 1, MaterialNumber = 4405021, SectionName = "Składniki", Name = "Cukier"},
                new Ingredient {Id = 2, MaterialNumber = 4431245, SectionName = "Składniki", Name = "Mleko zagęszczone"},
                new Ingredient {Id = 3, MaterialNumber = 4460655, SectionName = "Składniki", Name = "Odpieniacz"},
                new Ingredient {Id = 4, MaterialNumber = 4433212, SectionName = "Składniki", Name = "Konserwant"},
                new Ingredient {Id = 5, MaterialNumber = 4477132, SectionName = "Składniki", Name = "Aromat Krówka"},
                new Ingredient {Id = 6, MaterialNumber = 4498443, SectionName = "Składniki", Name = "Truskawka"},
                new Ingredient {Id = 7, MaterialNumber = 4458216, SectionName = "Składniki", Name = "Skrobia modyfikowana"},
                new Ingredient {Id = 8, MaterialNumber = 4465543, SectionName = "Składniki", Name = "Aromat truskawka"},
                new Ingredient {Id = 9, MaterialNumber = 4494328, SectionName = "Składniki", Name = "Wiśnia"},
                new Ingredient {Id = 10, MaterialNumber = 4465503, SectionName = "Składniki", Name = "Aromat wiśnia"},
                new Ingredient {Id = 11, MaterialNumber = 4475934, SectionName = "Składniki", Name = "Guma Xantan"},
                new Ingredient {Id = 12, MaterialNumber = 4416630, SectionName = "Składniki", Name = "Aromat waniliowy"},
                new Ingredient {Id = 13, Name = "Woda", SectionName = "Składniki" },
                new Ingredient {Id = 14, MaterialNumber = 4409530, SectionName = "Składniki", Name = "Syrop glukozowy"},

                new Ingredient{ Id = 15, MaterialNumber = 4439904, SectionName = "Opakowania", Use = "Container", Name = "Butelka czarna 1 kg", Capacity = 1},
                new Ingredient{ Id = 16, MaterialNumber = 4477398, SectionName = "Opakowania", Use = "Container", Name = "Wiadro białe 10 kg", Capacity = 10},
                new Ingredient{ Id = 17, MaterialNumber = 4033456, SectionName = "Opakowania", Use = "Container", Name = "Wiadro czerwone 3.2 kg", Capacity = 3.2},
                new Ingredient{ Id = 18, MaterialNumber = 4499540, SectionName = "Opakowania", Use = "Cap", Name = "Nakrentka RD50"},
                new Ingredient{ Id = 19, MaterialNumber = 4432324, SectionName = "Opakowania", Use = "Cap", Name = "Wieczko niebeiske średnica 18 cm (3.2 kg)"},
                new Ingredient{ Id = 20, MaterialNumber = 4466950, SectionName = "Opakowania", Use = "Cap", Name = "Wieczko białe średnica 32 cm (10 kg)"},
                new Ingredient{ Id = 21, MaterialNumber = 4436904, SectionName = "Opakowania", Use = "Label", Name = "Naklejka 100x100 biała"},
                new Ingredient{ Id = 22, MaterialNumber = 4410932, SectionName = "Opakowania", Use = "Label", Name = "Naklejka Truskawka w żelu 3.2 kg"},
                new Ingredient{ Id = 23, MaterialNumber = 4490437, SectionName = "Opakowania", Use = "Label", Name = "Naklejka Wiśnia w żelu 3.2 kg"},
                new Ingredient{ Id = 24, MaterialNumber = 4400475, SectionName = "Opakowania", Use = "Label", Name = "Naklejka Sos Krówka 1 kg"}

            };
            List<Recipe> Recipies = new List<Recipe>()
           {
               new Recipe{Id = 1, Name = "Sos krówka"},
               new Recipe {Id = 2, Name = "Truskawka w żelu"},
               new Recipe {Id = 3, Name = "Wiśnia w żelu"},
               new Recipe {Id = 4, Name = "Nadzienie waniliowe"}
           };

            List<Relation> relations = new List<Relation>()
           {
               new Relation{Id = 1, IngredientsId = 1, RecipeId = 1, Amount = 162},
               new Relation{Id = 2, IngredientsId = 2, RecipeId = 1, Amount = 430},
               new Relation{Id = 3, IngredientsId = 3, RecipeId = 1, Amount = 1.3},
               new Relation{Id = 4, IngredientsId = 4, RecipeId = 1, Amount = 2.2},
               new Relation{Id = 5, IngredientsId = 5, RecipeId = 1, Amount = 4.7},
               new Relation{Id = 6, IngredientsId = 13, RecipeId = 1, Amount = 400},


               new Relation{Id = 7, IngredientsId = 1, RecipeId = 2, Amount = 300},
               new Relation{Id = 8, IngredientsId = 4, RecipeId = 2, Amount = 42},
               new Relation{Id = 9, IngredientsId = 6, RecipeId = 2, Amount = 530},
               new Relation{Id = 10, IngredientsId = 7, RecipeId = 2, Amount = 2.7},
               new Relation{Id = 11, IngredientsId = 8, RecipeId = 2, Amount = 5.1},
               new Relation{Id = 12, IngredientsId = 13, RecipeId = 2, Amount = 120},


               new Relation{Id = 13, IngredientsId = 1, RecipeId = 3, Amount = 230},
               new Relation{Id = 14, IngredientsId = 4, RecipeId = 3, Amount = 4.2},
               new Relation{Id = 15, IngredientsId = 9, RecipeId = 3, Amount = 570},
               new Relation{Id = 16, IngredientsId = 7, RecipeId = 3, Amount = 40},
               new Relation{Id = 17, IngredientsId = 10,RecipeId = 3, Amount = 6.1},
               new Relation{Id = 18, IngredientsId = 13, RecipeId = 3, Amount = 150},


               new Relation{Id = 19, IngredientsId = 1, RecipeId = 4, Amount = 340},
               new Relation{Id = 20, IngredientsId = 4, RecipeId = 4, Amount = 3.6},
               new Relation{Id = 21, IngredientsId = 11, RecipeId = 4, Amount = 4.7},
               new Relation{Id = 22, IngredientsId = 7, RecipeId = 4, Amount = 120},
               new Relation{Id = 23, IngredientsId = 12, RecipeId = 4, Amount = 5.2},
               new Relation{Id = 24, IngredientsId = 13, RecipeId = 4, Amount = 250},
               new Relation{Id = 25, IngredientsId = 14, RecipeId = 4, Amount = 276.5},
           };
            List<Opakowania> opakowania = new List<Opakowania>()
            {
                new Opakowania{ Id = 1, MaterialNumber = 4439904, Name = "Butelka czarna 1 kg", Capacity = 1},
                new Opakowania{ Id = 2, MaterialNumber = 4477398, Name = "Wiadro białe 10 kg", Capacity = 10},
                new Opakowania{ Id = 3, MaterialNumber = 4033456, Name = "Wiadro czerwone 3.2 kg", Capacity = 3.2},
                new Opakowania{ Id = 4, MaterialNumber = 4499540, Name = "Nakrentka RD50"},
                new Opakowania{ Id = 5, MaterialNumber = 4432324, Name = "Wieczko niebeiske średnica 18 cm (3.2 kg)"},
                new Opakowania{ Id = 6, MaterialNumber = 4466950, Name = "Wieczko białe średnica 32 cm (10 kg)"},
                new Opakowania{ Id = 7, MaterialNumber = 4436904, Name = "Naklejka 100x100 biała"},
                new Opakowania{ Id = 8, MaterialNumber = 4410932, Name = "Naklejka Truskawka w żelu 3.2 kg"},
                new Opakowania{ Id = 9, MaterialNumber = 4490437, Name = "Naklejka Wiśnia w żelu 3.2 kg"},
                new Opakowania{ Id = 10, MaterialNumber = 4400475, Name = "Naklejka Sos Krówka 1 kg"}

            };

            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ROLE_ADM_ID = ADMIN_ID;

            const string NUsers_ID = "a12be9c5-aa65-4af6-bd97-00bd9344e575";
            const string ROLE_NUser_ID = NUsers_ID;

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ADM_ID,
                Name = "Admin"
            },
            new IdentityRole
            {
                Id = ROLE_NUser_ID,
                Name = "NormalUser"
            });

            var hasher = new PasswordHasher<IdentityUser>();

            List<IdentityUser> users = new List<IdentityUser>() {
                new IdentityUser
                {
                Id = ADMIN_ID,
                NormalizedUserName = "Sara",
                UserName = "Sara",
                EmailConfirmed = true,
                Email = "sara@gmail.com",
                NormalizedEmail = "sara@gmail.com",
                PasswordHash = hasher.HashPassword(null, "demo"),
                SecurityStamp = string.Empty
                },
                new IdentityUser
                {
                Id = NUsers_ID,
                NormalizedUserName = "Petro",
                UserName = "Petro",
                EmailConfirmed = true,
                Email = "petro@gmail.com",
                NormalizedEmail = "petro@gmail.com",
                PasswordHash = hasher.HashPassword(null, "demo"),
                SecurityStamp = string.Empty
                }
            };
            modelBuilder.Entity<IdentityUser>().HasData(users);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ADM_ID,
                UserId = ADMIN_ID
            }
            ,
            new IdentityUserRole<string>
            {
                RoleId = ROLE_NUser_ID,
                UserId = NUsers_ID
            }
            );

            modelBuilder.Entity<Ingredient>().HasData(Ingredients);
            modelBuilder.Entity<Recipe>().HasData(Recipies);
            modelBuilder.Entity<Relation>().HasData(relations);

        }
        public static double Count(double totalCount, double item)
        {
            return Math.Round((totalCount / 1000) * item, 2);
        }
    }
}