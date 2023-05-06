using System;

public class TurnModel
{
    private bool _isYourTurn = true;
    private bool _isAnotherPlayerTurn = false;

    private bool _myLastRoundTurn = true;

    public static event Action<int> OnProveYourTurn;
    public static event Action<int> OnProveAnotherPlayerTurn;

    public TurnModel()
    {
        ButtonsHandler.OnTwoPlayersGameStart += SetNoInternetGame;
        ButtonsHandler.OnLocalGameStart += SetInternetGame;

        FieldView.OnButtonClick += ProoveChangesIfYourTurn;
        PlayerNetwork.IsPlayerHost += SetTurn;
        FieldModel.OnFieldValueChanged += ChangeTurnAfterFieldChanges;
        TextMessageView.OnMessageStop += ChangeTurnAfterRoundEnd;
    }

    private void SetInternetGame()
    {  
        PlayerNetwork.OnGetClientFieldInfo += ProoveChangesIfAnotherPlayerTurn;
    }

    private void SetNoInternetGame()
    {
        FieldView.OnButtonClick += ProoveChangesIfAnotherPlayerTurn;
    }

    private void SetTurn(bool isYourTurn)
    {
        _isYourTurn = isYourTurn;
        _isAnotherPlayerTurn = !isYourTurn;

        _myLastRoundTurn = _isYourTurn;
    }

    private void ChangeTurnAfterRoundEnd()
    {
        //_isYourTurn = _isYourTurn == _myLastRoundTurn ? false : true;
        //_isAnotherPlayerTurn = !_isYourTurn;

        //_myLastRoundTurn = _isYourTurn;

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
