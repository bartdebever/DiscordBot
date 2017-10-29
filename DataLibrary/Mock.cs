using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Interfaces;

namespace DataLibrary
{
    public class Mock : DbContext
    {
        /// <summary>
        /// Used to test the EntityFramework and how our system functions.
        /// </summary>
        public DbSet<IUser> Users { get; set; }
    }


}
