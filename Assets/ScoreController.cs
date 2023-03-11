using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController
{
    private ScoreView _scoreView;
    private ScoreModel _scoreModel;

    public ScoreController(ScoreView scoreView, ScoreModel scoreModel)
    {
        _scoreView = scoreView;
        _scoreModel = scoreModel;

        Enable();
    }

    private void Enable()
    {
        RoundModel.OnRoundEnd += _scoreModel.ChangePlayerScore;
        _scoreModel.OnScoreChanged += _scoreView.ChangePlayerScore;
    }
}
