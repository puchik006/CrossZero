using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel
{
    private int _playerOneScore;
    private int _playerTwoScore;
    private bool _isFirstPlayerPlayedByCross = true;
    public event Action<string,string> OnScoreChanged;

    public int PlayerOneScore { get => _playerOneScore; }
    public int PlayerTwoScore { get => _playerTwoScore; }

    //TODO: refactor this!!!
    public void ChangePlayerScore(GameStatus gameStatus)
    {
        if (gameStatus == GameStatus.Draw)
        {
            _isFirstPlayerPlayedByCross = !_isFirstPlayerPlayedByCross;
            Debug.Log("Player One: " + _playerOneScore + " PlayerTwo: " + _playerTwoScore);
            OnScoreChanged?.Invoke(PlayerOneScore.ToString(),PlayerTwoScore.ToString());
            return;
        }

        if (_isFirstPlayerPlayedByCross && gameStatus == GameStatus.CrossWin)
        {
            _playerOneScore++;
            _isFirstPlayerPlayedByCross = !_isFirstPlayerPlayedByCross;
            Debug.Log("Player One: " + _playerOneScore + " PlayerTwo: " + _playerTwoScore);
            OnScoreChanged?.Invoke(PlayerOneScore.ToString(), PlayerTwoScore.ToString());
            return;
        }

        if (!_isFirstPlayerPlayedByCross && gameStatus == GameStatus.ZeroWin)
        {
            _playerOneScore++;
            _isFirstPlayerPlayedByCross = !_isFirstPlayerPlayedByCross;
            Debug.Log("Player One: " + _playerOneScore + " PlayerTwo: " + _playerTwoScore);
            OnScoreChanged?.Invoke(PlayerOneScore.ToString(), PlayerTwoScore.ToString());
            return;
        }

        if (_isFirstPlayerPlayedByCross && gameStatus == GameStatus.ZeroWin)
        {
            _playerTwoScore++;
            _isFirstPlayerPlayedByCross = !_isFirstPlayerPlayedByCross;
            Debug.Log("Player One: " + _playerOneScore + " PlayerTwo: " + _playerTwoScore);
            OnScoreChanged?.Invoke(PlayerOneScore.ToString(), PlayerTwoScore.ToString());
            return;
        }

        if (!_isFirstPlayerPlayedByCross && gameStatus == GameStatus.CrossWin)
        {
            _playerTwoScore++;
            _isFirstPlayerPlayedByCross = !_isFirstPlayerPlayedByCross;
            Debug.Log("Player One: " + _playerOneScore + " PlayerTwo: " + _playerTwoScore);
            OnScoreChanged?.Invoke(PlayerOneScore.ToString(), PlayerTwoScore.ToString());
            return;
        }
    }

    public void ClearPlayersScore()
    {

    }
}
