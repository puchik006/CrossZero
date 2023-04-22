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
        RoundModel.OnRoundEnd += _scoreModel.ChangePlayerScore;
        _scoreModel.OnScoreChanged += _scoreView.ChangePlayerScore;
    }
}
