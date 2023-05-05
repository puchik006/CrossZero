public class PlayersSign
{
    public FieldValue MySign;
    public FieldValue AnotherPlayerSign;

    public PlayersSign()
    {
        PlayerNetwork.IsPlayerHost += SetInitialPlayersSignForInternetGame;
        RoundModel.OnRoundEnd += SetPlayersSignAfterRound;

        ButtonsHandler.OnTwoPlayersGameStart += SetInitialPlayerSignForTwoPlayersGame;
    }

    private void SetInitialPlayerSignForTwoPlayersGame()
    {
        MySign = FieldValue.Cross;
        AnotherPlayerSign = FieldValue.Zero;
    }

    private void SetInitialPlayersSignForInternetGame(bool isPlayerHost)
    {
        MySign = isPlayerHost ? FieldValue.Cross : FieldValue.Zero;
        AnotherPlayerSign = isPlayerHost ? FieldValue.Zero : FieldValue.Cross;
    }

    private void SetPlayersSignAfterRound(GameStatus gameStatus)
    {
        MySign = MySign == FieldValue.Cross ? FieldValue.Zero : FieldValue.Cross;
        AnotherPlayerSign = AnotherPlayerSign == FieldValue.Cross ? FieldValue.Zero : FieldValue.Cross;
    }
}
