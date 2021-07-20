using UnityEngine;

public class FinalResult : MonoBehaviour
{
    private int currentFinalScreen = 0;
    [SerializeField] private GameConfig gameConfig;
    private int[] finalScreen;

    public int CurrentFinalScreen => currentFinalScreen; 

    public int GetFinalImageId(int symbolId)
    {
        int finalImageId = finalScreen[symbolId];
        return finalImageId;
    }

    public void SetNextFinalScreen()
    {
        if (currentFinalScreen == gameConfig.FinalScreens.Length - 1)
        {
            currentFinalScreen = 0;
        }
        else
        {
            currentFinalScreen++;
        }
        finalScreen = gameConfig.FinalScreens[currentFinalScreen].FinalSymbols;
    }
    void Start()
    {
        finalScreen = gameConfig.FinalScreens[currentFinalScreen].FinalSymbols;
    }
}
