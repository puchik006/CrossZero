using UnityEngine;
using TMPro;

public class PlayerNameView: MonoBehaviour
{
    [SerializeField] private TMP_Text _playerOneName;
    [SerializeField] private TMP_Text _playerTwoName;

    public void SetPlayerOneName(string name)
    {
        _playerOneName.text = name;
    }

    public void SetPlayerTwoName(string name) 
    {  
        _playerTwoName.text = name;
    }
}
