using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core
{
    public interface IAction
    {
        string Name {
            get;
        }

        void Do();
        void Undo();
    }
}
