using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        CellsButtonModel.OnButtonsModelChanged += ChangeSignWith;
    }

    private void ChangeSignWith(int cellNumber, FieldValue fieldValue)
    {
        _buttons[cellNumber].GetComponent<Image>().sprite = SignDictionary.GetImageWithFieldValue(fieldValue);
    }
}
