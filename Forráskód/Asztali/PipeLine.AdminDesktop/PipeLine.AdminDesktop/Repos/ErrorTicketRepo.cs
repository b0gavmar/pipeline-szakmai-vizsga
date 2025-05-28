using Mysqlx.Crud;
using PipeLine.Shared.Models.ErrorTicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.Repos
{
    public class ErrorTicketRepo
    {
        #region Database
        List<ErrorTicket> _errorTickets = new()
        {
            new ErrorTicket
                {
                    Id = Guid.NewGuid(),
                    Description = "Charging station is not responding.",
                    IsSolved = false
                },
                new ErrorTicket
                {
                    Id = Guid.NewGuid(),
                    Description = "Port 3 is physically damaged and cannot be used.",
                    IsSolved = false
                },
                new ErrorTicket
                {
                    Id = Guid.NewGuid(),
                    Description = "No power supply at the charging station.",
                    IsSolved = true
                },
                new ErrorTicket
                {
                    Id = Guid.NewGuid(),
                    Description = "Charging session was interrupted unexpectedly.",
                    IsSolved = false
                },
                new ErrorTicket
                {
                    Id = Guid.NewGuid(),
                    Description = "Error code E104: Overheating detected.",
                    IsSolved = true
                },
        };
        #endregion

        public List<ErrorTicket> FindAll()
        {
            return _errorTickets;
        }

        public void Delete(ErrorTicket errorTicket)
        {
            _errorTickets.Remove(errorTicket);
        }

        public void Save(ErrorTicket errorTicket)
        {
            if (errorTicket.Id != Guid.Empty)
                Update(errorTicket);
            else
                Insert(errorTicket);
        }

        private void Insert(ErrorTicket errorTicket)
        {
            _errorTickets.Add(errorTicket);
        }

        private void Update(ErrorTicket errorTicket)
        {
            ErrorTicket? errorTicketToUpdate = _errorTickets.FirstOrDefault(e => e.Id == errorTicket.Id);
        }
    }
}