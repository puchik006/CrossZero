public class CellsButtonController
{
    private CellsButtonView _buttonsView;
    private CellsButtonModel _buttonsModel;

    public CellsButtonController(CellsButtonView buttonsView, CellsButtonModel buttonsModel)
    {
        _buttonsView = buttonsView;
        _buttonsModel = buttonsModel;

        ButtonsHandler.OnTwoPlayersGameStart += SetTwoPlayersGame;
    }

    private void SetTwoPlayersGame()
    {
        _buttonsView.OnButtonClick += _buttonsModel.ChangeModel;
    }
}
