using System;

public class PlayersSign
{
    private FieldValue _mySign;
    private FieldValue _anotherPlayerSign;

    public FieldValue MySign { get => _mySign;}
    public FieldValue AnotherPlayerSign { get => _anotherPlayerSign;}

    public static Action<FieldValue> OnMySignChanged;
    public static Action<FieldValue> OnAnotherPlayerSignChanged;

    public static Action<FieldValue> OnActualSignChanged;

    private bool _isPlayerHost = true;

    public PlayersSign()
    {
        PlayerNetwork.IsPlayerHost += SetInitialPlayersSignForInternetGame; //?? maybe buttons will turn it
        ButtonsHandler.OnTwoPlayersGameStart += SetInitialPlayerSignForTwoPlayersGame;
        
        RoundModel.OnRoundEnd += SetPlayersSignAfterRound;
    }

    private void SetInitialPlayerSignForTwoPlayersGame()
    {
        _mySign = FieldValue.Cross;
        _anotherPlayerSign = FieldValue.Zero;

        InvokeChanges();
    }

    private void SetInitialPlayersSignForInternetGame(bool isPlayerHost)
    {
        _isPlayerHost = isPlayerHost;

        _mySign = isPlayerHost ? FieldValue.Cross : FieldValue.Zero;
        _anotherPlayerSign = isPlayerHost ? FieldValue.Zero : FieldValue.Cross;

        if (isPlayerHost)
        {
            InvokeChanges();
        }
        else
        {
            InvokeAlternativeChanges();
        }
    }

    private void SetPlayersSignAfterRound(GameStatus gameStatus) 
    {
        _mySign = _mySign == FieldValue.Cross ? FieldValue.Zero : FieldValue.Cross;
        _anotherPlayerSign = _anotherPlayerSign == FieldValue.Cross ? FieldValue.Zero : FieldValue.Cross;

        if (_isPlayerHost)
        {
            InvokeChanges();
        }
        else
        {
            InvokeAlternativeChanges();
        }
    }

    private void InvokeChanges()
    {
        OnMySignChanged?.Invoke(_mySign);
        OnAnotherPlayerSignChanged?.Invoke(_anotherPlayerSign);

        OnActualSignChanged?.Invoke(_mySign);
    }

    private void InvokeAlternativeChanges()
    {
        OnMySignChanged?.Invoke(_anotherPlayerSign);
        OnAnotherPlayerSignChanged?.Invoke(_mySign);

        OnActualSignChanged?.Invoke(_mySign);
    }
}
