using System;

public class TurnModel
{
    private bool _isYourTurn = true;
    private bool _isAnotherPlayerTurn = false;

    public static event Action<int> OnProveYourTurn;
    public static event Action<int> OnProveAnotherPlayerTurn;

    public TurnModel()
    {
        PlayerNetwork.IsPlayerHost += SetTurn;
        FieldModel.OnFieldValueChanged += ChangeTurnAfterFieldChanges;

        RoundModel.OnRoundEnd += ChangeTurnAfterRoundEnd;

        ButtonsHandler.OnTwoPlayersGameStart += SetNoInternetGame;
        ButtonsHandler.OnLocalGameStart += SetInternetGame;
    }

    private void SetInternetGame()
    {
        FieldView.OnButtonClick += ProoveChangesIfYourTurn;
        PlayerNetwork.OnGetClientFieldInfo += ProoveChangesIfAnotherPlayerTurn;
    }

    private void SetNoInternetGame()
    {
        FieldView.OnButtonClick += ProoveChangesIfYourTurn;
        FieldView.OnButtonClick += ProoveChangesIfAnotherPlayerTurn;
    }

    private void SetTurn(bool isYourTurn)
    {
        _isYourTurn = isYourTurn;
        _isAnotherPlayerTurn = !isYourTurn; 
    }

    private void ChangeTurnAfterRoundEnd(GameStatus gameStatus)
    {
        _isYourTurn = !_isYourTurn;
        _isAnotherPlayerTurn = !_isAnotherPlayerTurn;
    }

    private void ChangeTurnAfterFieldChanges(int asd,FieldValue fieldValue)
    {
        _isYourTurn = !_isYourTurn;
        _isAnotherPlayerTurn = !_isAnotherPlayerTurn;
    }

    public void ProoveChangesIfYourTurn(int fieldNumber)
    {
        if (_isYourTurn)
        {
            OnProveYourTurn?.Invoke(fieldNumber);
        }
    }

    public void ProoveChangesIfAnotherPlayerTurn(int fieldNumber)
    {
        if (_isAnotherPlayerTurn)
        {
            OnProveAnotherPlayerTurn?.Invoke(fieldNumber);
        }
    }
}
