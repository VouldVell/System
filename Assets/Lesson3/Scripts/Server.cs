using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


namespace System_Programming.Lesson3
{
    public class Server
    {
        private const int MAX_CONNECTION = 10;
        private int _port = 5805;
        private int _hostID;
        private int _reliableChannel;
        private bool _isStarted = false;
        private byte _error;
        private readonly List<int> _connectionIDs = new List<int>();
        private readonly Dictionary<int, string> _connectionNames = new Dictionary<int, string>();


        public void StartServer()
        {
            NetworkTransport.Init();
            ConnectionConfig cc = new ConnectionConfig();
            _reliableChannel = cc.AddChannel(QosType.Reliable);
            HostTopology topology = new HostTopology(cc, MAX_CONNECTION);
            _hostID = NetworkTransport.AddHost(topology, _port);
            _isStarted = true;
        }

        public void ShutDownServer()
        {
            if (!_isStarted) return;
            NetworkTransport.RemoveHost(_hostID);
            NetworkTransport.Shutdown();
            _isStarted = false;
        }

        public void Update()
        {
            if (!_isStarted) return;
            int recHostId;
            int connectionId;
            int channelId;
            byte[] recBuffer = new byte[1024];
            int bufferSize = 1024;
            int dataSize;
            NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out
            channelId, recBuffer, bufferSize, out dataSize, out _error);
            while (recData != NetworkEventType.Nothing)
            {
                switch (recData)
                {
                    case NetworkEventType.Nothing:
                        break;
                    case NetworkEventType.ConnectEvent:
                        _connectionNames.TryAdd(connectionId, "");
                        _connectionIDs.Add(connectionId);
                        break;
                    case NetworkEventType.DataEvent:
                        string message = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                        if (_connectionNames.ContainsKey(connectionId) && _connectionNames[connectionId] == "")
                        {
                            _connectionNames[connectionId] = message;
                            if (_connectionNames[connectionId] == "na") _connectionNames[connectionId] = "Player " + connectionId.ToString();
                            SendMessageToAll($"{_connectionNames[connectionId]} has connected.");
                            Debug.Log($"{_connectionNames[connectionId]} has connected.");
                        }
                        else
                        {
                            SendMessageToAll($"{_connectionNames[connectionId]}: {message}");
                            Debug.Log($"{_connectionNames[connectionId]}: {message}");
                        }
                        break;
                    case NetworkEventType.DisconnectEvent:
                        _connectionIDs.Remove(connectionId);
                        SendMessageToAll($"{_connectionNames[connectionId]} has disconnected.");
                        Debug.Log($"{_connectionNames[connectionId]} has disconnected.");
                        _connectionNames.Remove(connectionId);
                        break;
                    case NetworkEventType.BroadcastEvent:
                        break;
                }
                recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer,
                bufferSize, out dataSize, out _error);
            }
        }

        private void SendMessageToAll(string message)
        {
            for (int i = 0; i < _connectionIDs.Count; i++)
            {
                SendMessage(message, _connectionIDs[i]);
            }
        }

        public void SendMessage(string message, int connectionID)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(message);
            NetworkTransport.Send(_hostID, connectionID, _reliableChannel, buffer, message.Length *
            sizeof(char), out _error);
            if ((NetworkError)_error != NetworkError.Ok) Debug.Log((NetworkError)_error);
        }
    }
}