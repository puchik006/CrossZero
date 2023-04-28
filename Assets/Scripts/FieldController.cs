using System.Collections.Generic;

public class FieldController
{
    private protected List<FieldView> _fieldsView;
    private protected FieldModel _fieldsModel;

    public FieldController(List<FieldView> fieldsView, FieldModel fieldsModel)
    {
        _fieldsView = fieldsView;
        _fieldsModel = fieldsModel;

        Enable();
    }

    private void Enable()
    {
        _fieldsView.ForEach(e => e.OnFieldTouched += ChangeFieldSign);
        _fieldsModel.OnFieldValueChanged += ChangeView;
        RoundModel.OnRoundEnd += ClearFields;
    }

    private void ChangeFieldSign(int fieldNumber)
    {
        _fieldsModel.ChangeMatrix(fieldNumber);
    }

    private protected void ChangeView(int fieldNumber,FieldValue fieldValue)
    {
        _fieldsView[fieldNumber].ChangeFieldColor(fieldValue);
    }

    private protected void ClearFields(GameStatus gameStatus)
    {
        _fieldsModel.ClearMatrix();
        _fieldsModel.ChangeSignOnNewRound();
        _fieldsView.ForEach(e => e.ClearField());
    }
}
