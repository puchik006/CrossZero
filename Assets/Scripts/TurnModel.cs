using System;

public class TurnModel
{
    private bool _isYourTurn = true;
    private bool _isAnotherPlayerTurn = false;

    public static event Action<int> OnYourTurn;
    public static event Action<int> OnAnotherPlayerTurn;

    public TurnModel()
    {
        PlayerNetwork.IsPlayerHost += SetTurn;
        FieldModel.OnFieldValueChanged += ChangeTurnAfterFieldChanges;
    }

    private void SetTurn(bool isYourTurn)
    {
        _isYourTurn = isYourTurn;
        _isAnotherPlayerTurn = !isYourTurn; 
    }

    private void ChangeTurnAfterFieldChanges(int asd,FieldValue fieldValue)
    {
        _isYourTurn = !_isYourTurn;
        _isAnotherPlayerTurn = !_isAnotherPlayerTurn;
    }

    public void ProoveChangesIfYourTurn(int a)
    {
        if (_isYourTurn)
        {
            OnYourTurn?.Invoke(a);
        }
    }

    public void ProoveChangesIfAnotherPlayerTurn(int a)
    {
        if (_isAnotherPlayerTurn)
        {
            OnAnotherPlayerTurn?.Invoke(a);
        }
    }

}
