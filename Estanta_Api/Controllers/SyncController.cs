using System.Net;
using System.Net.Sockets;
using System.Text;
using Data.Models;
using Data.Models.Network;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Estanta_Api.Controllers;

[ApiController]
[Route("api/Sync")]
public class SyncController
{
    public SyncController()
    {
        var newUsersThread = new Thread(ReceiveUser);
        newUsersThread.Start();
        
        var receiveCharactersThread = new Thread(() => ReceiveElementChanges((int)Ports.Characters, _syncCharacters));
        var receiveEnvironmentThread = new Thread(() => ReceiveElementChanges((int)Ports.Environment, _syncEnvObjects));
        var receiveNpcThread = new Thread(() => ReceiveElementChanges((int)Ports.Npc, _syncNpc));
        receiveCharactersThread.Start();
        receiveEnvironmentThread.Start();
        receiveNpcThread.Start();
    }

    private readonly List<SyncUser> _connectedUsers = new();
    
    private readonly List<SyncElement> _syncCharacters = new();
    private readonly List<SyncElement> _syncEnvObjects = new();
    private readonly List<SyncElement> _syncNpc = new();

    private void ReceiveElementChanges(int port, ICollection<SyncElement> list)
    {
        var receiver = new UdpClient(port);
        IPEndPoint? remoteIp = null;
        while (true)
        {
            var data = receiver.Receive(ref remoteIp);
            var convertedData = Encoding.Unicode.GetString(data);
            if (string.IsNullOrEmpty(convertedData)) continue;
            
            var parsedMessage = JsonConvert.DeserializeObject<SyncElement>(convertedData);
            var existingElement = list.FirstOrDefault(w => w.Id == parsedMessage.Id);
            if (existingElement != null)
                existingElement.Data = parsedMessage.Data;
            else
                list.Add(parsedMessage);
                
            SendMessage(convertedData, parsedMessage.Id, port + 1); // TODO: change to dynamic
        }
    }

    private void ReceiveUser()
    {
        var receiver = new UdpClient((int)Ports.Users);
        IPEndPoint? remoteIp = null;
        while (true)
        {
            var data = receiver.Receive(ref remoteIp);
            var convertedData = Encoding.Unicode.GetString(data);
            if (string.IsNullOrEmpty(convertedData)) continue;
            
            var userId = Guid.Parse(convertedData);
            var existingElement = _connectedUsers.FirstOrDefault(w => w.Id == userId);
            if (existingElement != null) continue;
            
            var newUser = new SyncUser
            {
                Id = userId,
                Ip = remoteIp.Address.ToString()
            };
            _connectedUsers.Add(newUser);
        }
    }

    private void SendMessage(string convertedData, Guid? userId, int port)
    {
        var sender = new UdpClient();
        var data = Encoding.Unicode.GetBytes(convertedData);
        foreach (var user in _connectedUsers.Where(w => w.Id != userId!.Value))
        {
            sender.Send(data, data.Length, user.Ip, port);
        }
    }
}