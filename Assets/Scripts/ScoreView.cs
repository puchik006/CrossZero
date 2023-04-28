using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerOneScore;
    [SerializeField] private TMP_Text _playerTwoScore;


    public void ChangePlayerScore(string playerOneScore, string playerTwoScore)
    {
        _playerOneScore.text = playerOneScore;
        _playerTwoScore.text = playerTwoScore;
    }
}
