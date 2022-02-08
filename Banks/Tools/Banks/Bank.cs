using System.Linq;
using Banks.Tools.Accounts;
using Banks.Tools.BankSetting;
using Banks.Tools.ClientPart;

namespace Banks.Tools.Banks
{
    public class Bank
    {
        public Bank(BankSettings bankSettings, string name, uint id)
        {
            Settings = bankSettings;
            Name = name;
            Id = id;
        }

        public BankSettings Settings { get; }
        public string Name { get; set; }
        public uint Id { get; }

        public ClientList BankClients(AllClients allClients)
        {
            var clients = new ClientList();
            foreach (Client client in allClients.ImmutableClients.Where(client => client.IsBankId(Id))) clients.AddClient(client);

            return clients;
        }
    }
}