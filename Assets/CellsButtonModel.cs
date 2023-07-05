using System;

public class CellsButtonModel
{
    private FieldValue[] _data = new FieldValue[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public static Action<int, FieldValue> OnButtonsModelChanged;

    public FieldValue[] Data { get => _data;}

    private FieldValue _lastValue = FieldValue.Zero;

    public void ChangeModel(int number)
    {
        if (_data[number] != FieldValue.Empty) return;

        _data[number] = _lastValue == FieldValue.Cross ? FieldValue.Zero : FieldValue.Cross;
        _lastValue = _data[number];

        OnButtonsModelChanged?.Invoke(number, _data[number]);
    }

    public void ClearMatrix()
    {
        Array.Clear(_data, 0, _data.Length);
        _lastValue = FieldValue.Zero;
    }
}
