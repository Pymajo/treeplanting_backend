using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Application.ViewModels
{
    public class TreecoinsViewModel
    {
        public Guid Id { get; protected set; }
        public int Treecoins { get; protected set; }
        public TreecoinsViewModel(Guid id, int treecoins)
        {
            Id = id;
            Treecoins = treecoins;
        }
    }
}
