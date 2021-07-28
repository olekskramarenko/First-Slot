using UnityEngine;

[CreateAssetMenu(fileName = "New Game Config", menuName = "Game Config", order = 52)]
public class GameConfig : ScriptableObject
{
    [SerializeField] private SymbolData[] symbols;
    [SerializeField] private FinalScreenData[] finalScreens;

    public SymbolData[] Symbols => symbols;

    public FinalScreenData[] FinalScreens => finalScreens; 
}
