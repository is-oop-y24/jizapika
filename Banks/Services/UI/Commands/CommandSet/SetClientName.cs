using System;
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
            while (!IsUint(clientIdString) && IsCorrectClientId(centralBank, clientIdString))
                clientIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
            uint clientId = uint.Parse(clientIdString);

            string newClientName = _userInterface.WriteAndRead("Enter new name.");
            centralBank.Clients.FindClient(clientId).Name = newClientName;

            return true;
        }

        private bool IsUint(string command)
        {
            if (command.Split(' ').Length > 1) return false;
            if (!uint.TryParse(command.Split(' ')[0], out _)) return false;
            return true;
        }

        private bool IsCorrectClientId(CentralBank centralBank, string clientIdString)
        {
            try
            {
                centralBank.Clients.FindClient(uint.Parse(clientIdString));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}