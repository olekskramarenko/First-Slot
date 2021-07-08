using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResult : MonoBehaviour
{
    private int[][] finalScreens = new int[5][];
    private int finalSymbol;
    private int currentFinalScreen = 0;
    public int CurrentFinalScreen
    {
        get
        {
            return currentFinalScreen;
        }
    }

    public int GetFinalImageId(int symbolId)
    {
        int finalImageId = finalScreens[currentFinalScreen][symbolId-1];
        return finalImageId;
    }

    public void SetNextFinalScreen()
    {
        if (currentFinalScreen == finalScreens.Length - 1)
        {
            currentFinalScreen = 0;
        }
        else
        {
            currentFinalScreen++;
        }
    }
    void Start()
    {
        finalScreens[0] = new int[12] { 7, 2, 0, 0, 7, 2, 0, 0, 7, 2, 0, 0 };
        finalScreens[1] = new int[12] { 0, 3, 1, 1, 0, 3, 1, 1, 0, 3, 1, 1 };
        finalScreens[2] = new int[12] { 7, 3, 2, 2, 7, 3, 2, 2, 7, 3, 2, 2 };
        finalScreens[3] = new int[12] { 0, 5, 3, 3, 0, 5, 3, 3, 0, 5, 3, 3 };
        finalScreens[4] = new int[12] { 6, 0, 4, 4, 6, 0, 4, 4, 6, 0, 4, 4 };
    }
}
