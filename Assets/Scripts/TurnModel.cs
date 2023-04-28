public class TurnModel
{
    private bool _isYourTurn = true;
    private bool _isAnotherPlayerTurn = false;

    public bool IsYourTurn { get => _isYourTurn;}
    public bool IsAnotherPlayerTurn { get => _isAnotherPlayerTurn;}


    public TurnModel()
    {
        PlayerNetwork.IsPlayerHost += SetTurn;
    }

    public void SetTurn(bool isYourTurn)
    {
        _isYourTurn = isYourTurn;
        _isAnotherPlayerTurn = !isYourTurn; 
    }
}
