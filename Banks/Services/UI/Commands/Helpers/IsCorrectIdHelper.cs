using System;
using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands.Helpers
{
    public class IsCorrectIdHelper
    {
        public static bool IsUint(string command)
            => command.Split(' ').Length <= 1 && uint.TryParse(command.Split(' ')[0], out _);

        public static bool IsCorrectBankId(CentralBank centralBank, string bankIdString)
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

        public static bool IsCorrectClientId(CentralBank centralBank, string clientIdString)
        {
            try
            {
                return centralBank.IsCorrectClientId(uint.Parse(clientIdString));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsCorrectAccountId(CentralBank centralBank, string accountIdString)
        {
            try
            {
                return centralBank.IsCorrectAccountId(uint.Parse(accountIdString));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsCorrectTransactionId(CentralBank centralBank, string transactionIdString)
        {
            try
            {
                return centralBank.IsCorrectTransactionId(uint.Parse(transactionIdString));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}