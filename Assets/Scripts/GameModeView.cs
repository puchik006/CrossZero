using System;
using UnityEngine;
using UnityEngine.UI;

public class GameModeView: MonoBehaviour
{
    [SerializeField] private Button _InternetGame;
    [SerializeField] private Button _TwoPlayersGame;
    [SerializeField] private Button _AIGame;
    [SerializeField] private Button _LocalGame;

    public static event Action OnLocalGameStart;

    private void Start()
    {
        _LocalGame.onClick.AddListener(LocalGame);
    }

    private void LocalGame()
    {
        OnLocalGameStart?.Invoke();
    }
}
