using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    [SerializeField] private SymbolData symbolData;
    [SerializeField] private Image symbolImage;
    [SerializeField] private RectTransform symbolRT;

    public Image SymbolImage { get => symbolImage; set => symbolImage = value; }
    public SymbolData SymbolData { get => symbolData; set => symbolData = value; }
    public RectTransform SymbolRT { get => symbolRT; set => symbolRT = value; }
}
