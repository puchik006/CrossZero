using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.Netcode;

public class FieldView : MonoBehaviour
{
    [SerializeField] private int _fieldNumber;
    private Image _image;
    private Dictionary<FieldValue,Color> _colorForValue = new Dictionary<FieldValue, Color>()
    {
        {FieldValue.Empty,Color.white},
        {FieldValue.Cross,Color.red},
        {FieldValue.Zero,Color.blue},
    };
    public event Action<int> OnFieldTouched;
    public static event Action<int> OnButtonClick;

    private void Start()
    {
        _image = GetComponent<Image>();
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    private void ButtonClick() // rename Actions - one of them is for NetPlayer feedback
    {
        OnFieldTouched?.Invoke(_fieldNumber); // change field on yourself
        OnButtonClick?.Invoke(_fieldNumber); // feedback for second player
    }
    
    public void ChangeFieldColor(FieldValue fieldValue)
    {
        _image.color = _colorForValue[fieldValue];
    }

    public void ClearField()
    {
        _image.color = Color.white;
    }
}
