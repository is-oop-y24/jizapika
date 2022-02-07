using System;
using Banks.Services.UI.Commands.CommandCreate;
using Banks.Tools;

namespace Banks.Services.UI.Commands.CommandMake
{
    public class MakeWithdrawal : ICommand
    {
        private IUserInterface _userInterface;

        public MakeWithdrawal(IUserInterface userInterface)
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

                double withdrawalSum = double.MinValue;
                while (withdrawalSum < 0)
                {
                    try
                    {
                        withdrawalSum =
                            double.Parse(_userInterface.WriteAndRead(
                                "Enter withdrawal sum: "));
                    }
                    catch (Exception)
                    {
                        _userInterface.Write("Parse failed. Try again.");
                    }
                }

                centralBank.AddReplenishment(centralBank.Accounts.FindAccount(accountId), withdrawalSum).MakeIt();

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