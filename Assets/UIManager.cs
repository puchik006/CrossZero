using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _mainMenuScreen;
    [SerializeField] private GameObject _multiplayerScreen;

    private void Start()
    {
        CrossZerroLobby.OnGameStart += SetActiveGameScreen;
    }

    private void SetActiveGameScreen()
    {
        _gameScreen.SetActive(true);
        _mainMenuScreen.SetActive(false);
        _multiplayerScreen.SetActive(false);
    }

    private static string _message = "LOG: ";

    public static void GUIMessage(string message)
    {
        _message = _message + "\n"  + message;
    }

    private void OnGUI()
    {
        _message = GUI.TextArea(new Rect(10, 10, 200, 50), _message, 200);
    }
}
