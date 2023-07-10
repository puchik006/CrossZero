using System;
using System.Linq;

public class CellsButtonModel
{
    private CellValue[] _data = new CellValue[9];
    public CellValue[] Data => _data;
    
    public static event Action<int, CellValue> OnCellsModelChanged;

    public void ChangeModel(int number)
    {
        if (_data[number] != CellValue.Empty) return;
        _data[number] = GetNextValue();
        OnCellsModelChanged?.Invoke(number, _data[number]);
    }

    public void ClearMatrix()
    {
        Array.Clear(_data, 0, _data.Length);

        for (int i = 0; i < _data.Length; i++)
        {
            OnCellsModelChanged?.Invoke(i, CellValue.Empty);
        }
    }

    private CellValue GetNextValue()
    {
        int crossesCount = _data.Count(value => value == CellValue.Cross);
        int zerosCount = _data.Count(value => value == CellValue.Zero);
        return crossesCount <= zerosCount ? CellValue.Cross : CellValue.Zero;
    }
}
