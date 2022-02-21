using System;
using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands.CommandMake
{
    public class MakeTranslation : ICommand
    {
        private IUserInterface _userInterface;

        public MakeTranslation(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                string fromAccountIdString = _userInterface.WriteAndRead("Enter from-account id.");
                while (!IsUint(fromAccountIdString) && IsCorrectAccountId(centralBank, fromAccountIdString))
                    fromAccountIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                uint fromAccountId = uint.Parse(fromAccountIdString);

                string toAccountIdString = _userInterface.WriteAndRead("Enter to-account id.");
                while (!IsUint(toAccountIdString) && IsCorrectAccountId(centralBank, toAccountIdString))
                    toAccountIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                uint toAccountId = uint.Parse(toAccountIdString);

                double translationSum = double.MinValue;
                while (translationSum < 0)
                {
                    try
                    {
                        translationSum =
                            double.Parse(_userInterface.WriteAndRead(
                                "Enter translation sum: "));
                    }
                    catch (Exception)
                    {
                        _userInterface.Write("Parse failed. Try again.");
                    }
                }

                centralBank.MakeNewTranslation(fromAccountId, toAccountId, translationSum);

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

        private bool IsCorrectAccountId(CentralBank centralBank, string accountIdString)
        {
            try
            {
                centralBank.Clients.FindClient(uint.Parse(accountIdString));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}