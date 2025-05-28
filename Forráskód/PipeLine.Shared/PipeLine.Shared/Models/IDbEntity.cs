using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Models
{
    public interface IDbEntity<TEntity> where TEntity : class, new()
    {
        public Guid Id { get; set; }
        public bool HasId => Id != Guid.Empty;

        public string GetDbSetName()
        {
            return string.Concat(new TEntity().GetType().Name, 's');
        }
    }
}
