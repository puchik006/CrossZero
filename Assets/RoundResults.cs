using System;
using System.Linq;

public class RoundResults
{
    private CellsButtonModel _model;
    public static event Action<GameStatus> OnRoundEnd;

    public RoundResults(CellsButtonModel model)
    {
        _model = model;

       CellsButtonModel.OnCellsModelChanged += CheckRoundResult;
    }

    private void CheckRoundResult(int cellNumber, CellValue fieldValue)
    {
        CellValue[] data = _model.Data;

        int[][] winningCombinations = new int[][]
        {
            new int[] { 0, 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6, 7, 8 },
            new int[] { 0, 3, 6 }, new int[] { 1, 4, 7 }, new int[] { 2, 5, 8 },
            new int[] { 0, 4, 8 }, new int[] { 2, 4, 6 }
        };

        foreach (var combination in winningCombinations)
        {
            if (data[combination[0]] != CellValue.Empty && data[combination[0]] == data[combination[1]] && data[combination[0]] == data[combination[2]])
            {
                OnRoundEnd?.Invoke(data[combination[0]] == CellValue.Cross ? GameStatus.CrossWin : GameStatus.ZeroWin);
                return;
            }
        }

        if (!data.Contains(CellValue.Empty))
        {
            OnRoundEnd?.Invoke(GameStatus.Draw);
            return;
        }
    }
}
