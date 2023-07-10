using System;

public class RoundModel
{
    private FieldModel _fieldModel;
    public static event Action<GameStatus> OnRoundEnd;

    public RoundModel(FieldModel fieldModel)
    {
        _fieldModel = fieldModel;
        FieldModel.OnFieldValueChanged += OnFieldsChange;
    }

    private void OnFieldsChange(int fieldNumber, CellValue fieldValue)
    {
        CheckGameState(_fieldModel.Data);
    }

    private void CheckGameState(CellValue[] data)
    {
        bool IsEmptyFieldsExist() => Array.Exists(data, e => e == CellValue.Empty);

        if (!IsEmptyFieldsExist() && GameState(data) == GameStatus.InProgress)
        {
            OnRoundEnd?.Invoke(GameStatus.Draw);
        }

        if (GameState(data) != GameStatus.InProgress)
        {
            OnRoundEnd?.Invoke(GameState(data));
        }
    }

    //TODO: try to make easier
    private GameStatus GameState(CellValue[] data)
    {
        //rows
        if ((data[0] == data[1]) && (data[0] == data[2]) && data[0] != CellValue.Empty)
        {
            return data[0] == CellValue.Cross ? GameStatus.CrossWin : GameStatus.ZeroWin;
        }

        if ((data[3] == data[4]) && (data[3] == data[5]) && data[3] != CellValue.Empty)
        {
            return data[3] == CellValue.Cross ? GameStatus.CrossWin : GameStatus.ZeroWin;
        }

        if ((data[6] == data[7]) && (data[6] == data[8]) && data[6] != CellValue.Empty)
        {
            return data[6] == CellValue.Cross ? GameStatus.CrossWin : GameStatus.ZeroWin;
        }
        //columns
        if ((data[0] == data[3]) && (data[0] == data[6]) && data[0] != CellValue.Empty)
        {
            return data[0] == CellValue.Cross ? GameStatus.CrossWin : GameStatus.ZeroWin;
        }

        if ((data[1] == data[4]) && (data[1] == data[7]) && data[1] != CellValue.Empty)
        {
            return data[1] == CellValue.Cross ? GameStatus.CrossWin : GameStatus.ZeroWin;
        }

        if ((data[2] == data[5]) && (data[2] == data[8]) && data[2] != CellValue.Empty)
        {
            return data[2] == CellValue.Cross ? GameStatus.CrossWin : GameStatus.ZeroWin;
        }
        //diagonals
        if ((data[0] == data[4]) && (data[0] == data[8]) && data[0] != CellValue.Empty)
        {
            return data[0] == CellValue.Cross ? GameStatus.CrossWin : GameStatus.ZeroWin;
        }

        if ((data[2] == data[4]) && (data[2] == data[6]) && data[2] != CellValue.Empty)
        {
            return data[2] == CellValue.Cross ? GameStatus.CrossWin : GameStatus.ZeroWin;
        }

        return GameStatus.InProgress;
    }
}
