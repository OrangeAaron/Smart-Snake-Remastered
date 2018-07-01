using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Snake_Remastered.Logic
{
    public delegate void LifeHandler(object sender, LifeHandlerArgs e);

    public class LifeHandlerArgs : EventArgs  { }
}
