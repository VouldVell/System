using System;
using UnityEngine;


namespace System_Programming.Lesson3
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private UIView _uiView;
        private IDisposable _uiController;
        private Server _server;
        private Client _client;


        private void Start()
        {
            _server = new Server();
            _client = new Client();
            _uiController = new UIController(_uiView, _server, _client);
        }

        private void Update()
        {
            _server.Update();
            _client.Update();
        }

        private void OnDestroy()
        {
            _uiController.Dispose();
        }
    }
}