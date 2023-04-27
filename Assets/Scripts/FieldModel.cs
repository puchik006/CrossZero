using System;

public class FieldModel 
{
    private FieldValue[] _data = new FieldValue[9] {0,0,0,0,0,0,0,0,0};
    public FieldValue[] Data { get => _data; }

    public event Action<int, FieldValue> OnFieldValueChanged;

    public void ChangeMatrix(int number, FieldValue fieldValue)
    {
        if (_data[number] != FieldValue.Empty) return;

        _data[number] = fieldValue;
        OnFieldValueChanged?.Invoke(number, Data[number]);
    }

    public void ClearMatrix()
    {
        Array.Clear(_data, 0, _data.Length);
    }
}
