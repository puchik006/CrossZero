using System;
using System.Collections.Generic;

public class TurnModel
{
    private bool _isYourTurn = true;
    private bool _isAnotherPlayerTurn = false;

    private List<FieldView> _fieldsView;
    private FieldModel _fieldsModel;

    public static event Action<int> OnApproveToChangeFieldNumer;
    public static event Action<bool> OnTurn;

    public TurnModel(List<FieldView> fieldsView, FieldModel fieldsModel)
    {
        _fieldsModel = fieldsModel;
        _fieldsView = fieldsView;

        _fieldsView.ForEach(e => e.OnFieldTouched += ChangeFieldSignIN);

        PlayerNetwork.IsPlayerHost += SetTurn;
        PlayerNetwork.OnGetClientFieldInfo += ChangeFieldSignOUT;
    }

    private void ChangeFieldSignIN(int fieldNumber)
    {
        if (_fieldsModel.Data[fieldNumber] != FieldValue.Empty) return;

        if (_isYourTurn)
        {
            OnApproveToChangeFieldNumer?.Invoke(fieldNumber);
            SetTurn(false);
        }
    }

    private void ChangeFieldSignOUT(int fieldNumber)
    {
        if (_fieldsModel.Data[fieldNumber] != FieldValue.Empty) return;

        if (_isAnotherPlayerTurn) 
        {
            OnApproveToChangeFieldNumer?.Invoke(fieldNumber);
            SetTurn(true);
        }
    }

    public void SetTurn(bool isYourTurn)
    {
        _isYourTurn = isYourTurn;
        _isAnotherPlayerTurn = !isYourTurn;

        OnTurn?.Invoke(_isYourTurn);
    }
}
