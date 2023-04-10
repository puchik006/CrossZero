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

    }
}
