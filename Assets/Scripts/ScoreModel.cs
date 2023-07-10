using System;

public class ScoreModel
{
    private int _playerOneScore;
    private int _playerTwoScore;
    private bool _isFirstPlayerPlayedByCross = true;
    public event Action<string,string> ScoreChanged;

    public int PlayerOneScore { get => _playerOneScore; }
    public int PlayerTwoScore { get => _playerTwoScore; }

    //TODO: refactor this!!!
    public void ChangePlayerScore(GameStatus gameStatus)
    {
        if (_isFirstPlayerPlayedByCross && gameStatus == GameStatus.CrossWin)
        {
            _playerOneScore++;
        }

        if (!_isFirstPlayerPlayedByCross && gameStatus == GameStatus.ZeroWin)
        {
            _playerOneScore++;
        }

        if (_isFirstPlayerPlayedByCross && gameStatus == GameStatus.ZeroWin)
        {
            _playerTwoScore++;
        }

        if (!_isFirstPlayerPlayedByCross && gameStatus == GameStatus.CrossWin)
        {
            _playerTwoScore++;
        }

        _isFirstPlayerPlayedByCross = !_isFirstPlayerPlayedByCross;
        ScoreChanged?.Invoke(PlayerOneScore.ToString(), PlayerTwoScore.ToString());
    }

    public void ClearPlayersScore()
    {
        _playerOneScore = 0;
        _playerTwoScore = 0;
    }
}


