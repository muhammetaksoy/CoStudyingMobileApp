using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ResetPassword : EntityBase
    {
        public string RandomPass { get; set; }
        public bool isEmail { get; set; }

        public string Email { get; set; }

    }
}
