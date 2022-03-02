using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Banks.Exceptions;
using Banks.Tools.Banks;
using Banks.Tools.BankSetting;

namespace Banks.Tools.CentralBankTools
{
    public class AllBanks
    {
        private List<Bank> _banks;
        private uint _currentNumberId = 1;

        public AllBanks()
        {
            _banks = new List<Bank>();
        }

        public ImmutableList<Bank> ImmutableBanks => _banks.ToImmutableList();
        public uint AddBank_ReturnID(BankSettings bankSettings, string name)
        {
            var bank = new Bank(bankSettings, name, _currentNumberId);
            _banks.Add(bank);
            _currentNumberId++;
            return bank.Id;
        }

        public Bank FindBank(uint id)
        {
            if (!IsCorrectBankId(id))
                throw new BankException("Bank doesn't exist.");
            return _banks.FirstOrDefault(bank => bank.Id == id);
        }

        public void RenameBank(uint id, string newName)
        {
            FindBank(id).Name = newName;
        }

        public bool IsCorrectBankId(uint bankId)
            => _banks.Any(bank => bank.Id == bankId);
    }
}