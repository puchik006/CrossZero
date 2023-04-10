using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FieldModel _fieldsModel;
    [SerializeField] private List<FieldView> _fieldsView;
    private FieldController _fieldController;

    private RoundModel _roundModel;

    private TurnModel _turnModel;

    private IScoreModel _scoreModel;
    [SerializeField] private ScoreView _scoreView;
    private ScoreController _scoreController;   

    private PlayerNameModel _playerNameModel;
    private PlayerNameController _playerNameController;
    [SerializeField] private PlayerNameView _playerNameView;

    private TextMessageModel _textMessageModel;
    [SerializeField] private TextMessageView _textMessageView;
    private TextMessageController _textMessageController;
    
    
    void Start()
    {
        _fieldsModel = new FieldModel();
        _turnModel = new TurnModel(_fieldsView, _fieldsModel);
        _fieldController = new FieldController(_fieldsView, _fieldsModel);

        _roundModel = new RoundModel(_fieldsModel);

        _playerNameModel = new PlayerNameModel();
        _playerNameController = new PlayerNameController(_playerNameModel, _playerNameView);

        _scoreModel = new ScoreModel();
        _scoreController = new ScoreController(_scoreView,_scoreModel);

        _textMessageModel = new TextMessageModel();
        _textMessageController = new TextMessageController(_textMessageView, _textMessageModel);
    }
}
