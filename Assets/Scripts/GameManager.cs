using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FieldModel _fieldsModel;
    [SerializeField] private List<FieldView> _fieldsView;
    private FieldController _fieldController;

    private RoundModel _roundModel;

    private IScoreModel _scoreModel;
    [SerializeField] private ScoreView _scoreView;
    private ScoreController _scoreController;   

    private PlayerNameModel _playerNameModel;
    private PlayerNameController _playerNameController;
    [SerializeField] private PlayerNameView _playerNameView;

    private TextMessageModel _textMessageModel;
    [SerializeField] private TextMessageView _textMessageView;
    private TextMessageController _textMessageController;

    [SerializeField] private CellsButtonView _cellsButtonView;
    private CellsButtonController _cellsButtonController;
    private CellsButtonModel _cellsButtonModel;

    private void Start()
    { 
        _fieldsModel = new FieldModel();

        _fieldController = new FieldController(_fieldsView, _fieldsModel);

        _roundModel = new RoundModel(_fieldsModel);

        _playerNameModel = new PlayerNameModel();
        _playerNameController = new PlayerNameController(_playerNameModel, _playerNameView);

        _scoreModel = new ScoreModel();
        _scoreController = new ScoreController(_scoreView,_scoreModel);

        _textMessageModel = new TextMessageModel();
        _textMessageController = new TextMessageController(_textMessageView, _textMessageModel);

        _cellsButtonModel = new CellsButtonModel();
        _cellsButtonController = new CellsButtonController(_cellsButtonView, _cellsButtonModel);
    }
}
