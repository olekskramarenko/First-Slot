using UnityEngine;

[CreateAssetMenu(fileName = "New Game Config", menuName = "Game Config", order = 52)]
public class GameConfig : ScriptableObject
{
    [SerializeField] private SymbolData[] symbols;
    [SerializeField] private FinalScreenData[] finalScreens;
    [SerializeField] private WinLinesData[] winLines;

    public SymbolData[] Symbols => symbols;

    public FinalScreenData[] FinalScreens => finalScreens;

    public WinLinesData[] WinLines => winLines;
}
