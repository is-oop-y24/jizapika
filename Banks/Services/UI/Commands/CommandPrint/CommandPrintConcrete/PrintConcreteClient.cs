using System;
using Banks.Tools.CentralBankTools;

namespace Banks.Services.UI.Commands.CommandPrint.CommandPrintConcrete
{
    public class PrintConcreteClient : ICommand
    {
        private uint _id;
        private IUserInterface _userInterface;

        public PrintConcreteClient(IUserInterface userInterface, uint id)
        {
            _id = id;
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                _userInterface.Write($"id: {_id}, "
                                     + $"client name: {centralBank.ClientName(_id)}, "
                                     + $"client surname: {centralBank.ClientSurname(_id)}, "
                                     + $"client passport: {centralBank.ClientPassport(_id)}, "
                                     + $"client address: {centralBank.ClientAddress(_id)}, "
                                     + $"is client approved: {centralBank.IsClientApproved(_id)}.");
                return true;
            }
            catch (Exception)
            {
                _userInterface.Write("Maybe id not correct. Try to command 'print clients' for watch correct ids.");
                return false;
            }
        }
    }
}