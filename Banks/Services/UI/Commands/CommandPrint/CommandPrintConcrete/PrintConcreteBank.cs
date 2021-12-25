using System;
using Banks.Tools;

namespace Banks.Services.UI.Commands.CommandPrint.CommandPrintConcrete
{
    public class PrintConcreteBank : ICommand
    {
        private uint _id;
        private IUserInterface _userInterface;

        public PrintConcreteBank(IUserInterface userInterface, uint id)
        {
            _id = id;
            _userInterface = userInterface;
        }

        public bool RunCommand(out bool shouldQuit, CentralBank centralBank)
        {
            shouldQuit = false;
            try
            {
                _userInterface.Write($"id: {_id}" + $"name: {centralBank.Banks.FindBank(_id).Name}");
                return true;
            }
            catch (Exception)
            {
                _userInterface.Write("Maybe id not correct. Try to command 'print banks' for watch correct ids.");
                return false;
            }
        }
    }
}