public class TurnModel
{
    private bool _isYourTurn = true;
    private bool _isAnotherPlayerTurn = false;

    public TurnModel()
    {
        PlayerNetwork.IsYourTurnFirst += SetTurn;
    }

    public void SetTurn(bool isYourTurn)
    {
        _isYourTurn = isYourTurn;
        _isAnotherPlayerTurn = !isYourTurn;
        UIManager.GUIMessageForUpdate("Yout turn: " + _isYourTurn.ToString() + "\n Second player turn: " + _isAnotherPlayerTurn);
    }

    public bool GetYourTurn()
    {
        return _isYourTurn;
    }

    public bool GetSecondPlayerTurn()
    {
        return _isAnotherPlayerTurn;
    }
}
