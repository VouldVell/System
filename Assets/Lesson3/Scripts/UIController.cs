using System;


namespace System_Programming.Lesson3
{
    public class UIController : IDisposable
    {
        private UIView _view;
        private Server _server;
        private Client _client;

        public UIController(UIView view, Server server, Client client)
        {
            _view = view;
            _server = server;
            _client = client;

            _view.StartButton.onClick.AddListener(StartServer);
            _view.ShutDownButton.onClick.AddListener(ShutDownServer);
            _view.ConnectButton.onClick.AddListener(OnConnectButton);
            _view.DisconnectButton.onClick.AddListener(Disconnect);
            _view.SendButton.onClick.AddListener(SendMessage);

            _client.OnMessageReceive += ReceiveMessage;

            _view.InputNamePanel.SetActive(false);
        }

        private void StartServer()
        {
            _server.StartServer();
        }

        private void ShutDownServer()
        {
            _server.ShutDownServer();
        }

        private void Connect()
        {
            _client.Connect();
        }

        private void Disconnect()
        {
            _client.Disconnect();
        }

        private void SendMessage()
        {
            _client.SendMessage(_view.InputText.text);
            _view.InputText.text = "";
        }

        public void ReceiveMessage(object message)
        {
            _view.OutputText.text = _view.OutputText.text + "\n" + message.ToString();
        }

        private void OnConnectButton()
        {
            Connect();
            _view.InputNamePanel.SetActive(true);
            _view.OkButton.onClick.AddListener(OnOkButton);
        }

        private void OnOkButton()
        {
            if (_view.Name.text == "") _view.Name.text = "na";
            _client.SendMessage(_view.Name.text);
            _view.OkButton.onClick.RemoveAllListeners();
            _view.InputNamePanel.SetActive(false);
        }

        public void Dispose()
        {
            _view.StartButton.onClick.RemoveAllListeners();
            _view.ShutDownButton.onClick.RemoveAllListeners();
            _view.ConnectButton.onClick.RemoveAllListeners();
            _view.DisconnectButton.onClick.RemoveAllListeners();
            _view.SendButton.onClick.RemoveAllListeners();

            _client.OnMessageReceive -= ReceiveMessage;
        }
    }
}