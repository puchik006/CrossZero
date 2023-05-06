using System;

public class PlayersSign
{
    private FieldValue _mySign;
    private FieldValue _anotherPlayerSign;

    public FieldValue MySign { get => _mySign;}
    public FieldValue AnotherPlayerSign { get => _anotherPlayerSign;}

    public static Action<FieldValue> OnMySignChanged;
    public static Action<FieldValue> OnAnotherPlayerSignChanged;

    public PlayersSign()
    {
        PlayerNetwork.IsPlayerHost += SetInitialPlayersSignForInternetGame;
        RoundModel.OnRoundEnd += SetPlayersSignAfterRound;

        ButtonsHandler.OnTwoPlayersGameStart += SetInitialPlayerSignForTwoPlayersGame;
    }

    private void SetInitialPlayerSignForTwoPlayersGame()
    {
        _mySign = FieldValue.Cross;
        _anotherPlayerSign = FieldValue.Zero;

        InvokeChanges();
    }

    private void SetInitialPlayersSignForInternetGame(bool isPlayerHost)
    {
        _mySign = isPlayerHost ? FieldValue.Cross : FieldValue.Zero;
        _anotherPlayerSign = isPlayerHost ? FieldValue.Zero : FieldValue.Cross;

        InvokeChanges();
    }

    private void SetPlayersSignAfterRound(GameStatus gameStatus)
    {
        _mySign = _mySign == FieldValue.Cross ? FieldValue.Zero : FieldValue.Cross;
        _anotherPlayerSign = _anotherPlayerSign == FieldValue.Cross ? FieldValue.Zero : FieldValue.Cross;

        InvokeChanges();
    }

    private void InvokeChanges()
    {
        OnMySignChanged?.Invoke(_mySign);
        OnAnotherPlayerSignChanged?.Invoke(_anotherPlayerSign);
    }
}
