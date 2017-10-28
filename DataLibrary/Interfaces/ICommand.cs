using Discord.Commands;

namespace DataLibrary.Interfaces
{
    public interface ICommand : ICommandContext
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Help { get; set; }
        void Run();
    }
}