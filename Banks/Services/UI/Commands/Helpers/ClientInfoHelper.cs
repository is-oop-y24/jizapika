using System;
using Banks.Tools.BankSetting.BankAccountsSettings;

namespace Banks.Services.UI.Commands.Helpers
{
    public class ClientInfoHelper
    {
        public static string ClientName(IUserInterface userInterface)
            => userInterface.WriteAndRead("Please, enter name of client:");
        public static string ClientSurname(IUserInterface userInterface)
            => userInterface.WriteAndRead("Please, enter surname of client:");
        public static string ClientPassport(IUserInterface userInterface)
            => userInterface.WriteAndRead("Please, enter passport of client:");
        public static string ClientAddress(IUserInterface userInterface)
            => userInterface.WriteAndRead("Please, enter address of client:");
    }
}