using System;
using Banks.Services.UI.Commands.Helpers;
using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands.CommandCreate
{
    public class CreateClient : ICommand
    {
        private IUserInterface _userInterface;

        public CreateClient(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                string bankIdString = _userInterface.WriteAndRead("Enter bank id.");
                while (!IsUint(bankIdString) && IsCorrectBankId(centralBank, bankIdString))
                    bankIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                uint bankId = uint.Parse(bankIdString);
                uint thisClient = centralBank.AddClient_ReturnID(
                    bankId,
                    ClientInfoHelper.ClientName(_userInterface),
                    ClientInfoHelper.ClientSurname(_userInterface));
                string doWantPassport =
                    _userInterface.WriteAndRead("Do you want to add passport? Enter Y if yes.");
                string doWantAddress =
                    _userInterface.WriteAndRead("Do you want to add address? Enter Y if yes.");
                if (doWantPassport == "Y") centralBank.RepassportClient(thisClient, ClientInfoHelper.ClientPassport(_userInterface));
                if (doWantAddress == "Y") centralBank.ReaddressClient(thisClient, ClientInfoHelper.ClientAddress(_userInterface));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsUint(string command)
        {
            if (command.Split(' ').Length > 1) return false;
            if (!uint.TryParse(command.Split(' ')[0], out _)) return false;
            return true;
        }

        private bool IsCorrectBankId(CentralBank centralBank, string bankIdString)
        {
            try
            {
                return centralBank.IsCorrectBankId(uint.Parse(bankIdString));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}