using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController
{
    private List<FieldView> _fieldsView;
    private FieldModel _fieldsModel;
    private TurnModel _turnModel;

    public FieldController(List<FieldView> fieldsView, FieldModel fieldsModel, TurnModel turnModel)
    {
        _fieldsView = fieldsView;
        _fieldsModel = fieldsModel;
        _turnModel = turnModel;

        Enable();
    }

    private void Enable()
    {
        _fieldsModel.OnFieldValueChanged += ChangeView;

        _fieldsView.ForEach(e => e.OnFieldTouched += ChangeFieldSignIN);

        PlayerNetwork.OnGetClientFieldInfo += ChangeFieldSignOUT;

        RoundModel.OnRoundEnd += ClearFields;
    }

    private void ChangeFieldSignIN(int fieldNumber)
    {
        if (_turnModel.GetYourTurn()) // move it somehow from controller
        {
            _fieldsModel.ChangeMatrix(fieldNumber);
            _turnModel.SetTurn(false);
        }
    }

    private void ChangeFieldSignOUT(int fieldNumber)
    {
        if (_turnModel.GetSecondPlayerTurn()) // move it somehow from controller
        {
            _fieldsModel.ChangeMatrix(fieldNumber);
            _turnModel.SetTurn(true);
        }
    }

    private void ChangeView(int fieldNumber,FieldValue fieldValue)
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
