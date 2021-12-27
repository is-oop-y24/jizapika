using System;
using Banks.Tools;

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
                                     + $"client name: {centralBank.Clients.FindClient(_id).Name}, "
                                     + $"client surname: {centralBank.Clients.FindClient(_id).Surname}, "
                                     + $"client passport: {centralBank.Clients.FindClient(_id).Passport}, "
                                     + $"client address: {centralBank.Clients.FindClient(_id).Address}, "
                                     + $"is client approved: {centralBank.Clients.FindClient(_id).IsApproved()}.");
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