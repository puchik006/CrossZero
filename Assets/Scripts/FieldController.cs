using System.Collections.Generic;

public class FieldController 
{
    private List<FieldView> _fieldsView;
    private FieldModel _fieldsModel;

    private PlayersSign _playerSign;
    private TurnModel _turnModel;

    public FieldController(List<FieldView> fieldsView, FieldModel fieldsModel) 
    {
        _fieldsView = fieldsView;
        _fieldsModel = fieldsModel;

        _playerSign = new PlayersSign();
        _turnModel = new TurnModel();

        Enable();
    }

    private void Enable()
    {
        TurnModel.OnProveYourTurn += ChangeFieldSignIN;
        TurnModel.OnProveAnotherPlayerTurn += ChangeFieldSignOUT;
        FieldModel.OnFieldValueChanged += ChangeView;  
        RoundModel.OnRoundEnd += ClearFields;
    }

    private void ChangeFieldSignIN(int fieldNumber)
    {
        _fieldsModel.ChangeMatrix(fieldNumber,_playerSign.MySign);
    }

    private void ChangeFieldSignOUT(int fieldNumber)
    {
        _fieldsModel.ChangeMatrix(fieldNumber, _playerSign.AnotherPlayerSign);
    }

    private void ChangeView(int fieldNumber, FieldValue fieldValue)
    {
        _fieldsView[fieldNumber].ChangeFieldColor(fieldValue);
    }

    private void ClearFields(GameStatus gameStatus)
    {
        _fieldsModel.ClearMatrix();
        _fieldsView.ForEach(e => e.ClearField());
    }
}
