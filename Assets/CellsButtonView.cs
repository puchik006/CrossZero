using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellsButtonView : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons;
    public event Action<int> ButtonClicked;

    private void Awake()
    {
        _buttons.ForEach((button) => button.Add(() => ButtonClicked?.Invoke(_buttons.IndexOf(button))));
    }

    public void SetCellImage(int cellNumber, CellValue fieldValue)
    {
        _buttons[cellNumber].GetComponent<Image>().sprite = SignDictionary.GetImageWithFieldValue(fieldValue);
    }

    public void SetCellColor(int cellNumber,Color color)
    {
        _buttons[cellNumber].GetComponent<Image>().color = color;
    }
}
