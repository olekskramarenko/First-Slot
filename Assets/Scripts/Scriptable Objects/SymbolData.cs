using UnityEngine;

[CreateAssetMenu(fileName = "New SymbolData", menuName = "Symbol Data", order = 51)]
public class SymbolData : ScriptableObject
{
    [SerializeField] private Sprite symbolImage;
    [SerializeField] private float symbolCost;
    [SerializeField] private SymbolType symbolType;

    public Sprite SymbolImage => symbolImage;

    public float SymbolCost => symbolCost;

    internal SymbolType SymbolType => symbolType;
}