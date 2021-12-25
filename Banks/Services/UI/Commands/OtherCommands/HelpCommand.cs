using Banks.Tools;

namespace Banks.Services.UI.Commands.OtherCommands
{
    public class HelpCommand : ICommand
    {
        private IUserInterface _userInterface;
        public HelpCommand(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            _userInterface.Write("Some commands:");
            _userInterface.Write("block client - blocking someone");
            _userInterface.Write("unblock client - unblocking someone");
            _userInterface.Write("create account - create new account");
            _userInterface.Write("create bank - create new bank");
            _userInterface.Write("create client - create new client");
            _userInterface.Write("make replenishment - make replenishment in smth account");
            _userInterface.Write("make translation - make translation in smth account");
            _userInterface.Write("make withdrawal - make withdrawal in smth account");
            _userInterface.Write("print accounts - print all accounts");
            _userInterface.Write("print banks - print all banks");
            _userInterface.Write("print clients - print all clients");
            _userInterface.Write("print settings - print smth bank settings");
            _userInterface.Write("print transactions - print all transactions");
            _userInterface.Write("print concrete account - print smth account info");
            _userInterface.Write("print concrete bank - print smth bank info");
            _userInterface.Write("print concrete client - print smth client info");
            _userInterface.Write("print concrete transaction - print smth transaction info");
            _userInterface.Write("quit - end of program");
            _userInterface.Write("set client name - ...");
            _userInterface.Write("set client surname - ...");
            _userInterface.Write("set client passport - ...");
            _userInterface.Write("set client address - ...");
            _userInterface.Write("set settings - set smth bank settings");
            _userInterface.Write("? - help command");
            _userInterface.Write("wait 15 - wait 15 days.");
            return true;
        }
    }
}