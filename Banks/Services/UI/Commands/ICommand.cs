using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands
{
    public interface ICommand
    {
        public bool RunCommand(out bool shouldQuit, CentralBank centralBank);
    }
}