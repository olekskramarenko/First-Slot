using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    [SerializeField] private Image symbolImage;
    [SerializeField] private RectTransform symbolRT;

    public Image SymbolImage { get => symbolImage; set => symbolImage = value; }
    public RectTransform SymbolRT { get => symbolRT; set => symbolRT = value; }
}
