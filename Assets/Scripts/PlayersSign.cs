public class PlayersSign
{
    public FieldValue MySign;
    public FieldValue AnotherPlayerSign;

    public PlayersSign()
    {
        PlayerNetwork.IsPlayerHost += SetInitialPlayersSign;
        RoundModel.OnRoundEnd += SetPlayersSignAfterRound;
    }

    private void SetInitialPlayersSign(bool isPlayerHost)
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
