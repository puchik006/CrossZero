using Unity.Netcode;
using UnityEngine;

public class ScreensHandler : MonoBehaviour //rename
{
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _mainMenuScreen;
    [SerializeField] private GameObject _multiplayerScreen;

    private void Start()
    {
        CrossZerroLobby.OnGameStart += SetActiveGameScreen;
        ButtonsHandler.OnLocalGameStart += SetActiveHostClientScreen;
        ButtonsHandler.OnTwoPlayersGameStart += SetActiveGameScreen;
        ButtonsHandler.OnExitButtonClick += SetMainScreenActive;

        NetworkManager.Singleton.OnServerStarted += SetActiveGameScreen;
        NetworkManager.Singleton.OnClientConnectedCallback += SetActiveGameScreen;
    }

    private void SetActiveGameScreen()
    {
        //_gameScreen.SetActive(true);
        _mainMenuScreen.SetActive(false);
        _multiplayerScreen.SetActive(false);
    }

    private void SetActiveGameScreen(ulong aaa)
    {
        //_gameScreen.SetActive(true);
        _mainMenuScreen.SetActive(false);
        _multiplayerScreen.SetActive(false);
    }

    private void SetActiveHostClientScreen()
    {
        _multiplayerScreen.SetActive(true);
        _mainMenuScreen.SetActive(false);
    }

    private void SetMainScreenActive()
    {
        _mainMenuScreen.SetActive(true);
        _gameScreen.SetActive(false);
    }
    private static string _message = "LOG: ";

    public static void GUIMessage(string message)
    {
        _message = _message + "\n"  + message;
    }

    public static void GUIMessageForUpdate(string message)
    {
        _message = message;
    }

    private void OnGUI()
    {
        _message = GUI.TextArea(new Rect(10, 10, 300, 50), _message, 500);

        //if (GUI.Button(new Rect(10, 70, 50, 30), "Test"))
        //{
        //    Show();
        //}
    }
}
