﻿// <auto-generated />
using System;
using MaterialLossApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MaterialLossApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MaterialLossApp.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Capacity")
                        .HasColumnType("float");

                    b.Property<int>("MaterialNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Use")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 0.0,
                            MaterialNumber = 4405021,
                            Name = "Cukier",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 0.0,
                            MaterialNumber = 4431245,
                            Name = "Mleko zagęszczone",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 0.0,
                            MaterialNumber = 4460655,
                            Name = "Odpieniacz",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 4,
                            Capacity = 0.0,
                            MaterialNumber = 4433212,
                            Name = "Konserwant",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 5,
                            Capacity = 0.0,
                            MaterialNumber = 4477132,
                            Name = "Aromat Krówka",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 6,
                            Capacity = 0.0,
                            MaterialNumber = 4498443,
                            Name = "Truskawka",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 7,
                            Capacity = 0.0,
                            MaterialNumber = 4458216,
                            Name = "Skrobia modyfikowana",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 8,
                            Capacity = 0.0,
                            MaterialNumber = 4465543,
                            Name = "Aromat truskawka",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 9,
                            Capacity = 0.0,
                            MaterialNumber = 4494328,
                            Name = "Wiśnia",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 10,
                            Capacity = 0.0,
                            MaterialNumber = 4465503,
                            Name = "Aromat wiśnia",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 11,
                            Capacity = 0.0,
                            MaterialNumber = 4475934,
                            Name = "Guma Xantan",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 12,
                            Capacity = 0.0,
                            MaterialNumber = 4416630,
                            Name = "Aromat waniliowy",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 13,
                            Capacity = 0.0,
                            MaterialNumber = 0,
                            Name = "Woda",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 14,
                            Capacity = 0.0,
                            MaterialNumber = 4409530,
                            Name = "Syrop glukozowy",
                            SectionName = "Składniki",
                            Use = ""
                        },
                        new
                        {
                            Id = 15,
                            Capacity = 1.0,
                            MaterialNumber = 4439904,
                            Name = "Butelka czarna 1 kg",
                            SectionName = "Opakowania",
                            Use = "Container"
                        },
                        new
                        {
                            Id = 16,
                            Capacity = 10.0,
                            MaterialNumber = 4477398,
                            Name = "Wiadro białe 10 kg",
                            SectionName = "Opakowania",
                            Use = "Container"
                        },
                        new
                        {
                            Id = 17,
                            Capacity = 3.2000000000000002,
                            MaterialNumber = 4033456,
                            Name = "Wiadro czerwone 3.2 kg",
                            SectionName = "Opakowania",
                            Use = "Container"
                        },
                        new
                        {
                            Id = 18,
                            Capacity = 0.0,
                            MaterialNumber = 4499540,
                            Name = "Nakrentka RD50",
                            SectionName = "Opakowania",
                            Use = "Cap"
                        },
                        new
                        {
                            Id = 19,
                            Capacity = 0.0,
                            MaterialNumber = 4432324,
                            Name = "Wieczko niebeiske średnica 18 cm (3.2 kg)",
                            SectionName = "Opakowania",
                            Use = "Cap"
                        },
                        new
                        {
                            Id = 20,
                            Capacity = 0.0,
                            MaterialNumber = 4466950,
                            Name = "Wieczko białe średnica 32 cm (10 kg)",
                            SectionName = "Opakowania",
                            Use = "Cap"
                        },
                        new
                        {
                            Id = 21,
                            Capacity = 0.0,
                            MaterialNumber = 4436904,
                            Name = "Naklejka 100x100 biała",
                            SectionName = "Opakowania",
                            Use = "Label"
                        },
                        new
                        {
                            Id = 22,
                            Capacity = 0.0,
                            MaterialNumber = 4410932,
                            Name = "Naklejka Truskawka w żelu 3.2 kg",
                            SectionName = "Opakowania",
                            Use = "Label"
                        },
                        new
                        {
                            Id = 23,
                            Capacity = 0.0,
                            MaterialNumber = 4490437,
                            Name = "Naklejka Wiśnia w żelu 3.2 kg",
                            SectionName = "Opakowania",
                            Use = "Label"
                        },
                        new
                        {
                            Id = 24,
                            Capacity = 0.0,
                            MaterialNumber = 4400475,
                            Name = "Naklejka Sos Krówka 1 kg",
                            SectionName = "Opakowania",
                            Use = "Label"
                        });
                });

            modelBuilder.Entity("MaterialLossApp.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("IlośćNaklejek")
                        .HasColumnType("int");

                    b.Property<int>("IlośćOpakowań")
                        .HasColumnType("int");

                    b.Property<int>("IlośćPokrywNekrętek")
                        .HasColumnType("int");

                    b.Property<string>("Naklejka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NrZlecenia")
                        .HasColumnType("int");

                    b.Property<string>("Opakowanie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PokrywaNekrętka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("RecipesName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("MaterialLossApp.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sos krówka"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Truskawka w żelu"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Wiśnia w żelu"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Nadzienie waniliowe"
                        });
                });

            modelBuilder.Entity("MaterialLossApp.Models.Relation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("IngredientsId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Relations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 162.0,
                            IngredientsId = 1,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 430.0,
                            IngredientsId = 2,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 3,
                            Amount = 1.3,
                            IngredientsId = 3,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 4,
                            Amount = 2.2000000000000002,
                            IngredientsId = 4,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 5,
                            Amount = 4.7000000000000002,
                            IngredientsId = 5,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 6,
                            Amount = 400.0,
                            IngredientsId = 13,
                            RecipeId = 1
                        },
                        new
                        {
                            Id = 7,
                            Amount = 300.0,
                            IngredientsId = 1,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 8,
                            Amount = 42.0,
                            IngredientsId = 4,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 9,
                            Amount = 530.0,
                            IngredientsId = 6,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 10,
                            Amount = 2.7000000000000002,
                            IngredientsId = 7,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 11,
                            Amount = 5.0999999999999996,
                            IngredientsId = 8,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 12,
                            Amount = 120.0,
                            IngredientsId = 13,
                            RecipeId = 2
                        },
                        new
                        {
                            Id = 13,
                            Amount = 230.0,
                            IngredientsId = 1,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 14,
                            Amount = 4.2000000000000002,
                            IngredientsId = 4,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 15,
                            Amount = 570.0,
                            IngredientsId = 9,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 16,
                            Amount = 40.0,
                            IngredientsId = 7,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 17,
                            Amount = 6.0999999999999996,
                            IngredientsId = 10,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 18,
                            Amount = 150.0,
                            IngredientsId = 13,
                            RecipeId = 3
                        },
                        new
                        {
                            Id = 19,
                            Amount = 340.0,
                            IngredientsId = 1,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 20,
                            Amount = 3.6000000000000001,
                            IngredientsId = 4,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 21,
                            Amount = 4.7000000000000002,
                            IngredientsId = 11,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 22,
                            Amount = 120.0,
                            IngredientsId = 7,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 23,
                            Amount = 5.2000000000000002,
                            IngredientsId = 12,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 24,
                            Amount = 250.0,
                            IngredientsId = 13,
                            RecipeId = 4
                        },
                        new
                        {
                            Id = 25,
                            Amount = 276.5,
                            IngredientsId = 14,
                            RecipeId = 4
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = "a12be9c5-aa65-4af6-bd97-00bd9344e575",
                            Name = "NormalUser"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "73627745-f227-4e7f-842e-dde62ca5e87a",
                            Email = "sara@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "sara@gmail.com",
                            NormalizedUserName = "Sara",
                            PasswordHash = "AQAAAAIAAYagAAAAEKkYjJoTXoPSiD1Nb2ZffDNgG+QX2HOwW37tIv++8lTvysvA2EQA84eJd/1PQJpcJg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "Sara"
                        },
                        new
                        {
                            Id = "a12be9c5-aa65-4af6-bd97-00bd9344e575",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1cb0dfcc-389c-4202-90ff-3a35a0267f2a",
                            Email = "petro@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "petro@gmail.com",
                            NormalizedUserName = "Petro",
                            PasswordHash = "AQAAAAIAAYagAAAAEE2K9LVrQbycXUmL5leffSYkDoUWgPF3anm2hmBqtJS6VeLjjBoCHTf1YsFdsN6XDg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "Petro"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                            RoleId = "a18be9c0-aa65-4af8-bd17-00bd9344e575"
                        },
                        new
                        {
                            UserId = "a12be9c5-aa65-4af6-bd97-00bd9344e575",
                            RoleId = "a12be9c5-aa65-4af6-bd97-00bd9344e575"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
