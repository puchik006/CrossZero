using System;
using UnityEngine.UIElements;

public class TurnModel
{
    private bool _isYourTurn = true; //??
    private bool _isAnotherPlayerTurn = false; //??

    public static event Action<int> OnProveYourTurn;
    public static event Action<int> OnProveAnotherPlayerTurn;

    public TurnModel()
    {
        ButtonsHandler.OnTwoPlayersGameStart += SetNoInternetGame;
        ButtonsHandler.OnLocalGameStart += SetInternetGame;
        ButtonsHandler.OnInternetGameStart += SetInternetGame;

        PlayerNetwork.IsPlayerHost += SetTurn;

        FieldView.OnButtonClick += ProoveChangesIfYourTurn; 
        
        FieldModel.OnFieldValueChanged += ChangeTurnAfterFieldChanges;

        PlayersSign.OnActualSignChanged += ChangeTurnAfterRoundEnd;
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
    }

    private void ChangeTurnAfterRoundEnd(CellValue fieldValue) 
    {
        if (fieldValue == CellValue.Cross)
        {
            _isYourTurn = true;
            _isAnotherPlayerTurn = false;
        }
        else if (fieldValue == CellValue.Zero)
        {
            _isYourTurn = false;
            _isAnotherPlayerTurn = true;
        }
    }

    private void ChangeTurnAfterFieldChanges(int asd,CellValue fieldValue)
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
