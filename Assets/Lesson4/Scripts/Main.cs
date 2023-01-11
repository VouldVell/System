using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace System_Programming.Lesson4
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Button _startClientButton;
        [SerializeField] private Button _startServerButton;
        [SerializeField] private Button _startHostButton;
        [SerializeField] private GameObject _uiHolder;


        private void Start()
        {
            _startClientButton.onClick.AddListener(StartClientButton);
            _startServerButton.onClick.AddListener(StartServerButton);
            _startHostButton.onClick.AddListener(StartHostButton);
            _uiHolder.SetActive(true);
        }

        private void StartClientButton()
        {
            NetworkManager.Singleton.StartClient();

            CloseUI();
        }

        private void StartServerButton()
        {
            NetworkManager.Singleton.StartServer();
            CloseUI();
        }


        private void StartHostButton()
        {
            NetworkManager.Singleton.StartHost();
            CloseUI();
        }

        private void CloseUI()
        {
            _uiHolder.SetActive(false);
        }

        public void OnDestroy()
        {
            _startClientButton.onClick.RemoveAllListeners();
            _startServerButton.onClick.RemoveAllListeners();
            _startHostButton.onClick.RemoveAllListeners();
        }
    }
}