using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignDictionary: MonoBehaviour 
{
    [SerializeField] private Sprite _cross;
    [SerializeField] private Sprite _zero;
    [SerializeField] private Sprite _empty;

    private static Dictionary<FieldValue, Sprite> _imageToFieldValue;
   
    private void Start()
    {
        _imageToFieldValue = new Dictionary<FieldValue, Sprite>()
        {
            {FieldValue.Empty,_empty},
            {FieldValue.Cross,_cross},
            {FieldValue.Zero,_zero},
        };
    }

    public static Sprite GetImageWithFieldValue(FieldValue fieldValue)
    {
        return _imageToFieldValue[fieldValue];
    }
}
