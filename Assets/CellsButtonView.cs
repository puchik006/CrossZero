using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CellsButtonView : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons;
    public Action<int> OnButtonClick;

    private void Awake()
    {
        foreach (var button in _buttons)
        {
            button.Add(() => OnButtonClick?.Invoke(_buttons.IndexOf(button)));
        }
    }

    public void SetCellImage(int cellNumber, FieldValue fieldValue)
    {
        _buttons[cellNumber].GetComponent<Image>().sprite = SignDictionary.GetImageWithFieldValue(fieldValue);
    }

    public void ClearCells()
    {
        _buttons.ForEach(e => e.GetComponent<Image>().sprite = SignDictionary.GetImageWithFieldValue(FieldValue.Empty));
    }

    public void ChangeCellColor(int cellNumber)
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value,255);
        _buttons[cellNumber].GetComponent<Image>().color = randomColor;
    }

}
