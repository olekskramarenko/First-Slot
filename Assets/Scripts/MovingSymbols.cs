using UnityEngine;

public class MovingSymbols : MonoBehaviour
{
    [SerializeField] private Symbol[] allSymbols;
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private MovingReels MovingReels;
    [SerializeField] private FinalResult FinalResult;
    [SerializeField] private float symbolHeight;
    [SerializeField] private int symbolsCount, reelId;
    [SerializeField] private RectTransform mainCanvas;
    private float mainCanvasScale;
    private readonly int exitPosition = 223;
    private readonly int numberOfReels = 3;
    private int[] symbolReelsCounter;
    private ReelStates state = ReelStates.Stop;
    private float startReelPos, fullSpinDistance;

    internal ReelStates State { get => state; set => state = value; }
    public int ReelId => reelId;
    public float StartReelPos { get => startReelPos; set => startReelPos = value; }
    public float FullSpinDistance { get => fullSpinDistance; set => fullSpinDistance = value; }

    void Start()
    {
        symbolReelsCounter = new int[numberOfReels];
        mainCanvasScale = mainCanvas.lossyScale.y;
    }

    public void ResetSymbolReelsCounter()
    {
        for (int i = 0; i < symbolReelsCounter.Length; i++)
        {
            symbolReelsCounter[i] = 0;
        }
    }
    private void ChangeSymbolAndSprite(ReelStates reelState)
    {
        for (int i = 0; i < allSymbols.Length; i++)
        {
            var symbol = allSymbols[i];
            if (symbol.transform.position.y <= exitPosition * mainCanvasScale)
            {
                symbol.transform.position += Vector3.up * symbolHeight * symbolsCount * mainCanvasScale;
                if (reelState == ReelStates.SlowDown)
                {
                    int symbolFinalId = symbolReelsCounter[reelId];
                    if (symbolFinalId < 3) symbolReelsCounter[reelId]++;
                    int symbolId = (reelId * allSymbols.Length) + symbolFinalId;
                    symbol.SymbolFinalId = symbolId;
                    int finalImageId = FinalResult.GetFinalImageId(symbolId);
                    symbol.SymbolImage.sprite = gameConfig.Symbols[finalImageId].SymbolImage;
                    symbol.SymbolType = gameConfig.Symbols[finalImageId].SymbolType;
                } 
                else
                {
                    var random = Random.Range(0, gameConfig.Symbols.Length);
                    symbol.SymbolImage.sprite = gameConfig.Symbols[random].SymbolImage;
                }
            }
        }
    }

    void Update()
    {
        ChangeSymbolAndSprite(state);
    }
}
