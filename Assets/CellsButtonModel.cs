using System;
using System.Linq;

public class CellsButtonModel
{
    private FieldValue[] _data = new FieldValue[9];
    public FieldValue[] Data => _data;
    public static event Action<int, FieldValue> OnCellsModelChanged;

    public void ChangeModel(int number)
    {
        if (_data[number] != FieldValue.Empty) return;

        _data[number] = GetNextValue();
        OnCellsModelChanged?.Invoke(number, _data[number]);
    }

    public void ClearMatrix()
    {
        Array.Clear(_data, 0, _data.Length);
    }

    private FieldValue GetNextValue()
    {
        int crossesCount = _data.Count(value => value == FieldValue.Cross);
        int zerosCount = _data.Count(value => value == FieldValue.Zero);
        return crossesCount <= zerosCount ? FieldValue.Cross : FieldValue.Zero;
    }
}
