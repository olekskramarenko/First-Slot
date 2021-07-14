using UnityEngine;

public class MovingSymbols : MonoBehaviour
{
    [SerializeField] private Symbol[] allSymbols;
    [SerializeField] private Sprite[] allSymbolImages;
    [SerializeField] private MovingReels MovingReels;
    [SerializeField] private FinalResult FinalResult;
    [SerializeField] private float symbolHeight;
    [SerializeField] private int symbolsCount, reelId;
    [SerializeField] private RectTransform mainCanvas;
    private float mainCanvasScale;
    private readonly int exitPosition = 223;
    private readonly int numberOfReels = 3;
    private bool slowDownIsActive;
    private int[] symbolReelsCounter;

    void Start()
    {
        symbolReelsCounter = new int[numberOfReels];
        //slowDownIsActive = new bool[numberOfReels];
        mainCanvasScale = mainCanvas.lossyScale.y;
    }

    public void ResetSymbolReelsCounter()
    {
        for ( int i=0; i < symbolReelsCounter.Length; i++)
        {
            symbolReelsCounter[i] = 0;
        }
    }
    void ChangeSymbolAndSprite(bool slowDownIsActive)
    {
        for (int i = 0; i < allSymbols.Length; i++)
        {
            var symbol = allSymbols[i];
            if (!slowDownIsActive)
            {
                if (symbol.transform.position.y <= exitPosition * mainCanvasScale)
                {
                    symbol.transform.position += Vector3.up * symbolHeight * symbolsCount * mainCanvasScale;
                    symbol.SymbolImage.sprite = allSymbolImages[Random.Range(0, allSymbolImages.Length)];
                };
            }
            if (slowDownIsActive)
            {
                if (symbol.transform.position.y <= exitPosition * mainCanvasScale)
                {
                    symbol.transform.position += Vector3.up * symbolHeight * symbolsCount * mainCanvasScale;
                    int symbolFinalId = symbolReelsCounter[reelId];
                    
                    if (symbolFinalId < 3) symbolReelsCounter[reelId]++;
                    int symbolId = (reelId * allSymbols.Length) + symbolFinalId;
                    int finalImageId = FinalResult.GetFinalImageId(symbolId);
                    symbol.SymbolImage.sprite = allSymbolImages[finalImageId];
                }
            }
        }
    }
    void Update()
    {
        slowDownIsActive = MovingReels.GetSlowDownStatus(reelId);

        ChangeSymbolAndSprite(slowDownIsActive);
    }
}
