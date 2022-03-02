using System;
using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands.TimeCommands
{
    public class WaitCommand : ICommand
    {
        private IUserInterface _userInterface;
        private uint _daysNumber;
        public WaitCommand(IUserInterface userInterface, uint daysNumber)
        {
            _userInterface = userInterface;
            _daysNumber = daysNumber;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                centralBank.AddDays(_daysNumber);
                _userInterface.Write($"{_daysNumber} day(s) was skipped.");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}