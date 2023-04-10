using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerNameInput;

    private void Start()
    {
        _playerNameInput.onValueChanged.AddListener(ChangeFieldValue);
        CheckSavedPlayerName();
    }

    private void ChangeFieldValue(string playerName)
    {
        PlayerPrefs.SetString(PlayerKeys.Name,playerName);
    }

    private void CheckSavedPlayerName()
    {
        if (PlayerPrefs.HasKey(PlayerKeys.Name))
        {
            _playerNameInput.text = PlayerPrefs.GetString(PlayerKeys.Name);
        }
    }
}
