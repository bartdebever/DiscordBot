using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Interfaces
{
    public interface ITournament
    {
        int Id { get; set; }
        string Name { get; set; }
        string Edition { get; set; }
        List<IGame> Games { get; set; }
        List<IServer> Servers { get; set; }
        TimeSpan TimeSpan { get; set; }
        string SignUpMethod { get; set; }
        string Description { get; set; }
        List<IUser> Attendees { get; set; }
        List<string> Link { get; set; }
        void AddLink(string link);
        void RemoveLink(string link);
    }
}
