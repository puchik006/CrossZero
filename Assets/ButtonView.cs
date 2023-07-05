using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    [SerializeField] private int _fieldNumber;
    private Image _image;
    public event Action<int> OnButtonClick;

    private void Start()
    {
        _image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(() => OnButtonClick?.Invoke(_fieldNumber));
    }

    public void ChangeSignWith(FieldValue fieldValue)
    {
        _image.sprite = SignDictionary.GetImageWithFieldValue(fieldValue);
    }
}