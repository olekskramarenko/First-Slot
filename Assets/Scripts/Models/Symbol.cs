using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    [SerializeField] private Image symbolImage;
    [SerializeField] private RectTransform symbolRT;
    [SerializeField] private Animation symbolAnimation;
    private int symbolFinalId;
    private SymbolType symbolType;
    private int symbolCost;

    public Image SymbolImage { get => symbolImage; set => symbolImage = value; }
    public RectTransform SymbolRT { get => symbolRT; set => symbolRT = value; }
    public int SymbolFinalId { get => symbolFinalId; set => symbolFinalId = value; }
    public Animation SymbolAnimation { get => symbolAnimation; set => symbolAnimation = value; }
    public int SymbolCost { get => symbolCost; set => symbolCost = value; }
    internal SymbolType SymbolType { get => symbolType; set => symbolType = value; }
}
