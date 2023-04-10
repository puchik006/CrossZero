using UnityEngine;
using TMPro;

public class PlayerNameView: MonoBehaviour
{
    [SerializeField] private TMP_Text _playerOneName;
    [SerializeField] private TMP_Text _playerTwoName;

    public static string _myName;

    private void Start()
    {
        _myName =  PlayerPrefs.HasKey(PlayerKeys.Name) ? PlayerPrefs.GetString(PlayerKeys.Name) : "Player 2";
    }

    public void SetPlayerOneName(string name)
    {
        _playerOneName.text = name;
    }

    public void SetPlayerTwoName(string name) 
    {  
        _playerTwoName.text = name;
    }
}
