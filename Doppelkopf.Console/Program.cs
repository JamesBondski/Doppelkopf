using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Console
{
    class Program
    {
        static Dictionary<string, Command> commands = new Dictionary<string, Command>();

        static void AddCommand(Command command) {
            commands.Add(command.Keyword, command);
        }

        static void Main(string[] args) {
            State state = new State();

            AddCommand(new NewGameCommand());
            AddCommand(new ShowHandsCommand());
            AddCommand(new QuitCommand());
            AddCommand(new PlayTrickCommand());

            while(!state.DoQuit) {
                System.Console.Write("? ");
                string command = System.Console.ReadLine();
                string[] parts = command.Split(' ');

                if(commands.ContainsKey(parts[0])) {
                    string arg = parts.Length > 1 ? parts[1] : null;
                    commands[parts[0]].Execute(state, arg);
                }
                else {
                    System.Console.WriteLine("Unknown command.");
                }
            }
        }
    }
}
