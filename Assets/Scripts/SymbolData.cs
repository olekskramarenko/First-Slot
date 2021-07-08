using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New SymbolData", menuName = "Symbol Data", order = 51)]
public class SymbolData : ScriptableObject
{
    [SerializeField] private int symbolId;
    [SerializeField] private Sprite symbolImage;
    [SerializeField] private int reelId;

    public int ReelId { get => reelId;}
    public int SymbolId { get => symbolId;}
}
