using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Banks.Exceptions;
using Banks.Tools.BankSetting;

namespace Banks.Tools.Banks
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
        public Bank AddBank(BankSettings bankSettings, string name)
        {
            var bank = new Bank(bankSettings, name, _currentNumberId);
            _banks.Add(bank);
            _currentNumberId++;
            return bank;
        }

        public Bank FindBank(uint id)
        {
            foreach (Bank bank in _banks.Where(bank => bank.Id == id))
            {
                return bank;
            }

            throw new BankException($"Not correct id");
        }

        public Bank RenameBank(uint id, string newName)
        {
            Bank bank = FindBank(id);
            bank.Name = newName;
            return bank;
        }
    }
}