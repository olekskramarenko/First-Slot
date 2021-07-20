using UnityEngine;

public class FinalResult : MonoBehaviour
{
    //private int[][] finalScreens = new int[5][];
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


                                     // 0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11 };
        //finalScreens[0] = new int[12] { 7, 2, 0, 0, 7, 2, 0, 0, 7, 2, 0, 0 };
        //finalScreens[1] = new int[12] { 0, 3, 1, 1, 0, 3, 1, 1, 0, 3, 1, 1 };
        //finalScreens[2] = new int[12] { 7, 3, 2, 2, 7, 3, 2, 2, 7, 3, 2, 2 };
        //finalScreens[3] = new int[12] { 0, 5, 3, 3, 0, 5, 3, 3, 0, 5, 3, 3 };
        //finalScreens[4] = new int[12] { 6, 0, 4, 4, 6, 0, 4, 4, 6, 0, 4, 4 };
    }
}
