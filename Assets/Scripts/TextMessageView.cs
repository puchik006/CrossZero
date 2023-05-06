using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextMessageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _gameMessage;
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private int _messageTime = 5;

    public static event Action OnMessageStop;
    public static event Action OnMessageStart;

    public void ShowGameMessage(string message)
    {
        _gameMessage.text = message;
        StartCoroutine(ShowMessageForSec(_messageTime));
    }

    private IEnumerator ShowMessageForSec(int sec)
    {
        SetMessagePanelActive();
        yield return new WaitForSeconds(sec);
        SetMessagePanelInactive();
    }

    public void SetMessagePanelActive()
    {
        _messagePanel.SetActive(true);
        OnMessageStart?.Invoke();
    }
    public void SetMessagePanelInactive()
    {
        _messagePanel.SetActive(false);
        OnMessageStop?.Invoke();
    }
}
