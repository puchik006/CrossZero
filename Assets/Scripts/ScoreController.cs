using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController
{
    private ScoreView _scoreView;
    private IScoreModel _scoreModel;

    public ScoreController(ScoreView scoreView, IScoreModel scoreModel)
    {
        _scoreView = scoreView;
        _scoreModel = scoreModel;

        Enable();
    }

    private void Enable()
    {
        RoundModel.OnRoundEnd += _scoreModel.ChangePlayerScore; //a bit mess responsibilities
        _scoreModel.OnScoreChanged += _scoreView.ChangePlayerScore;
    }
}
