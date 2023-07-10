public class CellsButtonController
{
    private CellsButtonView _buttonsView;
    private CellsButtonModel _buttonsModel;

    public CellsButtonController(CellsButtonView buttonsView, CellsButtonModel buttonsModel)
    {
        _buttonsView = buttonsView;
        _buttonsModel = buttonsModel;

        ButtonsHandler.OnTwoPlayersGameStart += SetTwoPlayersGame;

        CellsButtonModel.OnCellsModelChanged += _buttonsView.SetCellImage;
        RoundResults.OnRoundEnd += SetNewRound;
    }

    private void SetTwoPlayersGame()
    {
        _buttonsView.ButtonClicked += _buttonsModel.ChangeModel;
    }

    private void SetNewRound(GameStatus gameStatus)
    {
        _buttonsModel.ClearMatrix();
    }
}
