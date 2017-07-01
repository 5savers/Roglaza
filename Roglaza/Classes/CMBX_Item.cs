using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roglaza.Classes
{
    public class CMBX_Item
    {
        public string Text = "";
        public string Value = "";
        public CMBX_Item(string text,string val)
        {
            Text = text;
            Value = val;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
