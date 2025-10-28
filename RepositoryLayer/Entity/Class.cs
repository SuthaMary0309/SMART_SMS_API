using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Class
    {
        public Guid ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string Grade { get; set; }
    }
}
