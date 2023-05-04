using System.Collections.Generic;

public class FieldControllerForNetworkGame 
{
    private List<FieldView> _fieldsView;
    private FieldModel _fieldsModel;

    private PlayersSign _playerSign;
    private TurnModel _turnModel;

    public FieldControllerForNetworkGame(List<FieldView> fieldsView, FieldModel fieldsModel) 
    {
        _fieldsView = fieldsView;
        _fieldsModel = fieldsModel;

        _playerSign = new PlayersSign();
        _turnModel = new TurnModel();

        Enable();
    }

    private void Enable()
    {
        //_fieldsView.ForEach(e => e.OnFieldTouched += ChangeFieldSignIN);

        FieldView.OnButtonClick += ChangeFieldSignIN;

        PlayerNetwork.OnGetClientFieldInfo += ChangeFieldSignOUT;
        FieldModel.OnFieldValueChanged += ChangeView;
        
        RoundModel.OnRoundEnd += ClearFields;

        //TurnModel.OnYourTurn += ChangeFieldSign;
        //TurnModel.OnAnotherPlayerTurn += ChangeFieldSign;
        
    }

    private void ChangeFieldSignIN(int fieldNumber)
    {
        //if (_turnModel.IsYourTurn) // can change turn on click on the same field when turn is yours | Need to check field emptys
        //{
        //    ChangeFieldSign(fieldNumber,_playerSign.MySign);
        //    //_turnModel.SetTurn(false);
        //}

        _turnModel.ProoveChangesIfYourTurn(fieldNumber);
    }

    private void ChangeFieldSignOUT(int fieldNumber)
    {
        //if (_turnModel.IsAnotherPlayerTurn) 
        //{
        //    ChangeFieldSign(fieldNumber, _playerSign.AnotherPlayerSign);
        //    //_turnModel.SetTurn(true);
        //}

        _turnModel.ProoveChangesIfAnotherPlayerTurn(fieldNumber);
    }

    private void ChangeFieldSign(int fieldNumber, FieldValue fieldValue)
    {
        _fieldsModel.ChangeMatrix(fieldNumber,fieldValue);
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
