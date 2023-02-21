using netgo.treeplanting.Domain.Commands.Seedling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Validations.Seedling
{
    public class EditSeedlingCommandValidation: SeedlingCommandValidation<EditSeedlingCommand>
    {
        public EditSeedlingCommandValidation()
        {
            ValidateSeedlingId();
        }
    }
}
