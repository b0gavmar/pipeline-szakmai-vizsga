using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.Enums;
using PipeLine.Shared.Models.ErrorTicketModels;
using PipeLine.Shared.Models.UserModels;

namespace PipeLine.Backend.Context
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            List<ApplicationUser> applicationUsers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Tamás",
                    LastName = "Teszt",
                    UserName = "TamasTester",
                    Email = "tamastest@gmail.com",
                    PhoneNumber = "1234567890",
                    PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "TamasT123@"),
                    NormalizedEmail = "tamastest@gmail.com".ToUpper(),
                    NormalizedUserName = "TAMASTESTER",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Patrícia",
                    LastName = "Próba",
                    UserName = "PatiProba",
                    Email = "patiproba@gmail.com",
                    PhoneNumber = "0987654321",
                    PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "456"),
                    NormalizedEmail = "patiproba@gmail.com".ToUpper(),
                    NormalizedUserName = "PATIPROBA",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Bence",
                    LastName = "Beállítás",
                    UserName = "BenceB",
                    Email = "bbence@gmail.com",
                    NormalizedEmail = "bbence@gmail.com".ToUpper(),
                    PhoneNumber = "111222333",
                    PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "789"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Réka",
                    LastName = "Regisztráció",
                    UserName = "RekaR",
                    Email = "rekareg@gmail.com",
                    PhoneNumber = "444555666",
                    NormalizedEmail = "rekareg@gmail.com".ToUpper(),
                    NormalizedUserName = "REKAR",
                    PasswordHash = passwordHasher.HashPassword(new ApplicationUser(),"101112"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Zoltán",
                    LastName = "Zárás",
                    UserName = "ZoliZ",
                    Email = "zoliz@gmail.com",
                    NormalizedEmail = "zoliz@gmail.com".ToUpper(),
                    PhoneNumber = "777888999",
                    NormalizedUserName = "ZOLIZ",
                    PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "131415"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "admin1",
                    Email = "admin@gmail.com",
                    PhoneNumber = "7777777777",

                    PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "Admin789@"),
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
            };

            List<IdentityUserRole<Guid>> userRoles = new List<IdentityUserRole<Guid>>
            {
                new IdentityUserRole<Guid>
                {
                    UserId = applicationUsers[0].Id, 
                    RoleId = roles[1].Id  
                },
                new IdentityUserRole<Guid>
                {
                    UserId = applicationUsers[1].Id, 
                    RoleId = roles[1].Id  
                },
                new IdentityUserRole<Guid>
                {
                    UserId = applicationUsers[2].Id, 
                    RoleId = roles[1].Id  
                },
                new IdentityUserRole<Guid>
                {
                    UserId = applicationUsers[3].Id,
                    RoleId = roles[1].Id
                },new IdentityUserRole<Guid>
                {
                    UserId = applicationUsers[4].Id,
                    RoleId = roles[1].Id
                },new IdentityUserRole<Guid>
                {
                    UserId = applicationUsers[5].Id,
                    RoleId = roles[0].Id
                }
            };

            List<EBike> eBikes = new List<EBike>
            {
                new EBike
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[0].Id,
                    DeviceType = DeviceType.EBike,
                    Manufacturer = "ENGWE",
                    Model = "EP-2 Pro",
                    Name = "Gyárvárosi Cruiser",
                    BatteryCapacity = 13000,
                    MaxChargingSpeed = 2,
                    BatteryVoltage = 35,
                    DetachableBattery = true,
                },
                new EBike
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[0].Id,
                    DeviceType = DeviceType.EBike,
                    Manufacturer = "ENGWE",
                    Model = "Engine Pro",
                    Name = "Napi Ingázó",
                    BatteryCapacity = 16000,
                    MaxChargingSpeed = 2.5,
                    BatteryVoltage = 48,
                    DetachableBattery = true,
                },
                new EBike
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[1].Id,
                    DeviceType = DeviceType.EBike,
                    Manufacturer = "HIMO",
                    Model = "Z20",
                    Name = "Kompakt Zöld",
                    BatteryCapacity = 10000,
                    MaxChargingSpeed = 0.6,
                    BatteryVoltage = 36,
                    DetachableBattery = false,
                },
                new EBike
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[1].Id,
                    DeviceType = DeviceType.EBike,
                    Manufacturer = "ADO",
                    Model = "DECE 300",
                    Name = "Terepvadász",
                    BatteryCapacity = 15000,
                    MaxChargingSpeed = 0.4,
                    BatteryVoltage = 48,
                    DetachableBattery = true,
                },
                new EBike
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[2].Id,
                    DeviceType = DeviceType.EBike,
                    Manufacturer = "Fiido",
                    Model = "M1 Pro",
                    Name = "Túrázó M1",
                    BatteryCapacity = 17000,
                    MaxChargingSpeed = 0.7,
                    BatteryVoltage = 48,
                    DetachableBattery = false,
                },
                new EBike
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[2].Id,
                    DeviceType = DeviceType.EBike,
                    Manufacturer = "TENWAYS",
                    Model = "CGO800S",
                    Name = "Stílusos Városi",
                    BatteryCapacity = 12000,
                    MaxChargingSpeed = 0.65,
                    BatteryVoltage = 36,
                    DetachableBattery = false,
                },
                new EBike
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[3].Id,
                    DeviceType = DeviceType.EBike,
                    Manufacturer = "HIMO",
                    Model = "C26",
                    Name = "Csendes Keringő",
                    BatteryCapacity = 14400,
                    MaxChargingSpeed = 0.5,
                    BatteryVoltage = 48,
                    DetachableBattery = false,
                },
                new EBike
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[3].Id,
                    DeviceType = DeviceType.EBike,
                    Manufacturer = "ADO",
                    Model = "A26",
                    Name = "Hegyi Roham",
                    BatteryCapacity = 16000,
                    MaxChargingSpeed = 0.7,
                    BatteryVoltage = 48,
                    DetachableBattery = false,
                },
            };

            List<EScooter> eScooters = new List<EScooter>
            {
                new EScooter
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[0].Id,
                    DeviceType = DeviceType.EScooter,
                    Manufacturer = "Xiaomi",
                    Model = "Mi Electric Scooter Pro 2",
                    Name = "Városi Villám",
                    BatteryCapacity = 12400,
                    MaxChargingSpeed = 0.5, 
                    BatteryVoltage = 36,
                    IsFoldable = true,
                },
                new EScooter
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[0].Id,
                    DeviceType = DeviceType.EScooter,
                    Manufacturer = "Segway",
                    Model = "Ninebot MAX G30",
                    Name = "Segway Explorer",
                    BatteryCapacity = 15300,
                    MaxChargingSpeed = 0.6,
                    BatteryVoltage = 36,
                    IsFoldable = false,
                },
                new EScooter
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[2].Id,
                    DeviceType = DeviceType.EScooter,
                    Manufacturer = "Kaabo",
                    Model = "Mantis 10 Pro",
                    Name = "Mantis Sárkány",
                    BatteryCapacity = 18000,
                    MaxChargingSpeed = 1.2,
                    BatteryVoltage = 48,
                    IsFoldable = true,
                },
                new EScooter
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[2].Id,
                    DeviceType = DeviceType.EScooter,
                    Manufacturer = "Dualtron",
                    Model = "Thunder 2",
                    Name = "Thunder Viharjáró",
                    BatteryCapacity = 36000,
                    MaxChargingSpeed = 2.5,
                    BatteryVoltage = 60,
                    IsFoldable = false,
                },
                new EScooter
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[2].Id,
                    DeviceType = DeviceType.EScooter,
                    Manufacturer = "E-TWOW",
                    Model = "GT SE",
                    Name = "GT Kompakt",
                    BatteryCapacity = 10400,
                    MaxChargingSpeed = 0.4,
                    BatteryVoltage = 36,
                    IsFoldable = true,
                },
                new EScooter
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[3].Id,
                    DeviceType = DeviceType.EScooter,
                    Manufacturer = "NIU",
                    Model = "KQi3 Pro",
                    Name = "NIU Futár",
                    BatteryCapacity = 12800,
                    MaxChargingSpeed = 0.6,
                    BatteryVoltage = 48,
                    IsFoldable = true,
                },
                new EScooter
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[4].Id,
                    DeviceType = DeviceType.EScooter,
                    Manufacturer = "Apollo",
                    Model = "Phantom V3",
                    Name = "Phantom Erőgép",
                    BatteryCapacity = 25200,
                    MaxChargingSpeed = 1.8,
                    BatteryVoltage = 60,
                    IsFoldable = false,
                },
            };

            List<ESkateBoard> eSkateBoards = new List<ESkateBoard>
            {
                new ESkateBoard
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[0].Id,
                    DeviceType = DeviceType.ESkateBoard,
                    Manufacturer = "Boosted",
                    Model = "Boosted Stealth",
                    Name = "Boosted Beast",
                    BatteryCapacity = 19900,
                    MaxChargingSpeed = 0.5, 
                    BatteryVoltage = 44,
                    CanBeLocked = false,
                },
                new ESkateBoard
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[1].Id,
                    DeviceType = DeviceType.ESkateBoard,
                    Manufacturer = "Meepo",
                    Model = "Meepo V4 Shuffle",
                    Name = "Meepo Cruiser",
                    BatteryCapacity = 14400,
                    MaxChargingSpeed = 0.4,
                    BatteryVoltage = 36,
                    CanBeLocked = true,
                },
                new ESkateBoard
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[3].Id,
                    DeviceType = DeviceType.ESkateBoard,
                    Manufacturer = "Evolve",
                    Model = "Bamboo GTR",
                    Name = "Evolve Off-Road",
                    BatteryCapacity = 18000,
                    MaxChargingSpeed = 0.6,
                    BatteryVoltage = 44,
                    CanBeLocked = true,
                },
                new ESkateBoard
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = applicationUsers[4].Id,
                    DeviceType = DeviceType.ESkateBoard,
                    Manufacturer = "WowGo",
                    Model = "WowGo 3X",
                    Name = "WowGo Speedster",
                    BatteryCapacity = 15000,
                    MaxChargingSpeed = 0.5,
                    BatteryVoltage = 36,
                    CanBeLocked = false,
                }
            };

            List<ChargingStation> chargingStations = new List<ChargingStation>{

                new ChargingStation
                {
                    Id = Guid.Parse("734aa62b-dcaa-4815-a659-24d1fcaa3b18"),
                    Name = "Budapest Micromobility Hub - West",
                    Latitude = 47.49801,
                    Longitude = 19.03991,
                    Address = "101 Budapest Blvd"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("2f3820bc-ba7d-487b-897e-12d2c5041ce5"),
                    Name = "Budapest Micromobility Hub - East",
                    Latitude = 47.50391,
                    Longitude = 19.11657,
                    Address = "45 Keleti út, Budapest"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"),
                    Name = "Szeged Micromobility Hub",
                    Latitude = 46.253,
                    Longitude = 20.148,
                    Address = "12 Tisza Street, Szeged"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("625307d2-d501-41e5-8fdf-77d151bdd260"),
                    Name = "Debrecen Micromobility Hub",
                    Latitude = 47.5316,
                    Longitude = 21.6273,
                    Address = "90 Debrecen Ring"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("e1621f6d-a8d5-4065-a537-0d2aa0adee96"),
                    Name = "Miskolc Micromobility Hub",
                    Latitude = 48.103,
                    Longitude = 20.778,
                    Address = "87 Miskolc Avenue"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("4db1ba58-c788-47cb-819c-08da571e5978"),
                    Name = "Pécs Micromobility Hub",
                    Latitude = 46.0727,
                    Longitude = 18.2323,
                    Address = "55 Pécs Drive"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("4fd34718-3872-4bf1-8fb5-5b30849fc80e"),
                    Name = "Győr Micromobility Hub",
                    Latitude = 47.6875,
                    Longitude = 17.6504,
                    Address = "33 Győr Center"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("74178acf-f8d4-4c2b-b55c-6f136b13bc39"),
                    Name = "Nyíregyháza Micromobility Hub",
                    Latitude = 47.9554,
                    Longitude = 21.7167,
                    Address = "72 Nyíregyháza Street"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("0d46cb77-0134-4850-99ec-8ac8d7105c26"),
                    Name = "Kecskemét Micromobility Hub",
                    Latitude = 46.9062,
                    Longitude = 19.6913,
                    Address = "23 Kecskemét Way"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("a57aff6e-a64c-4938-9a2c-974fc2505780"),
                    Name = "Vienna Micromobility Hub",
                    Latitude = 48.2082,
                    Longitude = 16.3738,
                    Address = "88 Vienna Central"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("91b1377a-33b8-4bf7-943c-8299d7f78343"),
                    Name = "Berlin Micromobility Hub",
                    Latitude = 52.52,
                    Longitude = 13.405,
                    Address = "102 Berlin Platz"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("c8c8281f-79b4-4a8a-b930-2c7af38d4e7b"),
                    Name = "Bratislava Micromobility Hub",
                    Latitude = 48.1486,
                    Longitude = 17.1077,
                    Address = "21 Bratislava Square"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("dba2e8e4-6d61-49af-b74a-4b9d927d0b2e"),
                    Name = "Prague Micromobility Hub",
                    Latitude = 50.0755,
                    Longitude = 14.4378,
                    Address = "77 Prague Center"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("f1e8c45b-2a91-4ff8-b3ec-24b2781c9d24"),
                    Name = "Brno Micromobility Hub",
                    Latitude = 49.1951,
                    Longitude = 16.6068,
                    Address = "63 Brno Main Street"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("1783ac8e-96fb-47c0-8c74-7bcac28cf383"),
                    Name = "Cluj-Napoca Micromobility Hub",
                    Latitude = 46.7712,
                    Longitude = 23.6236,
                    Address = "12 Cluj Avenue"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("5a9d3f31-c85d-47b1-9d8e-8e80e4a0943e"),
                    Name = "Zagreb Micromobility Hub",
                    Latitude = 45.815,
                    Longitude = 15.9819,
                    Address = "90 Zagreb Boulevard"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("eb8c3c7e-7044-4f32-9483-18a2a5f10ed9"),
                    Name = "Ljubljana Micromobility Hub",
                    Latitude = 46.0569,
                    Longitude = 14.5058,
                    Address = "54 Ljubljana Road"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("4e5f4cf4-8ed7-491f-b18e-7dbeed52b219"),
                    Name = "Kaposvár Micromobility Hub",
                    Latitude = 46.3594,
                    Longitude = 17.7827,
                    Address = "33 Kaposvár Ring"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("e8949cc0-566a-423c-beca-ef749acd972d"),
                    Name = "Székesfehérvár Micromobility Hub",
                    Latitude = 47.186,
                    Longitude = 18.4221,
                    Address = "11 Fehérvár Way"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("21aecd38-fae4-4f9b-b634-bb50ccaa32d8"),
                    Name = "Tatabánya Micromobility Hub",
                    Latitude = 47.584,
                    Longitude = 18.4039,
                    Address = "77 Tatabánya Route"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("4c4f3e6e-19d0-42e1-885f-35ec2e39fd1d"),
                    Name = "Eger Micromobility Hub",
                    Latitude = 47.9025,
                    Longitude = 20.3772,
                    Address = "45 Eger Main Road"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("b5f85c22-9d87-4e5b-bf56-45a1dc0bfa9a"),
                    Name = "Szeged Micromobility Hub - Belváros",
                    Latitude = 46.253,
                    Longitude = 20.1414,
                    Address = "34 Kárász Street, Szeged"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("7c414209-c0f1-4ef2-bc4d-863deec95bf2"),
                    Name = "Szeged Micromobility Hub - Újszeged",
                    Latitude = 46.2496,
                    Longitude = 20.1728,
                    Address = "22 Torontál tér, Szeged"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("e5bfb6da-4d5e-4c7c-9bcb-8b8dbf9a1a64"),
                    Name = "Szombathely Micromobility Hub",
                    Latitude = 47.2307,
                    Longitude = 16.6218,
                    Address = "58 Szombathely Road"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("340e8c31-6f52-4e7f-9f43-c2c1b9b9add4"),
                    Name = "Veszprém Micromobility Hub",
                    Latitude = 47.0933,
                    Longitude = 17.9115,
                    Address = "23 Veszprém Avenue"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("9cb9a098-3cfb-46dc-bd72-4f2a938323f7"),
                    Name = "Békéscsaba Micromobility Hub",
                    Latitude = 46.6834,
                    Longitude = 21.0887,
                    Address = "14 Békéscsaba Ring"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("bb0fdbc1-cc97-4601-9c81-ace35cf1aaba"),
                    Name = "Sopron Micromobility Hub",
                    Latitude = 47.6817,
                    Longitude = 16.5845,
                    Address = "66 Sopron Street"
                },
                new ChargingStation
                {
                    Id = Guid.Parse("2a466d50-82c3-4e64-9d79-bdf59fe2f25b"),
                    Name = "Zalaegerszeg Micromobility Hub",
                    Latitude = 46.845,
                    Longitude = 16.8416,
                    Address = "35 Zalaegerszeg Boulevard"
                }
            };

            List<ChargingPort> chargingPorts = new List<ChargingPort>{
                new ChargingPort
                {
                    Id = Guid.Parse("a91026c8-1dc2-4311-adf5-49402703d000"),
                    IsBeingUsed = false,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("734aa62b-dcaa-4815-a659-24d1fcaa3b18"),
                    PortNumber = 1,
                    MaxChargingSpeed = 1.6,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("b20b2723-5dc1-411c-8a30-6a0655a8481a"),
                    IsBeingUsed = true,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("734aa62b-dcaa-4815-a659-24d1fcaa3b18"),
                    PortNumber = 2,
                    MaxChargingSpeed = 1.5,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("8f7f6784-4e76-4fba-9f44-e014fc3f5940"),
                    IsBeingUsed = true,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("2f3820bc-ba7d-487b-897e-12d2c5041ce5"),
                    PortNumber = 1,
                    MaxChargingSpeed = 1.5,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("d2dea098-a325-4a2f-923e-a581ddd4791b"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("2f3820bc-ba7d-487b-897e-12d2c5041ce5"),
                    PortNumber = 2,
                    MaxChargingSpeed = 1.6,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("ef0f137a-37bb-45a6-b830-460467f74ceb"),
                    IsBeingUsed = true,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"),
                    PortNumber = 1,
                    MaxChargingSpeed = 1.2,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("2d1b1640-aae9-49db-aa61-c9331e6b1b7d"),
                    IsBeingUsed = true,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"),
                    PortNumber = 2,
                    MaxChargingSpeed = 1.5,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("838d79f1-89a9-4cc7-a992-febbb7804b14"),
                    IsBeingUsed = true,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"),
                    PortNumber = 3,
                    MaxChargingSpeed = 1.6,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("11b58460-2e0d-4d58-a6f5-39a3945a6289"),
                    IsBeingUsed = true,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"),
                    PortNumber = 4,
                    MaxChargingSpeed = 1.2,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("c2efcc7e-b977-4c8f-ad61-a2a93432a11c"),
                    IsBeingUsed = true,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("625307d2-d501-41e5-8fdf-77d151bdd260"),
                    PortNumber = 1,
                    MaxChargingSpeed = 1.6,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("1dcebbab-37a2-4473-9aff-dbc89f5f843f"),
                    IsBeingUsed = true,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("625307d2-d501-41e5-8fdf-77d151bdd260"),
                    PortNumber = 2,
                    MaxChargingSpeed = 1.6,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("08bcd0e3-5f9c-48f7-b345-a22b4c1961cb"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("625307d2-d501-41e5-8fdf-77d151bdd260"),
                    PortNumber = 3,
                    MaxChargingSpeed = 1.2,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("206213c1-7437-4340-aedc-91d5ba483d77"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("e1621f6d-a8d5-4065-a537-0d2aa0adee96"),
                    PortNumber = 1,
                    MaxChargingSpeed = 1.6,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("ab0d6c6e-f47f-4ae1-b77c-beb93b136842"),
                    IsBeingUsed = true,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("e1621f6d-a8d5-4065-a537-0d2aa0adee96"),
                    PortNumber = 2,
                    MaxChargingSpeed = 1.5,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("d972ac98-7f68-4690-a947-ba2f59d7a14c"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("e1621f6d-a8d5-4065-a537-0d2aa0adee96"),
                    PortNumber = 3,
                    MaxChargingSpeed = 1.5,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("6e934b8f-e509-4376-be7e-71f16309e7fe"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("e1621f6d-a8d5-4065-a537-0d2aa0adee96"),
                    PortNumber = 4,
                    MaxChargingSpeed = 1.6,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("8a5a4cba-d12d-4353-92c1-d60eefdaa712"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("4db1ba58-c788-47cb-819c-08da571e5978"),
                    PortNumber = 1,
                    MaxChargingSpeed = 1.5,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("21176d38-9fbb-41e0-8a51-638250daaf40"),
                    IsBeingUsed = false,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("4db1ba58-c788-47cb-819c-08da571e5978"),
                    PortNumber = 2,
                    MaxChargingSpeed = 1.2,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("e726c61a-30d2-4b58-a8a1-ab23750d6250"),
                    IsBeingUsed = true,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("4fd34718-3872-4bf1-8fb5-5b30849fc80e"),
                    PortNumber = 1,
                    MaxChargingSpeed = 1.5,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("cebbe124-3172-40f5-bc7a-b420a9e65459"),
                    IsBeingUsed = true,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("4fd34718-3872-4bf1-8fb5-5b30849fc80e"),
                    PortNumber = 2,
                    MaxChargingSpeed = 1.6,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("01d024a5-fe51-4419-a872-1a19f98db4c7"),
                    IsBeingUsed = true,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("4fd34718-3872-4bf1-8fb5-5b30849fc80e"),
                    PortNumber = 3,
                    MaxChargingSpeed = 1.2,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("494aebb3-23d3-4d91-99f8-7f86c60f5ea3"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("4fd34718-3872-4bf1-8fb5-5b30849fc80e"),
                    PortNumber = 4,
                    MaxChargingSpeed = 1.5,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("a05290a1-01fe-489a-b233-3063ab451048"),
                    IsBeingUsed = true,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("74178acf-f8d4-4c2b-b55c-6f136b13bc39"),
                    PortNumber = 1,
                    MaxChargingSpeed = 1.6,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("445cba27-5324-4b2d-a00d-3d12f0a32645"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("74178acf-f8d4-4c2b-b55c-6f136b13bc39"),
                    PortNumber = 2,
                    MaxChargingSpeed = 1.6,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("cbfa84da-a918-4f6e-ae7c-2319fbdc911a"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("74178acf-f8d4-4c2b-b55c-6f136b13bc39"),
                    PortNumber = 3,
                    MaxChargingSpeed = 1.2,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("b42e7da5-cbb1-407f-9e1e-0bfa290db559"),
                    IsBeingUsed = false,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("0d46cb77-0134-4850-99ec-8ac8d7105c26"),
                    PortNumber = 1,
                    MaxChargingSpeed = 1.5,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("dd8dd71b-2961-449a-860d-dfef294d2a8d"),
                    IsBeingUsed = true,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("0d46cb77-0134-4850-99ec-8ac8d7105c26"),
                    PortNumber = 2,
                    MaxChargingSpeed = 1.5,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("c503e753-9388-4cd2-bb34-78ba2cc352e1"),
                    IsBeingUsed = true,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("0d46cb77-0134-4850-99ec-8ac8d7105c26"),
                    PortNumber = 3,
                    MaxChargingSpeed = 1.6,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("82a4a1a6-210e-4c0e-b96f-b9d0109acd59"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("a57aff6e-a64c-4938-9a2c-974fc2505780"),
                    PortNumber = 1,
                    MaxChargingSpeed = 1.2,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("68de7fec-ae57-43c3-920f-c976d9c1b464"),
                    IsBeingUsed = true,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("a57aff6e-a64c-4938-9a2c-974fc2505780"),
                    PortNumber = 2,
                    MaxChargingSpeed = 1.6,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("1c1351ab-a67b-4f64-9859-8a6403e6ef95"),
                    IsBeingUsed = true,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("a57aff6e-a64c-4938-9a2c-974fc2505780"),
                    PortNumber = 3,
                    MaxChargingSpeed = 1.2,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("d128636e-95c4-4728-acc8-0468d02f54f7"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("a57aff6e-a64c-4938-9a2c-974fc2505780"),
                    PortNumber = 4,
                    MaxChargingSpeed = 1.6,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.Parse("eb59229f-a0d4-49a3-a1cc-f3504451ace1"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("91b1377a-33b8-4bf7-943c-8299d7f78343"),
                    PortNumber = 1,
                    MaxChargingSpeed = 1.2,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("f11dac41-e9c0-4395-9e29-9aa65d4cee85"),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("91b1377a-33b8-4bf7-943c-8299d7f78343"),
                    PortNumber = 2,
                    MaxChargingSpeed = 1.6,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.Parse("fd342c7a-e418-4480-8dfb-e3c8611cdcdf"),
                    IsBeingUsed = true,
                    IsDisabled = true,
                    ChargingStationId = Guid.Parse("91b1377a-33b8-4bf7-943c-8299d7f78343"),
                    PortNumber = 3,
                    MaxChargingSpeed = 1.5,
                    IsCharging = true
                },
                new ChargingPort
                {
                    Id = Guid.NewGuid(),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"),
                    PortNumber = 45,
                    MaxChargingSpeed = 1.5,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.NewGuid(),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"),
                    PortNumber = 46,
                    MaxChargingSpeed = 1.5,
                    IsCharging = false
                },
                new ChargingPort
                {
                    Id = Guid.NewGuid(),
                    IsBeingUsed = false,
                    IsDisabled = false,
                    ChargingStationId = Guid.Parse("b3cab32d-4d07-4f9f-ad73-00d1ab6f909f"),
                    PortNumber = 47,
                    MaxChargingSpeed = 1.5,
                    IsCharging = false
                }
            };

            List<ChargingInstance> chargingInstances = new List<ChargingInstance>
            {
                  new ChargingInstance
                {
                    Id = Guid.NewGuid(),
                    ChargingPortId = chargingPorts[0].Id,
                    DeviceId = eBikes[4].Id,
                    StartingPercentage = 40,
                    EndPercentage = 100,
                    Start = new DateTime(2024, 2, 10, 14, 30, 0), // 2024.02.10. 14:30
                    End = new DateTime(2024, 2, 10, 16, 45, 0),   // 2024.02.10. 16:45
                    DesiredEndPercentage = 100,
                },
                new ChargingInstance
                {
                    Id = Guid.NewGuid(),
                    ChargingPortId = chargingPorts[1].Id,
                    DeviceId = eScooters[2].Id,
                    StartingPercentage = 50,
                    EndPercentage =100,
                    Start = new DateTime(2024, 2, 11, 9, 15, 0),
                    End = new DateTime(2024, 2, 11, 11, 30, 0),
                    DesiredEndPercentage = 100,
                },
                new ChargingInstance
                {
                    Id = Guid.NewGuid(),
                    ChargingPortId = chargingPorts[0].Id,
                    DeviceId = eBikes[1].Id,
                    StartingPercentage=60,
                    EndPercentage=100,
                    Start = new DateTime(2024, 2, 12, 17, 0, 0),
                    End = new DateTime(2024, 2, 12, 19, 15, 0),
                    DesiredEndPercentage = 100,
                },
                new ChargingInstance
                {
                    Id = Guid.NewGuid(),
                    ChargingPortId = chargingPorts[2].Id,
                    DeviceId = eSkateBoards[3].Id,
                    StartingPercentage = 70,
                    EndPercentage=99,
                    Start = new DateTime(2024, 2, 13, 22, 45, 0),
                    End = new DateTime(2024, 2, 14, 1, 0, 0),
                    DesiredEndPercentage = 99,
                },
                new ChargingInstance
                {
                    Id = Guid.NewGuid(),
                    ChargingPortId = chargingPorts[3].Id,
                    DeviceId = eBikes[6].Id,
                    StartingPercentage = 10,
                    EndPercentage=82,
                    Start = new DateTime(2024, 2, 15, 8, 0, 0),
                    End = new DateTime(2024, 2, 15, 10, 30, 0),
                    DesiredEndPercentage = 85,
                }
            };

            List<ErrorTicket> errorTickets = new List<ErrorTicket>
            {
                new ErrorTicket
                {
                    Id = Guid.NewGuid(),
                    ChargingStationId = chargingStations[0].Id,
                    Description = "Port 2 does not work.",
                    IsSolved = false
                },
                new ErrorTicket
                {
                    Id = Guid.NewGuid(),
                    ChargingStationId = chargingStations[0].Id,
                    Description = "Port 3 is physically damaged and cannot be used.",
                    IsSolved = false
                },
                new ErrorTicket
                {
                    Id = Guid.NewGuid(),
                    ChargingStationId = chargingStations[0].Id,
                    Description = "No power at the charging station",
                    IsSolved = true
                },
                new ErrorTicket
                {
                    Id = Guid.NewGuid(),
                    ChargingStationId = chargingStations[1].Id,
                    Description = "Multiple ports are damaged",
                    IsSolved = false
                },
                new ErrorTicket
                {
                    Id = Guid.NewGuid(),
                    ChargingStationId = chargingStations[1].Id,
                    Description = "Some people vandalised multiple ports",
                    IsSolved = true
                },
            };


            modelBuilder.Entity<IdentityRole<Guid>>().HasData(roles);
            modelBuilder.Entity<ApplicationUser>().HasData(applicationUsers);
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(userRoles);
            modelBuilder.Entity<EBike>().HasData(eBikes);
            modelBuilder.Entity<EScooter>().HasData(eScooters);
            modelBuilder.Entity<ESkateBoard>().HasData(eSkateBoards);
            modelBuilder.Entity<ChargingStation>().HasData(chargingStations);
            modelBuilder.Entity<ChargingPort>().HasData(chargingPorts);
            modelBuilder.Entity<ErrorTicket>().HasData(errorTickets);
            modelBuilder.Entity<ChargingInstance>().HasData(chargingInstances);

            modelBuilder.Entity<ChargingPort>()
                .HasOne(p => p.ChargingStation) 
                .WithMany(s => s.Ports) 
                .HasForeignKey(p => p.ChargingStationId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Device>()
                .HasOne(d =>d.ApplicationUser)
                .WithMany(a => a.Devices)
                .HasForeignKey(d=>d.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChargingInstance>()
            .HasOne(ci => ci.Device)
            .WithMany(d => d.ChargingInstances)
            .HasForeignKey(ci => ci.DeviceId)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ChargingInstance>()
           .HasOne(ci => ci.ChargingPort)
           .WithMany(d => d.ChargingInstances)
           .HasForeignKey(ci => ci.ChargingPortId)
           .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ErrorTicket>()
           .HasOne(et => et.ChargingStation)
           .WithMany(ct => ct.ErrorTickets)
           .HasForeignKey(et => et.ChargingStationId)
           .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
