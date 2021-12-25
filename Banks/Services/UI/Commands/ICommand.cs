using Banks.Services.UI;
using Banks.Services.UI.Commands.CommandCreate;
using Banks.Tools;

namespace Banks.Services.UI.Commands
{
    public interface ICommand
    {
        public bool RunCommand(out bool shouldQuit, CentralBank centralBank);
    }
}