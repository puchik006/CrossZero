using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeModel
{
    private GameMode _gameMode;
    public GameMode GameMode { get => _gameMode;}

    public static event Action<GameMode> OnGameModeChanged;

    public void ChangeGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;
        OnGameModeChanged?.Invoke(GameMode);
    }
}

public enum GameMode
{
    TwoPlayersGame,
    GameWithInternet,
    GamiWithAI,
    LocalNetworkGame
}
