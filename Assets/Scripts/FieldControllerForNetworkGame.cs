using System.Collections.Generic;

public class FieldControllerForNetworkGame : FieldController
{
    private PlayersSign _playerSign;
    private TurnModel _turnModel;

    public FieldControllerForNetworkGame(List<FieldView> fieldsView, FieldModel fieldsModel) : base(fieldsView,fieldsModel)
    {
        _playerSign = new PlayersSign();
        _turnModel = new TurnModel();

        Enable();
    }

    private void Enable()
    {
        _fieldsView.ForEach(e => e.OnFieldTouched += ChangeFieldSignIN);
        PlayerNetwork.OnGetClientFieldInfo += ChangeFieldSignOUT;
        _fieldsModel.OnFieldValueChanged += ChangeView;
        
        RoundModel.OnRoundEnd += ClearFields;
    }

    private void ChangeFieldSignIN(int fieldNumber)
    {
        if (_turnModel.IsYourTurn) // can change turn on click on the same field when turn is yours
        {
            ChangeFieldSign(fieldNumber,_playerSign.MySign);
            _turnModel.SetTurn(false);
        }
    }

    private void ChangeFieldSignOUT(int fieldNumber)
    {
        if (_turnModel.IsAnotherPlayerTurn) 
        {
            ChangeFieldSign(fieldNumber, _playerSign.AnotherPlayerSign);
            _turnModel.SetTurn(true);
        }
    }

    private void ChangeFieldSign(int fieldNumber, FieldValue fieldValue)
    {
        _fieldsModel.ChangeMatrix(fieldNumber,fieldValue);
    }
}
