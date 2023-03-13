using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessageModel
{
    void CreateMessage(GameStatus gameStatus);
}

public class TextMessageModel: IMessageModel
{
    private string _gameMessage;

    private Dictionary<GameStatus,string> _gameStatusToString = new Dictionary<GameStatus, string>()
    {
        {GameStatus.Draw,"DRAW!"},
        {GameStatus.CrossWin,"Cross win!"},
        {GameStatus.ZeroWin,"Zero win!"}
    };

    public string GameMessage { get => _gameMessage;}

    public void CreateMessage(GameStatus gameStatus)
    {
        _gameMessage = _gameStatusToString[gameStatus];
    }
}
