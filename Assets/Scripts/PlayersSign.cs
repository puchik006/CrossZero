using System;

public class PlayersSign
{
    private CellValue _mySign;
    private CellValue _anotherPlayerSign;

    public CellValue MySign { get => _mySign;}
    public CellValue AnotherPlayerSign { get => _anotherPlayerSign;}

    public static Action<CellValue> OnMySignChanged;
    public static Action<CellValue> OnAnotherPlayerSignChanged;

    public static Action<CellValue> OnActualSignChanged;

    private bool _isPlayerHost = true;

    public PlayersSign()
    {
        PlayerNetwork.IsPlayerHost += SetInitialPlayersSignForInternetGame; //?? maybe buttons will turn it
        ButtonsHandler.OnTwoPlayersGameStart += SetInitialPlayerSignForTwoPlayersGame;
        
        RoundModel.OnRoundEnd += SetPlayersSignAfterRound;
    }

    private void SetInitialPlayerSignForTwoPlayersGame()
    {
        _mySign = CellValue.Cross;
        _anotherPlayerSign = CellValue.Zero;

        InvokeChanges();
    }

    private void SetInitialPlayersSignForInternetGame(bool isPlayerHost)
    {
        _isPlayerHost = isPlayerHost;

        _mySign = isPlayerHost ? CellValue.Cross : CellValue.Zero;
        _anotherPlayerSign = isPlayerHost ? CellValue.Zero : CellValue.Cross;

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
        _mySign = _mySign == CellValue.Cross ? CellValue.Zero : CellValue.Cross;
        _anotherPlayerSign = _anotherPlayerSign == CellValue.Cross ? CellValue.Zero : CellValue.Cross;

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
