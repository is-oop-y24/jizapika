using Banks.Services.UI.Commands.CommandBlock;
using Banks.Services.UI.Commands.CommandCreate;
using Banks.Services.UI.Commands.CommandMake;
using Banks.Services.UI.Commands.CommandPrint;
using Banks.Services.UI.Commands.CommandPrint.CommandPrintConcrete;
using Banks.Services.UI.Commands.CommandQuit;
using Banks.Services.UI.Commands.CommandSet;
using Banks.Services.UI.Commands.OtherCommands;
using Banks.Services.UI.Commands.TimeCommands;

namespace Banks.Services.UI.Commands
{
    public class CommandDirector
    {
        private IUserInterface _userInterface;

        public CommandDirector(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public ICommand Command(string command)
        {
            switch (FirstWord(command))
            {
                case "create":
                    return CreateDirector(DeleteFirstWord(command));
                case "print":
                    return PrintDirector(DeleteFirstWord(command));
                case "make":
                    return MakeDirector(DeleteFirstWord(command));
                case "set":
                    return SetDirector(DeleteFirstWord(command));
                case "wait":
                    return WaitDirector(DeleteFirstWord(command));
                case "block":
                    return BlockDirector(DeleteFirstWord(command));
                case "quit":
                    return new QuitCommand(_userInterface);
                case "?":
                    return new HelpCommand(_userInterface);
                default:
                    return new UnknownCommand(_userInterface);
            }
        }

        private string FirstWord(string command)
        {
            return command.Split(' ')[0];
        }

        private string DeleteFirstWord(string command)
        {
            string[] line = command.Split(' ');
            string returnCommand = string.Empty;
            for (int i = 1; i < line.Length; i++)
            {
                returnCommand = string.Concat(returnCommand, line[i]);
            }

            return returnCommand;
        }

        private int HowManyWords(string command)
        {
            return command.Split(' ').Length;
        }

        // create
        private ICommand CreateDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("What do you want to create?");

            switch (FirstWord(command))
            {
                case "bank":
                    return new CreateBank(_userInterface);
                case "client":
                    return new CreateClient(_userInterface);
                default:
                    return new UnknownCommand(_userInterface);
            }
        }

        // make
        private ICommand MakeDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("What do you want to make?");

            switch (FirstWord(command))
            {
                case "Replenishment":
                    return new MakeReplenishment(_userInterface);
                case "Translation":
                    return new MakeTranslation(_userInterface);
                case "Withdrawal":
                    return new MakeWithdrawal(_userInterface);
                default:
                    return new UnknownCommand(_userInterface);
            }
        }

        // print
        private ICommand PrintDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("What do you want to print?");

            switch (FirstWord(command))
            {
                case "banks":
                    return new PrintBanks(_userInterface);
                case "users":
                    return new PrintClients(_userInterface);
                case "accounts":
                    return new PrintAccounts(_userInterface);
                case "transactions":
                    return new PrintTransactions(_userInterface);
                case "settings":
                    return new PrintBankSettings(_userInterface);
                case "concrete":
                    return PrintConcreteDirector(DeleteFirstWord(command));
                default:
                    return new UnknownCommand(_userInterface);
            }
        }

        // print concrete
        private ICommand PrintConcreteDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("What do you want to print?");

            switch (FirstWord(command))
            {
                case "bank":
                    return PrintConcreteBankDirector(DeleteFirstWord(command));
                case "client":
                    return PrintConcreteClientDirector(DeleteFirstWord(command));
                case "account":
                    return PrintConcreteAccountDirector(DeleteFirstWord(command));
                case "transaction":
                    return PrintConcreteTransactionDirector(DeleteFirstWord(command));
                default:
                    return new UnknownCommand(_userInterface);
            }
        }

        // print concrete bank
        private ICommand PrintConcreteBankDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("What's id of this bank?");

            if (HowManyWords(command) > 1) return new UnknownCommand(_userInterface);
            if (!uint.TryParse(FirstWord(command), out _)) return new UnknownCommand(_userInterface);
            return new PrintConcreteBank(_userInterface, uint.Parse(command));
        }

        // print concrete client
        private ICommand PrintConcreteClientDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("What's id of this client?");

            if (HowManyWords(command) > 1) return new UnknownCommand(_userInterface);
            if (!uint.TryParse(FirstWord(command), out _)) return new UnknownCommand(_userInterface);
            return new PrintConcreteBank(_userInterface, uint.Parse(command));
        }

        // print concrete account
        private ICommand PrintConcreteAccountDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("What's id of this account?");

            if (HowManyWords(command) > 1) return new UnknownCommand(_userInterface);
            if (!uint.TryParse(FirstWord(command), out _)) return new UnknownCommand(_userInterface);
            return new PrintConcreteBank(_userInterface, uint.Parse(command));
        }

        // print concrete transaction
        private ICommand PrintConcreteTransactionDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("What's id of this transaction?");

            if (HowManyWords(command) > 1) return new UnknownCommand(_userInterface);
            if (!uint.TryParse(FirstWord(command), out _)) return new UnknownCommand(_userInterface);
            return new PrintConcreteBank(_userInterface, uint.Parse(command));
        }

        // set
        private ICommand SetDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("What do you want to set?");

            switch (FirstWord(command))
            {
                case "settings":
                    return new SetBankSettings(_userInterface);
                case "client":
                    return SetClientDirector(DeleteFirstWord(command));
                default:
                    return new UnknownCommand(_userInterface);
            }
        }

        // set client
        private ICommand SetClientDirector(string command)
        {
            if (HowManyWords(command) == 0)
            {
                command = _userInterface.WriteAndRead(
                    "What do you want to set with client? (name / surname / passport / address)");
            }

            if (HowManyWords(command) > 1) return new UnknownCommand(_userInterface);
            if (!uint.TryParse(FirstWord(command), out _)) return new UnknownCommand(_userInterface);
            switch (command)
            {
                case "name":
                    return new SetClientName(_userInterface);
                case "surname":
                    return new SetClientSurname(_userInterface);
                case "passport":
                    return new SetClientPassport(_userInterface);
                case "address":
                    return new SetClientAddress(_userInterface);
                default:
                    return new UnknownCommand(_userInterface);
            }
        }

        // wait
        private ICommand WaitDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("How many days do you want to skip?");

            if (HowManyWords(command) > 1) return new UnknownCommand(_userInterface);
            if (!uint.TryParse(FirstWord(command), out _)) return new UnknownCommand(_userInterface);
            return new WaitCommand(_userInterface, uint.Parse(command));
        }

        // block
        private ICommand BlockDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("What do you want to create?");

            switch (FirstWord(command))
            {
                case "client":
                    return BlockClientDirector(DeleteFirstWord(command));
                default:
                    return new UnknownCommand(_userInterface);
            }
        }

        // block client
        private ICommand BlockClientDirector(string command)
        {
            if (HowManyWords(command) == 0)
                command = _userInterface.WriteAndRead("What's id of this client?");

            if (HowManyWords(command) > 1) return new UnknownCommand(_userInterface);
            if (!uint.TryParse(FirstWord(command), out _)) return new UnknownCommand(_userInterface);
            return new BlockClient(_userInterface, uint.Parse(command));
        }
    }
}