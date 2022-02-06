using System.Collections.Generic;
using System.Collections.Immutable;

namespace Banks.Tools.ClientPart
{
    public class ClientList
    {
        private List<Client> _clients;

        public ClientList()
        {
            _clients = new List<Client>();
        }

        public ImmutableList<Client> ImmutableClients => _clients.ToImmutableList();

        public Client AddClient(Client client)
        {
            _clients.Add(client);
            return client;
        }
    }
}