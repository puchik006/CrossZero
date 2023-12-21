using System.Collections.Generic;
using UnityEngine;

public class SignDictionary: MonoBehaviour 
{
    [SerializeField] private Sprite _cross;
    [SerializeField] private Sprite _zero;
    [SerializeField] private Sprite _empty;
    [SerializeField] private Sprite _exit;

    private static Dictionary<CellValue, Sprite> _imageToFieldValue;
   
    private void Start()
    {
        _imageToFieldValue = new Dictionary<CellValue, Sprite>()
        {
            {CellValue.Empty,_empty},
            {CellValue.Cross,_cross},
            {CellValue.Zero,_zero},
            {CellValue.Exit,_exit},
        };
    }

    public static Sprite GetImageWithFieldValue(CellValue fieldValue)
    {
        return _imageToFieldValue[fieldValue];
    }
}
