public class PlayerNameController
{
    private PlayerNameModel _playerNameModel;
    private PlayerNameView _playerNameView;

    public PlayerNameController(PlayerNameModel playerNameModel,PlayerNameView playerNameView)
    {
        _playerNameModel = playerNameModel;
        _playerNameView = playerNameView;

        Enable();
    }

    private void Enable()
    {
        PlayerNetwork.IsPlayerHost += _playerNameModel.SetPlayerName;
        PlayerNetwork.OnPlayerConnected += _playerNameModel.SetSecondPlayerName;

        _playerNameModel.PlayerOneNameChanged += _playerNameView.SetPlayerOneName;
        _playerNameModel.PlayerTwoNameChanged += _playerNameView.SetPlayerTwoName;
    }
}
