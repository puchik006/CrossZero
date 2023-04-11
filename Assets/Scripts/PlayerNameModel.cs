using System;
using UnityEngine;

public class PlayerNameModel
{
    private string _playerOneName;
    private string _playerTwoName;

    public event Action<string> PlayerOneNameChanged;
    public event Action<string> PlayerTwoNameChanged;

    public string PlayerOneName { get => _playerOneName;}
    public string PlayerTwoName { get => _playerTwoName;}

    public void SetPlayerName(bool isPlayerHost)
    {
        if (isPlayerHost)
        {
            _playerOneName = PlayerPrefs.HasKey(PlayerKeys.Name) ? PlayerPrefs.GetString(PlayerKeys.Name) : "Player 1";
            PlayerOneNameChanged?.Invoke(PlayerOneName);
        }
        else
        {
            _playerTwoName = PlayerPrefs.HasKey(PlayerKeys.Name) ? PlayerPrefs.GetString(PlayerKeys.Name) : "Player 2";
            PlayerTwoNameChanged?.Invoke(PlayerTwoName);
        }
    }

    public void SetSecondPlayerName(string firstPlayerName, string secondPlayerName)
    {
        if (_playerOneName != null)
        {
            _playerTwoName = secondPlayerName;
            PlayerTwoNameChanged?.Invoke(PlayerTwoName);
        }

        if (_playerTwoName != null)
        {
            _playerOneName = firstPlayerName;
            PlayerOneNameChanged?.Invoke(PlayerOneName);
        }

        //if (_playerOneName == null)
        //{
        //    _playerOneName = secondPlayerName;
        //    PlayerOneNameChanged?.Invoke(PlayerOneName);
        //}

        //if (_playerTwoName == null)
        //{
        //    _playerTwoName = secondPlayerName;
        //    PlayerTwoNameChanged?.Invoke(PlayerTwoName);
        //}
    }
}
