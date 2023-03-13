using System.Collections;
using UnityEngine;

public class TextMessageController
{
    private TextMessageView _textMessageView;
    private TextMessageModel _textMessageModel;

    public TextMessageController(TextMessageView textMessageView, TextMessageModel textMessageModel)
    {
        _textMessageView = textMessageView;
        _textMessageModel = textMessageModel;

        Enable();
    }

    private void Enable()
    {
        RoundModel.OnRoundEnd += SetUpMessage;
    }

    private void SetUpMessage(GameStatus gameStatus)
    {
        _textMessageModel.CreateMessage(gameStatus);
        _textMessageView.ShowGameMessage(_textMessageModel.GameMessage);
    }
}
