using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roglaza.Classes
{
    class ManagedApp
    {
        public string name = "";
        public string path = "";

        public int start = -1;
        public int stop = -1;

        public override string ToString()
        {
            string s = start.ToString();
            string p = stop.ToString(); 

            string res = "From: " + s + "  |   To:" + p + "      ";
            while (res.Length < 30)
                res += " ";
            if (start > 9 )
                res = res.Substring(0, res.Length - 1);
            if( stop > 9)
                res = res.Substring(0, res.Length - 1);

            return res+"|   "+path;
        }
       
        public ManagedApp(string s)
	{
        if (s.Contains('|'))
        {
            string[] arr = s.Split('|');
            start = RoglazaHelper.str_to_int(arr[0], -1);
            stop = RoglazaHelper.str_to_int(arr[1], -1);
            path = arr[2].Trim();
            extract_name();

        }
    }

        public ManagedApp(int _start, int _stop, string _path)
        {
            // TODO: Complete member initialization
            this.start = _start;
            this.stop = _stop;
            this.path = _path;
            extract_name();

        }

        private void extract_name()
        {
            try
            {
                string[] temp = path.Split(new string[] { "\\", "/" }, StringSplitOptions.RemoveEmptyEntries);
                name = temp[temp.Length - 1];

            }
            catch { }
        }
    }
}
