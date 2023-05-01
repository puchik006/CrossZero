using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsHandler: MonoBehaviour
{
    #region MainScreen Buttons and Events
    [SerializeField] private Button _internetGame;
    [SerializeField] private Button _twoPlayersGame;
    [SerializeField] private Button _aiGame;
    [SerializeField] private Button _localGame;

    public static event Action OnLocalGameStart;
    public static event Action OnInternetGameStart;
    public static event Action OnAIGameStart;
    public static event Action OnTwoPlayersGameStart;
    #endregion

    [SerializeField] private Button _exitButton;

    public static event Action OnExitButtonClick;

    private void Start()
    {
        Enable();

        _localGame.onClick.AddListener(() => OnLocalGameStart?.Invoke());
        _internetGame.onClick.AddListener(() => OnInternetGameStart?.Invoke());
        _aiGame.onClick.AddListener(() => OnAIGameStart?.Invoke());
        _twoPlayersGame.onClick.AddListener(() => OnTwoPlayersGameStart?.Invoke());

        _exitButton.onClick.AddListener(() => OnExitButtonClick?.Invoke());
    }

    private void Enable()
    {
        OnLocalGameStart += () => _exitButton.gameObject.SetActive(true);
        OnTwoPlayersGameStart += () => _exitButton.gameObject.SetActive(true);
    }


}
