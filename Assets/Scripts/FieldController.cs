using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController
{
    private List<FieldView> _fieldsView;
    private FieldModel _fieldsModel;

    public FieldController(List<FieldView> fieldsView, FieldModel fieldsModel)
    {
        _fieldsView = fieldsView;
        _fieldsModel = fieldsModel;

        Enable();
    }

    private void Enable()
    {
        _fieldsModel.OnFieldValueChanged += ChangeView;

        _fieldsView.ForEach(e => e.OnFieldTouched += ChangeFieldSign);
        PlayerNetwork.OnGetClientFieldInfo += ChangeFieldSign;

       RoundModel.OnRoundEnd += ClearFields;
    }

    private void ChangeFieldSign(int fieldNumber)
    {
        _fieldsModel.ChangeMatrix(fieldNumber);
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
