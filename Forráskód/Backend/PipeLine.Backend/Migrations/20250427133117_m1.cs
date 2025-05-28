using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PipeLine.Backend.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FirstName = table.Column<string>(type: "longtext", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChargingStations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingStations", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DeviceType = table.Column<int>(type: "int", nullable: false),
                    Manufacturer = table.Column<string>(type: "longtext", nullable: true),
                    Model = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    BatteryCapacity = table.Column<double>(type: "double", nullable: true),
                    MaxChargingSpeed = table.Column<double>(type: "double", nullable: true),
                    BatteryVoltage = table.Column<double>(type: "double", nullable: true),
                    Discriminator = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    DetachableBattery = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    IsFoldable = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CanBeLocked = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChargingPorts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsBeingUsed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDisabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ChargingStationId = table.Column<Guid>(type: "char(36)", nullable: false),
                    PortNumber = table.Column<int>(type: "int", nullable: false),
                    MaxChargingSpeed = table.Column<double>(type: "double", nullable: false),
                    IsCharging = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingPorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargingPorts_ChargingStations_ChargingStationId",
                        column: x => x.ChargingStationId,
                        principalTable: "ChargingStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ErrorTickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ChargingStationId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    IsSolved = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorTickets_ChargingStations_ChargingStationId",
                        column: x => x.ChargingStationId,
                        principalTable: "ChargingStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChargingInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ChargingPortId = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    StartingPercentage = table.Column<int>(type: "int", nullable: true),
                    EndPercentage = table.Column<int>(type: "int", nullable: true),
                    DesiredEndPercentage = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargingInstances_ChargingPorts_ChargingPortId",
                        column: x => x.ChargingPortId,
                        principalTable: "ChargingPorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ChargingInstances_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, "Admin", "ADMIN" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("78999ea0-6338-4a00-b990-8c2f32e1393c"), 0, "7ca47adf-c0ad-4eb8-9dc9-29b14f75af80", "tamastest@gmail.com", false, "Tamás", "Teszt", false, null, "TAMASTEST@GMAIL.COM", "TAMASTESTER", "AQAAAAIAAYagAAAAECP4TkmP22zofsbX4RkL37IOP3/mWad2k+iKtmIFOBl+qXo0g/TUmKGHY+47JAbf1g==", "1234567890", false, "50d77837-1e20-4bdc-8864-fdcf18cf7a60", false, "TamasTester" },
                    { new Guid("7fc4fdae-4413-4114-b085-40e1eaa80142"), 0, "c24dddba-85a6-4a33-9c79-a30254a1c605", "admin@gmail.com", false, "Admin", "Admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEFPpwA+MJoUPWxOzrOxvkrztwBzkhNG9Vvg4j/id6W7LS+B4yxKeAvnVRboFlJcrEA==", "7777777777", false, "4ed5e6c2-e282-44e1-84e8-fbfddd72c6dd", false, "admin1" },
                    { new Guid("8dfc4275-dee6-4da0-adf8-d9b1afaf9299"), 0, "6a7fe70c-cd0d-48bf-84eb-806d17c629ff", "zoliz@gmail.com", false, "Zoltán", "Zárás", false, null, "ZOLIZ@GMAIL.COM", "ZOLIZ", "AQAAAAIAAYagAAAAEAceATwexw6a/dZ1VsxRGgfxnlAoiKZoG3pXM1UkpyAsq09BIa2fQvKXBRb6qqOKXg==", "777888999", false, "bbc99d98-1559-4bf8-a4d8-92a8d7931267", false, "ZoliZ" },
                    { new Guid("93233682-756a-4ca3-8759-e4ec37182f29"), 0, "df1ae909-2089-40c9-b3fb-181e16819561", "patiproba@gmail.com", false, "Patrícia", "Próba", false, null, "PATIPROBA@GMAIL.COM", "PATIPROBA", "AQAAAAIAAYagAAAAEGBwpxQ3hJPWxYd9VdlQfS4Sg1OXRsEmo/C7adQJCubHijSu0iHrAwvinWe0bIf5uQ==", "0987654321", false, "8fe6be20-d8d4-4797-8a2e-0507f8611d6d", false, "PatiProba" },
                    { new Guid("cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847"), 0, "3e7d899e-317d-4533-8303-b6f0a083bd57", "bbence@gmail.com", false, "Bence", "Beállítás", false, null, "BBENCE@GMAIL.COM", null, "AQAAAAIAAYagAAAAEB+WUk/xUbn+gov4uD7Ej8Q3pz3rM6cdI8eSKFOFJmyq5Jo1TLTFByB+qIXyvemqLw==", "111222333", false, "1b868f53-84cc-4460-b645-e4597e4aae94", false, "BenceB" },
                    { new Guid("dd6f6963-4229-4921-a26f-06f5f221ff1b"), 0, "8fc32e0d-5b1f-450a-858f-33d6b74f2296", "rekareg@gmail.com", false, "Réka", "Regisztráció", false, null, "REKAREG@GMAIL.COM", "REKAR", "AQAAAAIAAYagAAAAEIDm5OMzv9xnDJmKz4kJjrgjKhGMc18VV035gwoWSzLPBLD5hYqftx43dKJt7z4kxA==", "444555666", false, "e78722f4-6b8a-4c07-a758-36aa55dd29f8", false, "RekaR" }
                });

            migrationBuilder.InsertData(
                table: "ChargingStations",
                columns: new[] { "Id", "Address", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { new Guid("0d46cb77-0134-4850-99ec-8ac8d7105c26"), "23 Kecskemét Way", 46.906199999999998, 19.691299999999998, "Kecskemét Micromobility Hub" },
                    { new Guid("1783ac8e-96fb-47c0-8c74-7bcac28cf383"), "12 Cluj Avenue", 46.7712, 23.6236, "Cluj-Napoca Micromobility Hub" },
                    { new Guid("21aecd38-fae4-4f9b-b634-bb50ccaa32d8"), "77 Tatabánya Route", 47.584000000000003, 18.4039, "Tatabánya Micromobility Hub" },
                    { new Guid("2a466d50-82c3-4e64-9d79-bdf59fe2f25b"), "35 Zalaegerszeg Boulevard", 46.844999999999999, 16.8416, "Zalaegerszeg Micromobility Hub" },
                    { new Guid("2f3820bc-ba7d-487b-897e-12d2c5041ce5"), "45 Keleti út, Budapest", 47.503909999999998, 19.116569999999999, "Budapest Micromobility Hub - East" },
                    { new Guid("340e8c31-6f52-4e7f-9f43-c2c1b9b9add4"), "23 Veszprém Avenue", 47.093299999999999, 17.9115, "Veszprém Micromobility Hub" },
                    { new Guid("4c4f3e6e-19d0-42e1-885f-35ec2e39fd1d"), "45 Eger Main Road", 47.902500000000003, 20.377199999999998, "Eger Micromobility Hub" },
                    { new Guid("4db1ba58-c788-47cb-819c-08da571e5978"), "55 Pécs Drive", 46.072699999999998, 18.232299999999999, "Pécs Micromobility Hub" },
                    { new Guid("4e5f4cf4-8ed7-491f-b18e-7dbeed52b219"), "33 Kaposvár Ring", 46.359400000000001, 17.782699999999998, "Kaposvár Micromobility Hub" },
                    { new Guid("4fd34718-3872-4bf1-8fb5-5b30849fc80e"), "33 Győr Center", 47.6875, 17.650400000000001, "Győr Micromobility Hub" },
                    { new Guid("5a9d3f31-c85d-47b1-9d8e-8e80e4a0943e"), "90 Zagreb Boulevard", 45.814999999999998, 15.9819, "Zagreb Micromobility Hub" },
                    { new Guid("625307d2-d501-41e5-8fdf-77d151bdd260"), "90 Debrecen Ring", 47.531599999999997, 21.627300000000002, "Debrecen Micromobility Hub" },
                    { new Guid("734aa62b-dcaa-4815-a659-24d1fcaa3b18"), "101 Budapest Blvd", 47.498010000000001, 19.039909999999999, "Budapest Micromobility Hub - West" },
                    { new Guid("74178acf-f8d4-4c2b-b55c-6f136b13bc39"), "72 Nyíregyháza Street", 47.955399999999997, 21.716699999999999, "Nyíregyháza Micromobility Hub" },
                    { new Guid("7c414209-c0f1-4ef2-bc4d-863deec95bf2"), "22 Torontál tér, Szeged", 46.249600000000001, 20.172799999999999, "Szeged Micromobility Hub - Újszeged" },
                    { new Guid("91b1377a-33b8-4bf7-943c-8299d7f78343"), "102 Berlin Platz", 52.520000000000003, 13.404999999999999, "Berlin Micromobility Hub" },
                    { new Guid("9cb9a098-3cfb-46dc-bd72-4f2a938323f7"), "14 Békéscsaba Ring", 46.683399999999999, 21.088699999999999, "Békéscsaba Micromobility Hub" },
                    { new Guid("a57aff6e-a64c-4938-9a2c-974fc2505780"), "88 Vienna Central", 48.208199999999998, 16.373799999999999, "Vienna Micromobility Hub" },
                    { new Guid("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"), "12 Tisza Street, Szeged", 46.253, 20.148, "Szeged Micromobility Hub" },
                    { new Guid("b5f85c22-9d87-4e5b-bf56-45a1dc0bfa9a"), "34 Kárász Street, Szeged", 46.253, 20.141400000000001, "Szeged Micromobility Hub - Belváros" },
                    { new Guid("bb0fdbc1-cc97-4601-9c81-ace35cf1aaba"), "66 Sopron Street", 47.681699999999999, 16.584499999999998, "Sopron Micromobility Hub" },
                    { new Guid("c8c8281f-79b4-4a8a-b930-2c7af38d4e7b"), "21 Bratislava Square", 48.148600000000002, 17.107700000000001, "Bratislava Micromobility Hub" },
                    { new Guid("dba2e8e4-6d61-49af-b74a-4b9d927d0b2e"), "77 Prague Center", 50.075499999999998, 14.437799999999999, "Prague Micromobility Hub" },
                    { new Guid("e1621f6d-a8d5-4065-a537-0d2aa0adee96"), "87 Miskolc Avenue", 48.103000000000002, 20.777999999999999, "Miskolc Micromobility Hub" },
                    { new Guid("e5bfb6da-4d5e-4c7c-9bcb-8b8dbf9a1a64"), "58 Szombathely Road", 47.230699999999999, 16.6218, "Szombathely Micromobility Hub" },
                    { new Guid("e8949cc0-566a-423c-beca-ef749acd972d"), "11 Fehérvár Way", 47.186, 18.4221, "Székesfehérvár Micromobility Hub" },
                    { new Guid("eb8c3c7e-7044-4f32-9483-18a2a5f10ed9"), "54 Ljubljana Road", 46.056899999999999, 14.505800000000001, "Ljubljana Micromobility Hub" },
                    { new Guid("f1e8c45b-2a91-4ff8-b3ec-24b2781c9d24"), "63 Brno Main Street", 49.195099999999996, 16.6068, "Brno Micromobility Hub" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("78999ea0-6338-4a00-b990-8c2f32e1393c") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("7fc4fdae-4413-4114-b085-40e1eaa80142") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("8dfc4275-dee6-4da0-adf8-d9b1afaf9299") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("93233682-756a-4ca3-8759-e4ec37182f29") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("dd6f6963-4229-4921-a26f-06f5f221ff1b") }
                });

            migrationBuilder.InsertData(
                table: "ChargingPorts",
                columns: new[] { "Id", "ChargingStationId", "IsBeingUsed", "IsCharging", "IsDisabled", "MaxChargingSpeed", "PortNumber" },
                values: new object[,]
                {
                    { new Guid("01d024a5-fe51-4419-a872-1a19f98db4c7"), new Guid("4fd34718-3872-4bf1-8fb5-5b30849fc80e"), true, true, true, 1.2, 3 },
                    { new Guid("08bcd0e3-5f9c-48f7-b345-a22b4c1961cb"), new Guid("625307d2-d501-41e5-8fdf-77d151bdd260"), false, false, false, 1.2, 3 },
                    { new Guid("11b58460-2e0d-4d58-a6f5-39a3945a6289"), new Guid("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"), true, false, true, 1.2, 4 },
                    { new Guid("1c1351ab-a67b-4f64-9859-8a6403e6ef95"), new Guid("a57aff6e-a64c-4938-9a2c-974fc2505780"), true, false, false, 1.2, 3 },
                    { new Guid("1dcebbab-37a2-4473-9aff-dbc89f5f843f"), new Guid("625307d2-d501-41e5-8fdf-77d151bdd260"), true, true, false, 1.6000000000000001, 2 },
                    { new Guid("206213c1-7437-4340-aedc-91d5ba483d77"), new Guid("e1621f6d-a8d5-4065-a537-0d2aa0adee96"), false, true, false, 1.6000000000000001, 1 },
                    { new Guid("21176d38-9fbb-41e0-8a51-638250daaf40"), new Guid("4db1ba58-c788-47cb-819c-08da571e5978"), false, false, true, 1.2, 2 },
                    { new Guid("2d1b1640-aae9-49db-aa61-c9331e6b1b7d"), new Guid("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"), true, false, true, 1.5, 2 },
                    { new Guid("445cba27-5324-4b2d-a00d-3d12f0a32645"), new Guid("74178acf-f8d4-4c2b-b55c-6f136b13bc39"), false, true, false, 1.6000000000000001, 2 },
                    { new Guid("494aebb3-23d3-4d91-99f8-7f86c60f5ea3"), new Guid("4fd34718-3872-4bf1-8fb5-5b30849fc80e"), false, false, false, 1.5, 4 },
                    { new Guid("530e70de-8bdc-4037-8276-69ab27f71484"), new Guid("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"), false, false, false, 1.5, 47 },
                    { new Guid("68de7fec-ae57-43c3-920f-c976d9c1b464"), new Guid("a57aff6e-a64c-4938-9a2c-974fc2505780"), true, false, true, 1.6000000000000001, 2 },
                    { new Guid("6e934b8f-e509-4376-be7e-71f16309e7fe"), new Guid("e1621f6d-a8d5-4065-a537-0d2aa0adee96"), false, true, false, 1.6000000000000001, 4 },
                    { new Guid("82a4a1a6-210e-4c0e-b96f-b9d0109acd59"), new Guid("a57aff6e-a64c-4938-9a2c-974fc2505780"), false, true, false, 1.2, 1 },
                    { new Guid("838d79f1-89a9-4cc7-a992-febbb7804b14"), new Guid("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"), true, false, false, 1.6000000000000001, 3 },
                    { new Guid("8a5a4cba-d12d-4353-92c1-d60eefdaa712"), new Guid("4db1ba58-c788-47cb-819c-08da571e5978"), false, false, false, 1.5, 1 },
                    { new Guid("8f7f6784-4e76-4fba-9f44-e014fc3f5940"), new Guid("2f3820bc-ba7d-487b-897e-12d2c5041ce5"), true, false, false, 1.5, 1 },
                    { new Guid("a05290a1-01fe-489a-b233-3063ab451048"), new Guid("74178acf-f8d4-4c2b-b55c-6f136b13bc39"), true, false, false, 1.6000000000000001, 1 },
                    { new Guid("a91026c8-1dc2-4311-adf5-49402703d000"), new Guid("734aa62b-dcaa-4815-a659-24d1fcaa3b18"), false, false, true, 1.6000000000000001, 1 },
                    { new Guid("ab0d6c6e-f47f-4ae1-b77c-beb93b136842"), new Guid("e1621f6d-a8d5-4065-a537-0d2aa0adee96"), true, false, true, 1.5, 2 },
                    { new Guid("ad6fab12-1c99-426e-a201-bc8e14e75b74"), new Guid("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"), false, false, false, 1.5, 45 },
                    { new Guid("b20b2723-5dc1-411c-8a30-6a0655a8481a"), new Guid("734aa62b-dcaa-4815-a659-24d1fcaa3b18"), true, true, false, 1.5, 2 },
                    { new Guid("b42e7da5-cbb1-407f-9e1e-0bfa290db559"), new Guid("0d46cb77-0134-4850-99ec-8ac8d7105c26"), false, true, true, 1.5, 1 },
                    { new Guid("c2efcc7e-b977-4c8f-ad61-a2a93432a11c"), new Guid("625307d2-d501-41e5-8fdf-77d151bdd260"), true, true, true, 1.6000000000000001, 1 },
                    { new Guid("c503e753-9388-4cd2-bb34-78ba2cc352e1"), new Guid("0d46cb77-0134-4850-99ec-8ac8d7105c26"), true, true, false, 1.6000000000000001, 3 },
                    { new Guid("cbfa84da-a918-4f6e-ae7c-2319fbdc911a"), new Guid("74178acf-f8d4-4c2b-b55c-6f136b13bc39"), false, false, false, 1.2, 3 },
                    { new Guid("cebbe124-3172-40f5-bc7a-b420a9e65459"), new Guid("4fd34718-3872-4bf1-8fb5-5b30849fc80e"), true, false, false, 1.6000000000000001, 2 },
                    { new Guid("d128636e-95c4-4728-acc8-0468d02f54f7"), new Guid("a57aff6e-a64c-4938-9a2c-974fc2505780"), false, false, false, 1.6000000000000001, 4 },
                    { new Guid("d1abc777-fa3d-425d-ada7-503c5de6b20c"), new Guid("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"), false, false, false, 1.5, 46 },
                    { new Guid("d2dea098-a325-4a2f-923e-a581ddd4791b"), new Guid("2f3820bc-ba7d-487b-897e-12d2c5041ce5"), false, true, false, 1.6000000000000001, 2 },
                    { new Guid("d972ac98-7f68-4690-a947-ba2f59d7a14c"), new Guid("e1621f6d-a8d5-4065-a537-0d2aa0adee96"), false, true, false, 1.5, 3 },
                    { new Guid("dd8dd71b-2961-449a-860d-dfef294d2a8d"), new Guid("0d46cb77-0134-4850-99ec-8ac8d7105c26"), true, true, true, 1.5, 2 },
                    { new Guid("e726c61a-30d2-4b58-a8a1-ab23750d6250"), new Guid("4fd34718-3872-4bf1-8fb5-5b30849fc80e"), true, false, true, 1.5, 1 },
                    { new Guid("eb59229f-a0d4-49a3-a1cc-f3504451ace1"), new Guid("91b1377a-33b8-4bf7-943c-8299d7f78343"), false, true, false, 1.2, 1 },
                    { new Guid("ef0f137a-37bb-45a6-b830-460467f74ceb"), new Guid("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"), true, true, true, 1.2, 1 },
                    { new Guid("f11dac41-e9c0-4395-9e29-9aa65d4cee85"), new Guid("91b1377a-33b8-4bf7-943c-8299d7f78343"), false, true, false, 1.6000000000000001, 2 },
                    { new Guid("fd342c7a-e418-4480-8dfb-e3c8611cdcdf"), new Guid("91b1377a-33b8-4bf7-943c-8299d7f78343"), true, true, true, 1.5, 3 }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "DetachableBattery", "DeviceType", "Discriminator", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[] { new Guid("01239c68-890a-4186-b115-1bde2639063c"), new Guid("78999ea0-6338-4a00-b990-8c2f32e1393c"), 13000.0, 35.0, true, 0, "EBike", "ENGWE", 2.0, "EP-2 Pro", "Gyárvárosi Cruiser" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "DeviceType", "Discriminator", "IsFoldable", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[,]
                {
                    { new Guid("0eb8c9cd-475a-438d-ae3c-21884ea90bcc"), new Guid("78999ea0-6338-4a00-b990-8c2f32e1393c"), 15300.0, 36.0, 1, "EScooter", false, "Segway", 0.59999999999999998, "Ninebot MAX G30", "Segway Explorer" },
                    { new Guid("1cc80793-209f-4619-908e-0a580f5525db"), new Guid("cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847"), 36000.0, 60.0, 1, "EScooter", false, "Dualtron", 2.5, "Thunder 2", "Thunder Viharjáró" },
                    { new Guid("235a36c1-fec9-4c33-b0cb-85423990a137"), new Guid("dd6f6963-4229-4921-a26f-06f5f221ff1b"), 12800.0, 48.0, 1, "EScooter", true, "NIU", 0.59999999999999998, "KQi3 Pro", "NIU Futár" }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "DetachableBattery", "DeviceType", "Discriminator", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[] { new Guid("26940568-4647-49c7-80c7-e96ccc884ea1"), new Guid("dd6f6963-4229-4921-a26f-06f5f221ff1b"), 14400.0, 48.0, false, 0, "EBike", "HIMO", 0.5, "C26", "Csendes Keringő" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "CanBeLocked", "DeviceType", "Discriminator", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[] { new Guid("3f6eb602-3135-4ec1-b619-0c67e72c3637"), new Guid("93233682-756a-4ca3-8759-e4ec37182f29"), 14400.0, 36.0, true, 2, "ESkateBoard", "Meepo", 0.40000000000000002, "Meepo V4 Shuffle", "Meepo Cruiser" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "DetachableBattery", "DeviceType", "Discriminator", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[] { new Guid("4e40698d-022f-4a3f-8a91-c36372e5ac99"), new Guid("cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847"), 17000.0, 48.0, false, 0, "EBike", "Fiido", 0.69999999999999996, "M1 Pro", "Túrázó M1" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "CanBeLocked", "DeviceType", "Discriminator", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[] { new Guid("659a4302-241a-45ba-9a7a-6d1aba3c8b71"), new Guid("78999ea0-6338-4a00-b990-8c2f32e1393c"), 19900.0, 44.0, false, 2, "ESkateBoard", "Boosted", 0.5, "Boosted Stealth", "Boosted Beast" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "DetachableBattery", "DeviceType", "Discriminator", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[] { new Guid("7a05a19d-1395-449e-afd6-6c4322fb0dd8"), new Guid("93233682-756a-4ca3-8759-e4ec37182f29"), 15000.0, 48.0, true, 0, "EBike", "ADO", 0.40000000000000002, "DECE 300", "Terepvadász" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "DeviceType", "Discriminator", "IsFoldable", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[,]
                {
                    { new Guid("81fd7101-d207-42b3-97c2-bfc2f67c9bda"), new Guid("cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847"), 18000.0, 48.0, 1, "EScooter", true, "Kaabo", 1.2, "Mantis 10 Pro", "Mantis Sárkány" },
                    { new Guid("83cefd82-f987-4471-9293-5dd401931f0a"), new Guid("78999ea0-6338-4a00-b990-8c2f32e1393c"), 12400.0, 36.0, 1, "EScooter", true, "Xiaomi", 0.5, "Mi Electric Scooter Pro 2", "Városi Villám" },
                    { new Guid("8721f5b0-cf96-4a80-aa1b-7b343be48af2"), new Guid("cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847"), 10400.0, 36.0, 1, "EScooter", true, "E-TWOW", 0.40000000000000002, "GT SE", "GT Kompakt" }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "DetachableBattery", "DeviceType", "Discriminator", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[] { new Guid("97840601-53d2-47a9-9062-ed774142658d"), new Guid("78999ea0-6338-4a00-b990-8c2f32e1393c"), 16000.0, 48.0, true, 0, "EBike", "ENGWE", 2.5, "Engine Pro", "Napi Ingázó" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "CanBeLocked", "DeviceType", "Discriminator", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[,]
                {
                    { new Guid("bb2868d7-1f6a-4651-aaca-8178c172590b"), new Guid("dd6f6963-4229-4921-a26f-06f5f221ff1b"), 18000.0, 44.0, true, 2, "ESkateBoard", "Evolve", 0.59999999999999998, "Bamboo GTR", "Evolve Off-Road" },
                    { new Guid("bd3cb4d1-4d28-44d5-bd28-dc167f8e121f"), new Guid("8dfc4275-dee6-4da0-adf8-d9b1afaf9299"), 15000.0, 36.0, false, 2, "ESkateBoard", "WowGo", 0.5, "WowGo 3X", "WowGo Speedster" }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "DeviceType", "Discriminator", "IsFoldable", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[] { new Guid("c935f07a-263d-4be1-a3e6-99554f670aff"), new Guid("8dfc4275-dee6-4da0-adf8-d9b1afaf9299"), 25200.0, 60.0, 1, "EScooter", false, "Apollo", 1.8, "Phantom V3", "Phantom Erőgép" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "ApplicationUserId", "BatteryCapacity", "BatteryVoltage", "DetachableBattery", "DeviceType", "Discriminator", "Manufacturer", "MaxChargingSpeed", "Model", "Name" },
                values: new object[,]
                {
                    { new Guid("cbed7941-6f04-4086-9a3e-8f46c9f91477"), new Guid("cfd11ce4-29ba-43ef-b3e6-f7dc8a76f847"), 12000.0, 36.0, false, 0, "EBike", "TENWAYS", 0.65000000000000002, "CGO800S", "Stílusos Városi" },
                    { new Guid("d59c2bdf-b80c-491a-ab25-590edb690df4"), new Guid("93233682-756a-4ca3-8759-e4ec37182f29"), 10000.0, 36.0, false, 0, "EBike", "HIMO", 0.59999999999999998, "Z20", "Kompakt Zöld" },
                    { new Guid("eafabb49-4083-4de4-8bd7-d092b27007ee"), new Guid("dd6f6963-4229-4921-a26f-06f5f221ff1b"), 16000.0, 48.0, false, 0, "EBike", "ADO", 0.69999999999999996, "A26", "Hegyi Roham" }
                });

            migrationBuilder.InsertData(
                table: "ErrorTickets",
                columns: new[] { "Id", "ChargingStationId", "Description", "IsSolved" },
                values: new object[,]
                {
                    { new Guid("1683a4ca-a195-4ca6-a4cb-2ee28d3ade3d"), new Guid("734aa62b-dcaa-4815-a659-24d1fcaa3b18"), "Port 2 does not work.", false },
                    { new Guid("40018d28-d733-4589-a319-b320a66d3029"), new Guid("2f3820bc-ba7d-487b-897e-12d2c5041ce5"), "Some people vandalised multiple ports", true },
                    { new Guid("723878fd-f94b-4e42-af02-a6cafb58eb21"), new Guid("734aa62b-dcaa-4815-a659-24d1fcaa3b18"), "No power at the charging station", true },
                    { new Guid("b6105591-b25b-4c10-a402-772ec48b9b82"), new Guid("2f3820bc-ba7d-487b-897e-12d2c5041ce5"), "Multiple ports are damaged", false },
                    { new Guid("cd1cbbad-bcfb-443b-b4b5-8751a7942d6c"), new Guid("734aa62b-dcaa-4815-a659-24d1fcaa3b18"), "Port 3 is physically damaged and cannot be used.", false }
                });

            migrationBuilder.InsertData(
                table: "ChargingInstances",
                columns: new[] { "Id", "ChargingPortId", "DesiredEndPercentage", "DeviceId", "End", "EndPercentage", "Start", "StartingPercentage" },
                values: new object[,]
                {
                    { new Guid("2388c138-a8ea-48da-8140-5c8d87da6adf"), new Guid("d2dea098-a325-4a2f-923e-a581ddd4791b"), 85, new Guid("26940568-4647-49c7-80c7-e96ccc884ea1"), new DateTime(2024, 2, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), 82, new DateTime(2024, 2, 15, 8, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { new Guid("59d4f463-e491-4894-bfd8-91162d032cfe"), new Guid("a91026c8-1dc2-4311-adf5-49402703d000"), 100, new Guid("4e40698d-022f-4a3f-8a91-c36372e5ac99"), new DateTime(2024, 2, 10, 16, 45, 0, 0, DateTimeKind.Unspecified), 100, new DateTime(2024, 2, 10, 14, 30, 0, 0, DateTimeKind.Unspecified), 40 },
                    { new Guid("afd7de1c-cb15-4fab-8576-68effd4b7504"), new Guid("a91026c8-1dc2-4311-adf5-49402703d000"), 100, new Guid("97840601-53d2-47a9-9062-ed774142658d"), new DateTime(2024, 2, 12, 19, 15, 0, 0, DateTimeKind.Unspecified), 100, new DateTime(2024, 2, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), 60 },
                    { new Guid("de29d60e-ce40-4bcd-b4f5-e6f2378cebd0"), new Guid("b20b2723-5dc1-411c-8a30-6a0655a8481a"), 100, new Guid("81fd7101-d207-42b3-97c2-bfc2f67c9bda"), new DateTime(2024, 2, 11, 11, 30, 0, 0, DateTimeKind.Unspecified), 100, new DateTime(2024, 2, 11, 9, 15, 0, 0, DateTimeKind.Unspecified), 50 },
                    { new Guid("dfc2430e-0125-4981-a109-2fca390ff039"), new Guid("8f7f6784-4e76-4fba-9f44-e014fc3f5940"), 99, new Guid("bd3cb4d1-4d28-44d5-bd28-dc167f8e121f"), new DateTime(2024, 2, 14, 1, 0, 0, 0, DateTimeKind.Unspecified), 99, new DateTime(2024, 2, 13, 22, 45, 0, 0, DateTimeKind.Unspecified), 70 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChargingInstances_ChargingPortId",
                table: "ChargingInstances",
                column: "ChargingPortId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingInstances_DeviceId",
                table: "ChargingInstances",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingPorts_ChargingStationId",
                table: "ChargingPorts",
                column: "ChargingStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ApplicationUserId",
                table: "Devices",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorTickets_ChargingStationId",
                table: "ErrorTickets",
                column: "ChargingStationId");
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
                name: "ChargingInstances");

            migrationBuilder.DropTable(
                name: "ErrorTickets");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ChargingPorts");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "ChargingStations");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
