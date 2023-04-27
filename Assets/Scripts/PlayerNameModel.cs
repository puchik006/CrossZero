using System;
using UnityEngine;

public class PlayerNameModel
{
    private string _myName;
    private string _playerOneName;
    private string _playerTwoName;

    public event Action<string> PlayerOneNameChanged;
    public event Action<string> PlayerTwoNameChanged;
    public static event Action<string> SayMyName;

    public string PlayerOneName { get => _playerOneName;}
    public string PlayerTwoName { get => _playerTwoName;}

    public PlayerNameModel()
    {
        _myName = PlayerPrefs.HasKey(PlayerKeys.Name) ? PlayerPrefs.GetString(PlayerKeys.Name) : "";
    }

    public void SetPlayerName(bool isPlayerHost)
    {
        if (isPlayerHost)
        {
            _playerOneName = _myName == "" ? "Player 1" : _myName;
            PlayerOneNameChanged?.Invoke(PlayerOneName);
        }
        else
        {
            _playerTwoName = _myName == "" ? "Player 2" : _myName;
            PlayerTwoNameChanged?.Invoke(PlayerTwoName);
        }
    }

    public void SetSecondPlayerName(string secondPlayerName)
    {
        if (_playerOneName == _myName)
        {
            _playerTwoName = secondPlayerName;
            PlayerTwoNameChanged?.Invoke(PlayerTwoName);
        }

        if (_playerTwoName == _myName)
        {
            _playerOneName = secondPlayerName;
            PlayerOneNameChanged?.Invoke(PlayerOneName);
        }
    }

    public void SendNameToAnotherPlayer(ulong id)
    {
        SayMyName?.Invoke(_myName);
    }
}
