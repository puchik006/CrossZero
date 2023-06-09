using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class FieldView : MonoBehaviour
{
    [SerializeField] private int _fieldNumber;
    private Image _image;

    public static event Action<int> OnButtonClick;

    private void Start()
    {
        _image = gameObject.GetComponent<Image>();
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        OnButtonClick?.Invoke(_fieldNumber);
    }
    
    public void ChangeFieldColor(FieldValue fieldValue)
    {
        //_image.color = _colorForValue[fieldValue];

        _image.sprite = SignDictionary.GetImageWithFieldValue(fieldValue);
    }

    public void ClearField()
    {
        _image.color = Color.white;
    }
}
