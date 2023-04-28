using System;

public class FieldModel 
{
    private FieldValue[] _data = new FieldValue[9] {0,0,0,0,0,0,0,0,0};
    public FieldValue[] Data { get => _data; }

    private bool _isLastFieldCross = false;

    public event Action<int, FieldValue> OnFieldValueChanged;

    public void ChangeMatrix(int number)
    {
        if (_data[number] != FieldValue.Empty) return;

        Data[number] = _isLastFieldCross ? FieldValue.Zero : FieldValue.Cross;
        _isLastFieldCross = !_isLastFieldCross;

        OnFieldValueChanged?.Invoke(number, Data[number]);
    }


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

    public void ChangeSignOnNewRound()
    {
        _isLastFieldCross = false;
    }
}
