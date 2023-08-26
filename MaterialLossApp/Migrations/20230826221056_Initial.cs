using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaterialLossApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteIngredientId = table.Column<int>(type: "int", nullable: false),
                    RealizedOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrZlecenia = table.Column<int>(type: "int", nullable: false),
                    RecipesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Opakowanie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PokrywaNekrętka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naklejka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IlośćOpakowań = table.Column<int>(type: "int", nullable: false),
                    IlośćPokrywNekrętek = table.Column<int>(type: "int", nullable: false),
                    IlośćNaklejek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    IngredientCount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RealizedOrders",
                columns: table => new
                {
                    RealizedOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealizedOrderNumber = table.Column<int>(type: "int", nullable: false),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealizedOrders", x => x.RealizedOrderId);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WasteIngredients",
                columns: table => new
                {
                    WasteIngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealizedOrderId = table.Column<int>(type: "int", nullable: false),
                    IngredientNumber = table.Column<int>(type: "int", nullable: false),
                    IngredientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<float>(type: "real", nullable: false),
                    Waste = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    RealizedOrdersRealizedOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteIngredients", x => x.WasteIngredientId);
                    table.ForeignKey(
                        name: "FK_WasteIngredients_RealizedOrders_RealizedOrdersRealizedOrderId",
                        column: x => x.RealizedOrdersRealizedOrderId,
                        principalTable: "RealizedOrders",
                        principalColumn: "RealizedOrderId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a12be9c5-aa65-4af6-bd97-00bd9344e575", null, "NormalUser", null },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e575", null, "Admin", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a12be9c5-aa65-4af6-bd97-00bd9344e575", 0, "6c15a056-3264-4a5a-aa51-1e5c1de171c7", "petro@gmail.com", true, false, null, "petro@gmail.com", "Petro", "AQAAAAIAAYagAAAAEOOJzm+8uRx9zMB+MsR5Y+ocdKGInrmUt61s9iGcMf5tu2kcbX4BQDyR28ggFnK8pA==", null, false, "", false, "Petro" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e575", 0, "7fd44156-aefb-49b3-8b4b-056b1d42907c", "sara@gmail.com", true, false, null, "sara@gmail.com", "Sara", "AQAAAAIAAYagAAAAEEnl44BHyjcZO2p9SeUdHKBOOWCIS4taR1TKH24avEplB1UoBHXD1Cbi5ekVS498kg==", null, false, "", false, "Sara" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Capacity", "MaterialNumber", "Name", "SectionName", "Use" },
                values: new object[,]
                {
                    { 1, 0.0, 4405021, "Cukier", "Składniki", "" },
                    { 2, 0.0, 4431245, "Mleko zagęszczone", "Składniki", "" },
                    { 3, 0.0, 4460655, "Odpieniacz", "Składniki", "" },
                    { 4, 0.0, 4433212, "Konserwant", "Składniki", "" },
                    { 5, 0.0, 4477132, "Aromat Krówka", "Składniki", "" },
                    { 6, 0.0, 4498443, "Truskawka", "Składniki", "" },
                    { 7, 0.0, 4458216, "Skrobia modyfikowana", "Składniki", "" },
                    { 8, 0.0, 4465543, "Aromat truskawka", "Składniki", "" },
                    { 9, 0.0, 4494328, "Wiśnia", "Składniki", "" },
                    { 10, 0.0, 4465503, "Aromat wiśnia", "Składniki", "" },
                    { 11, 0.0, 4475934, "Guma Xantan", "Składniki", "" },
                    { 12, 0.0, 4416630, "Aromat waniliowy", "Składniki", "" },
                    { 13, 0.0, 0, "Woda", "Składniki", "" },
                    { 14, 0.0, 4409530, "Syrop glukozowy", "Składniki", "" },
                    { 15, 1.0, 4439904, "Butelka czarna 1 kg", "Opakowania", "Container" },
                    { 16, 10.0, 4477398, "Wiadro białe 10 kg", "Opakowania", "Container" },
                    { 17, 3.2000000000000002, 4033456, "Wiadro czerwone 3.2 kg", "Opakowania", "Container" },
                    { 18, 0.0, 4499540, "Nakrentka RD50", "Opakowania", "Cap" },
                    { 19, 0.0, 4432324, "Wieczko niebeiske średnica 18 cm (3.2 kg)", "Opakowania", "Cap" },
                    { 20, 0.0, 4466950, "Wieczko białe średnica 32 cm (10 kg)", "Opakowania", "Cap" },
                    { 21, 0.0, 4436904, "Naklejka 100x100 biała", "Opakowania", "Label" },
                    { 22, 0.0, 4410932, "Naklejka Truskawka w żelu 3.2 kg", "Opakowania", "Label" },
                    { 23, 0.0, 4490437, "Naklejka Wiśnia w żelu 3.2 kg", "Opakowania", "Label" },
                    { 24, 0.0, 4400475, "Naklejka Sos Krówka 1 kg", "Opakowania", "Label" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sos krówka" },
                    { 2, "Truskawka w żelu" },
                    { 3, "Wiśnia w żelu" },
                    { 4, "Nadzienie waniliowe" }
                });

            migrationBuilder.InsertData(
                table: "Relations",
                columns: new[] { "Id", "Amount", "IngredientsId", "RecipeId" },
                values: new object[,]
                {
                    { 1, 162.0, 1, 1 },
                    { 2, 430.0, 2, 1 },
                    { 3, 1.3, 3, 1 },
                    { 4, 2.2000000000000002, 4, 1 },
                    { 5, 4.7000000000000002, 5, 1 },
                    { 6, 400.0, 13, 1 },
                    { 7, 300.0, 1, 2 },
                    { 8, 42.0, 4, 2 },
                    { 9, 530.0, 6, 2 },
                    { 10, 2.7000000000000002, 7, 2 },
                    { 11, 5.0999999999999996, 8, 2 },
                    { 12, 120.0, 13, 2 },
                    { 13, 230.0, 1, 3 },
                    { 14, 4.2000000000000002, 4, 3 },
                    { 15, 570.0, 9, 3 },
                    { 16, 40.0, 7, 3 },
                    { 17, 6.0999999999999996, 10, 3 },
                    { 18, 150.0, 13, 3 },
                    { 19, 340.0, 1, 4 },
                    { 20, 3.6000000000000001, 4, 4 },
                    { 21, 4.7000000000000002, 11, 4 },
                    { 22, 120.0, 7, 4 },
                    { 23, 5.2000000000000002, 12, 4 },
                    { 24, 250.0, 13, 4 },
                    { 25, 276.5, 14, 4 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "a12be9c5-aa65-4af6-bd97-00bd9344e575", "a12be9c5-aa65-4af6-bd97-00bd9344e575" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "a18be9c0-aa65-4af8-bd17-00bd9344e575" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WasteIngredients_RealizedOrdersRealizedOrderId",
                table: "WasteIngredients",
                column: "RealizedOrdersRealizedOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ItemsCount");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Relations");

            migrationBuilder.DropTable(
                name: "WasteIngredients");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RealizedOrders");
        }
    }
}
