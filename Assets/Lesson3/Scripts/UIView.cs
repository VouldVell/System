using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace System_Programming.Lesson3
{
    public class UIView : MonoBehaviour
    {
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button ShutDownButton { get; private set; }
        [field: SerializeField] public Button ConnectButton { get; private set; }
        [field: SerializeField] public Button DisconnectButton { get; private set; }
        [field: SerializeField] public TMP_Text OutputText { get; private set; }
        [field: SerializeField] public TMP_InputField InputText { get; private set; }
        [field: SerializeField] public Button SendButton { get; private set; }
        [field: SerializeField] public GameObject InputNamePanel { get; private set; }
        [field: SerializeField] public TMP_InputField Name { get; private set; }
        [field: SerializeField] public Button OkButton { get; private set; }
    }
}