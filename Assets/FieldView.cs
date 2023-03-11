using UnityEngine;
using System;

public class FieldView : MonoBehaviour
{
    [SerializeField] private Field _field;// TODO:replace to int
    private SpriteRenderer _spriteRenderer;

    public event Action<int> OnFieldTouched;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        OnFieldTouched?.Invoke(_field.Number);
    }

    public void ChangeFieldColor(FieldValue fieldValue)
    {
        if (fieldValue == FieldValue.Cross)
        {
            _spriteRenderer.color = Color.red;
        }
        else if (fieldValue == FieldValue.Zero)
        {
            _spriteRenderer.color = Color.blue;
        }
        else
        {
            _spriteRenderer.color = Color.white;
        }
    }

    public void ClearField()
    {
        _spriteRenderer.color = Color.white;
    }
}
