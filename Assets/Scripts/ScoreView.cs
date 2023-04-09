using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerOneScore;
    [SerializeField] private TMP_Text _playerTwoScore;
    [SerializeField] private TMP_Text _playerOneName;
    [SerializeField] private TMP_Text _playerTwoName;

    public void ChangePlayerScore(string playerOneScore, string playerTwoScore)
    {
        _playerOneScore.text = playerOneScore;
        _playerTwoScore.text = playerTwoScore;
    }
}
