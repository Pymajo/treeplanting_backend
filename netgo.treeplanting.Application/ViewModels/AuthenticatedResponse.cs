using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Application.ViewModels
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
        public Guid? Id { get; set; }
    }
}
