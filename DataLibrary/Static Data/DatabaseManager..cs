using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Useraccounts.Interfaces;

namespace DataLibrary.Static_Data
{
    public static class DatabaseManager
    {
        static Mock mock = new Mock();

        public static Mock GetMock()
        {
            return mock;
        } 
    }
}
