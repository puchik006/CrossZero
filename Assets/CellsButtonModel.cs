using System;

public class CellsButtonModel
{
    private FieldValue[] _data = new FieldValue[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public static Action<int, FieldValue> OnButtonsModelChanged;

    public void ChangeModel(int number)
    {
        if (_data[number] != FieldValue.Empty) return;

        _data[number] = FieldValue.Cross;
        OnButtonsModelChanged?.Invoke(number, _data[number]);
    }

    public void ClearMatrix()
    {
        Array.Clear(_data, 0, _data.Length);
    }

    private void CheckGameState()
    {

    }
}
