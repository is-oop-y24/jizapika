using System.Collections.Generic;

namespace Banks.Tools.ClientPart
{
    public class ClientList
    {
        public ClientList()
        {
            Clients = new List<Client>();
        }

        public List<Client> Clients { get; }
        public Client AddClient(Client client)
        {
            Clients.Add(client);
            return client;
        }
    }
}