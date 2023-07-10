using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSignView : MonoBehaviour
{
    [SerializeField] private Image _playerOneSign;
    [SerializeField] private Image _playerTwoSign;

    private Dictionary<CellValue, Color> _colorForValue = new Dictionary<CellValue, Color>() // same as in FieldView
    {
        {CellValue.Empty,Color.white},
        {CellValue.Cross,Color.red},
        {CellValue.Zero,Color.blue},
    };

    private void Start()
    {
        PlayersSign.OnMySignChanged += ChangeFirstPlayerSign;
        PlayersSign.OnAnotherPlayerSignChanged += ChangeSecondPlayerSign;
    }

    private void ChangeFirstPlayerSign(CellValue fieldValue)
    {
        _playerOneSign.color = _colorForValue[fieldValue];
    }

    private void ChangeSecondPlayerSign(CellValue fieldValue)
    {
        _playerTwoSign.color = _colorForValue[fieldValue];
    }
}
