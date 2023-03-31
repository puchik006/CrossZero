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
}
