using System.Collections;
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
    
    
    void Start()
    {
        _fieldsModel = new FieldModel();
        _fieldController = new FieldController(_fieldsView, _fieldsModel);

        _roundModel = new RoundModel(_fieldsModel);

        _scoreModel = new ScoreModel();
        _scoreController = new ScoreController(_scoreView,_scoreModel);
    }
}
