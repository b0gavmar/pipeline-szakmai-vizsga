using PipeLine.Shared.Models.UserModels;

namespace PipeLine.AdminDesktop.Repos
{
    class ApplicationUserRepo
    {
        #region Database
        List<ApplicationUser> _applicationUsers = new()
            {
                 new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Tamás",
                    LastName = "Teszt",
                    UserName = "TamasTester",
                    Email = "tamastest@gmail.com",
                    PhoneNumber = "1234567890",
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Patrícia",
                    LastName = "Próba",
                    UserName = "PatiProba",
                    Email = "patiproba@gmail.com",
                    PhoneNumber = "0987654321",
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Bence",
                    LastName = "Beállítás",
                    UserName = "BenceB",
                    Email = null,
                    PhoneNumber = "111222333",
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Réka",
                    LastName = "Regisztráció",
                    UserName = "RekaR",
                    Email = "rekareg@gmail.com",
                    PhoneNumber = "444555666",
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Zoltán",
                    LastName = "Zárás",
                    UserName = "ZoliZ",
                    Email = null,
                    PhoneNumber = "777888999",
                }
            };
        #endregion

        public List<ApplicationUser> FindAll()
        {
            return _applicationUsers;
        }

        public void Delete(ApplicationUser applicationUser)
        {
            _applicationUsers.Remove(applicationUser);
        }
        public void Save(ApplicationUser applicationUser)
        {
            if (applicationUser.Id != Guid.Empty)
                Update(applicationUser);
            else
                Insert(applicationUser);
        }

        private void Insert(ApplicationUser applicationUser)
        {
            _applicationUsers.Add(applicationUser);
        }

        private void Update(ApplicationUser applicationUser)
        {
            ApplicationUser? applicationUserToUpdate = _applicationUsers.FirstOrDefault(u => u.Id == applicationUser.Id);
        }
    }
}
