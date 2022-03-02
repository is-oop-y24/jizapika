using System;
using Banks.Services.UI.Commands.Helpers;
using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands.CommandSet
{
    public class SetClientName : ICommand
    {
        private IUserInterface _userInterface;

        public SetClientName(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            string clientIdString = _userInterface.WriteAndRead("Enter client id.");
            while (!IsCorrectIdHelper.IsUint(clientIdString) && IsCorrectIdHelper.IsCorrectClientId(centralBank, clientIdString))
                clientIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
            uint clientId = uint.Parse(clientIdString);

            string newClientName = _userInterface.WriteAndRead("Enter new name.");
            centralBank.RenameClient(clientId, newClientName);

            return true;
        }
    }
}