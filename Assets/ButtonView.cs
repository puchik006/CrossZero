using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    private Image _image;
    public static event Action<GameObject> OnButtonClick;

    private void Start()
    {
        _image = GetComponent<Image>();
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);
    }



    private void ButtonClick()
    {
        //_image.sprite = SignDictionary.GetImageWithFieldValue(FieldValue.Cross);
        OnButtonClick?.Invoke(gameObject);
    }
}

public class ButtonViewController
{

}

public class ButtonsModel
{
    private FieldValue[] _data = new FieldValue[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public void ChangeModel()
    {

    }
}