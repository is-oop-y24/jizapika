using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Banks.Exceptions;
using Banks.Tools.Accounts;
using Banks.Tools.Banks;
using Banks.Tools.Transactions;

namespace Banks.Tools.ClientPart
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
        public Client AddClient(Bank bank, string name, string surname)
        {
            var newClient = new Client(bank, _currentNumberId, name, surname);
            _currentNumberId++;
            _clients.Add(newClient);
            return newClient;
        }

        public Client FindClient(uint id)
            => _clients.FirstOrDefault(client => client.Id == id);

        public Client RenameClient(uint id, string newName)
        {
            Client client = FindClient(id);
            client.Name = newName;
            return client;
        }

        public Client ResurnameClient(uint id, string newSurname)
        {
            Client client = FindClient(id);
            client.Surname = newSurname;
            return client;
        }

        public Client RepassportClient(uint id, string newPassport)
        {
            Client client = FindClient(id);
            client.Passport = newPassport;
            return client;
        }

        public Client ReaddressClient(uint id, string newAddress)
        {
            Client client = FindClient(id);
            client.Address = newAddress;
            return client;
        }

        public Client BlockClient(uint id, AllAccounts accounts, AllTransactions transactions)
        {
            Client client = FindClient(id);
            client.BlockHim(accounts, transactions);
            return client;
        }
    }
}