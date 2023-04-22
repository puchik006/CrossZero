using System;
using System.Collections.Generic;

public class FieldControllerForNetworkGame //it is actually Field Controller with turns mechanic for network game
{
    private List<FieldView> _fieldsView;
    private FieldModel _fieldsModel;

    private bool _isYourTurn = true;
    private bool _isAnotherPlayerTurn = false;

    public FieldControllerForNetworkGame(List<FieldView> fieldsView, FieldModel fieldsModel)
    {
        _fieldsModel = fieldsModel;
        _fieldsView = fieldsView;

        Enable();
    }

    private void Enable()
    {
        _fieldsView.ForEach(e => e.OnFieldTouched += ChangeFieldSignIN);
        PlayerNetwork.OnGetClientFieldInfo += ChangeFieldSignOUT;
        _fieldsModel.OnFieldValueChanged += ChangeView;

        PlayerNetwork.IsPlayerHost += SetTurn;
        
        RoundModel.OnRoundEnd += ClearFields;
    }

    private void ChangeFieldSignIN(int fieldNumber)
    {
        if (_fieldsModel.Data[fieldNumber] != FieldValue.Empty) return; //??

        if (_isYourTurn)
        {
            ChangeFieldSign(fieldNumber);
            SetTurn(false);
        }
    }

    private void ChangeFieldSignOUT(int fieldNumber)
    {
        if (_fieldsModel.Data[fieldNumber] != FieldValue.Empty) return; //??

        if (_isAnotherPlayerTurn) 
        {
            ChangeFieldSign(fieldNumber);
            SetTurn(true);
        }
    }

    public void SetTurn(bool isYourTurn)
    {
        _isYourTurn = isYourTurn;
        _isAnotherPlayerTurn = !isYourTurn;
    }

    private void ChangeFieldSign(int fieldNumber)
    {
        _fieldsModel.ChangeMatrix(fieldNumber);
    }

    private void ChangeView(int fieldNumber, FieldValue fieldValue)
    {
        _fieldsView[fieldNumber].ChangeFieldColor(fieldValue);
    }

    private void ClearFields(GameStatus gameStatus)
    {
        _fieldsModel.ClearMatrix();
        _fieldsModel.ChangeSignOnNewRound();
        _fieldsView.ForEach(e => e.ClearField());
    }
}

public class TurnModel
{
    private bool _isYourTurn = true;
    private bool _isAnotherPlayerTurn = false;

    public static event Action<bool> OnTurnChanged;

    public void SetTurn(bool isYourTurn)
    {
        _isYourTurn = isYourTurn;
        _isAnotherPlayerTurn = !isYourTurn; 

        OnTurnChanged?.Invoke(_isYourTurn);
    }

    public TurnModel()
    {
        PlayerNetwork.IsPlayerHost += SetTurn;

        OnTurnChanged?.Invoke(_isYourTurn);
    }
}
