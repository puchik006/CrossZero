using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSignView : MonoBehaviour
{
    [SerializeField] private Image _playerOneSign;
    [SerializeField] private Image _playerTwoSign;

    private Dictionary<FieldValue, Color> _colorForValue = new Dictionary<FieldValue, Color>()
    {
        {FieldValue.Empty,Color.white},
        {FieldValue.Cross,Color.red},
        {FieldValue.Zero,Color.blue},
    };

    private void Start()
    {
        PlayersSign.OnMySignChanged += ChangeMySign;
        PlayersSign.OnAnotherPlayerSignChanged += ChangeAnotherPlayerSign;
    }

    private void ChangeMySign(FieldValue fieldValue)
    {
        _playerOneSign.color = _colorForValue[fieldValue];
    }

    private void ChangeAnotherPlayerSign(FieldValue fieldValue)
    {
        _playerTwoSign.color = _colorForValue[fieldValue];
    }
}
