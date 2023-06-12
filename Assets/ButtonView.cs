using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    [SerializeField] private int _fieldNumber;
    private Image _image;
    public event Action<int> OnButtonClick;

    private void Start()
    {
        _image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(() => OnButtonClick?.Invoke(_fieldNumber));
    }

    public void ChangeSignWith(FieldValue fieldValue)
    {
        _image.sprite = SignDictionary.GetImageWithFieldValue(fieldValue);
    }
}

public class ButtonViewController
{
    private List<ButtonView> _buttonsViewList;
    private ButtonsModel _buttonsModel;

    public ButtonViewController(List<ButtonView> buttonsViewList, ButtonsModel buttonsModel)
    {
        _buttonsViewList = buttonsViewList;
        _buttonsModel = buttonsModel;

        Subscriptions();
    }

    private void Subscriptions()
    {
        ButtonsHandler.OnTwoPlayersGameStart += SetTwoPlayersGame;
    }

    private void SetTwoPlayersGame()
    {
        _buttonsViewList.ForEach(e => e.OnButtonClick += ChangeModel);


    }

    private void ChangeModel(int buttonNumber)
    {
        _buttonsModel.ChangeModel(buttonNumber,FieldValue.Empty);
    }
}

public class ButtonsModel
{
    private FieldValue[] _data = new FieldValue[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private event Action<int, FieldValue> OnButtonsModelChanged;

    public void ChangeModel(int number, FieldValue fieldValue)
    {
        if (_data[number] != FieldValue.Empty) return;

        _data[number] = fieldValue;
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

public class Turn
{
    private static bool _isFirstPlayerTurn = true;

    public Turn()
    {
        ButtonsHandler.OnTwoPlayersGameStart += SetTurnNonInternetGame;
    }

    private void SetTurnNonInternetGame()
    {

    }
}

public class Round
{

}

public class CrossZerroPlayer
{
    private string _name;
    private int _score;
    private bool _isMyTurn;
    private FieldValue _sign;
}