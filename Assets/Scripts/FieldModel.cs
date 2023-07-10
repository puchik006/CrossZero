using System;

public class FieldModel 
{
    private CellValue[] _data = new CellValue[9] {0,0,0,0,0,0,0,0,0};
    public CellValue[] Data { get => _data; }

    public static event Action<int, CellValue> OnFieldValueChanged;

    public void ChangeMatrix(int number, CellValue fieldValue) 
    {
        if (_data[number] != CellValue.Empty) return;

        _data[number] = fieldValue;
        OnFieldValueChanged?.Invoke(number, Data[number]);
    }

    public void ClearMatrix()
    {
        Array.Clear(_data, 0, _data.Length);
    }
}
