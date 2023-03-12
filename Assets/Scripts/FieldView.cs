using UnityEngine;
using System;
using System.Collections.Generic;

public class FieldView : MonoBehaviour
{
    [SerializeField] private int _fieldNumber;
    private SpriteRenderer _spriteRenderer;
    private Dictionary<FieldValue,Color> _colorForValue = new Dictionary<FieldValue, Color>()
    {
        {FieldValue.Empty,Color.white},
        {FieldValue.Cross,Color.red},
        {FieldValue.Zero,Color.blue},
    };
    public event Action<int> OnFieldTouched;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        OnFieldTouched?.Invoke(_fieldNumber);
    }

    public void ChangeFieldColor(FieldValue fieldValue)
    {
        _spriteRenderer.color = _colorForValue[fieldValue];
    }

    public void ClearField()
    {
        _spriteRenderer.color = Color.white;
    }
}
