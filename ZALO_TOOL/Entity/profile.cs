using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZALO_TOOL.Entity
{
    public class profile
    {
        private int _id;
        private bool _gender;
        private string _name;

        public profile()
        {
            Reset_Data();
        }

        private void Reset_Data()
        {
            _id = 0;
            _gender = true;
            _name = ""; 
        }

        public int id { get => _id; set => _id = value; }
        public bool gender { get => _gender; set => _gender = value; }
        public string name { get => _name; set => _name = value; }
    }
}
