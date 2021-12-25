using System;
using Banks.Tools;

namespace Banks.Services.UI.Commands.CommandMake
{
    public class MakeReplenishment : ICommand
    {
        private IUserInterface _userInterface;

        public MakeReplenishment(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                string accountIdString = _userInterface.WriteAndRead("Enter account id.");
                while (!IsUint(accountIdString) && IsCorrectAccountId(centralBank, accountIdString))
                    accountIdString = _userInterface.WriteAndRead("Not correct. Please, try again");
                uint accountId = uint.Parse(accountIdString);

                double replenishmentSum = double.MinValue;
                while (replenishmentSum < 0)
                {
                    try
                    {
                        replenishmentSum =
                            double.Parse(_userInterface.WriteAndRead(
                                "Enter replenishment sum: "));
                    }
                    catch (Exception)
                    {
                        _userInterface.Write("Parse failed. Try again.");
                    }
                }

                centralBank.MakeReplenishment(centralBank.Accounts.FindAccount(accountId), replenishmentSum);

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