using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Banks.Exceptions;
using Banks.Tools.Banks;
using Banks.Tools.ClientPart;

namespace Banks.Tools.CentralBankTools
{
    public class AllClients
    {
        private List<Client> _clients;
        private uint _currentNumberId;

        public AllClients()
        {
            _clients = new List<Client>();
            _currentNumberId = 1;
        }

        public ImmutableList<Client> ImmutableClients => _clients.ToImmutableList();
        public uint AddClient_ReturnID(Bank bank, string name, string surname)
        {
            var newClient = new Client(bank, _currentNumberId, name, surname);
            _currentNumberId++;
            _clients.Add(newClient);
            return newClient.Id;
        }

        public Client FindClient(uint id)
        {
            if (!IsCorrectClientId(id))
                throw new BankException("Client doesn't exist.");
            return _clients.FirstOrDefault(client => client.Id == id);
        }

        public void RenameClient(uint id, string newName)
        {
            FindClient(id).Name = newName;
        }

        public void ResurnameClient(uint id, string newSurname)
        {
            FindClient(id).Surname = newSurname;
        }

        public void RepassportClient(uint id, string newPassport)
        {
            FindClient(id).Passport = newPassport;
        }

        public void ReaddressClient(uint id, string newAddress)
        {
            FindClient(id).Address = newAddress;
        }

        public void BlockClient(uint id, AllAccounts accounts, AllTransactions transactions)
            => FindClient(id).BlockHim(accounts, transactions);

        public bool IsCorrectClientId(uint clientId)
            => _clients.Any(client => client.Id == clientId);
    }
}