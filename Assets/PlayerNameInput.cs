using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerNameInput;

    public static event Action<string> OnPlayerNameChanged;

    private void Start()
    {
        _playerNameInput.onValueChanged.AddListener(ChangeFieldValue);
        CheckSavedPlayerName();
    }

    private void ChangeFieldValue(string playerName)
    {
        OnPlayerNameChanged?.Invoke(playerName);
        PlayerPrefs.SetString("myName",playerName); // replace myName with Enum or simething else
    }

    private void CheckSavedPlayerName()
    {
        if (PlayerPrefs.HasKey("myName"))
        {
            _playerNameInput.text = PlayerPrefs.GetString("myName");
        }
    }
}
