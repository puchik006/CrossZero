using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Field", menuName = "Field", order = 51)]
public class Field : ScriptableObject
{
    [SerializeField] private byte _number;
    [SerializeField] private Color _sprite;

    public byte Number { get => _number; }
    public Color Sprite { get => _sprite;}
}
