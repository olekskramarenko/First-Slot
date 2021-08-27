using UnityEngine;

public class MovingSymbols : MonoBehaviour, IUpdatable
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
    private int symbolsCounter;
    private bool slowDownStatus;
    private float startReelPos, fullSpinDistance;

    public int ReelId => reelId;
    public float StartReelPos { get => startReelPos; set => startReelPos = value; }
    public float FullSpinDistance { get => fullSpinDistance; set => fullSpinDistance = value; }
    public bool SlowDownStatus { get => slowDownStatus; set => slowDownStatus = value; }

    public delegate void PlaySoundEvent(SoundType sound);
    public static event PlaySoundEvent OnSoundPLayed;
    void Start()
    {
        symbolsCounter = 0;
        mainCanvasScale = mainCanvas.lossyScale.y;
    }

    public void ResetSymbolReelsCounter()
    {
        symbolsCounter = 0;
    }
    private void ChangeSymbolAndSprite()
    {
        for (int i = 0; i < allSymbols.Length; i++)
        {
            var symbol = allSymbols[i];
            if (symbol.transform.position.y <= exitPosition * mainCanvasScale)
            {
                symbol.transform.position += Vector3.up * symbolHeight * symbolsCount * mainCanvasScale;
                if (OnSoundPLayed != null & reelId == 0) OnSoundPLayed(SoundType.reelChanged);
                if (slowDownStatus)
                {
                    int symbolFinalId = symbolsCounter;
                    if (symbolFinalId < 3) symbolsCounter++;
                    int symbolId = (reelId * allSymbols.Length) + symbolFinalId;
                    symbol.SymbolFinalId = symbolId;
                    int finalImageId = FinalResult.GetFinalImageId(symbolId);
                    symbol.SymbolImage.sprite = gameConfig.Symbols[finalImageId].SymbolImage;
                    symbol.SymbolType = gameConfig.Symbols[finalImageId].SymbolType;
                    symbol.SymbolCost = gameConfig.Symbols[finalImageId].SymbolCost;
                }
                else
                {
                    var random = Random.Range(0, gameConfig.Symbols.Length);
                    symbol.SymbolImage.sprite = gameConfig.Symbols[random].SymbolImage;
                }
            }
        }
    }

    public void GiveToUpdate()
    {
        ChangeSymbolAndSprite();
    }
    void OnEnable()
    {
        UpdateManager.Register(this);
    }

    void OnDisable()
    {
        UpdateManager.Unregister(this);
    }

}
