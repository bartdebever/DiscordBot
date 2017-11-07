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
        private static readonly Mock Mock = new Mock();

        public static Mock GetMock()
        {
            return Mock;
        } 
    }
}
