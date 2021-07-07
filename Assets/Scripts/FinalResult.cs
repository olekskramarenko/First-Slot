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
        finalScreens[0] = new int[12] { 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2 };
        finalScreens[1] = new int[12] { 0, 1, 1, 3, 0, 1, 1, 3, 0, 1, 1, 3 };
        finalScreens[2] = new int[12] { 0, 2, 2, 3, 0, 2, 2, 4, 0, 2, 2, 3 };
        finalScreens[3] = new int[12] { 0, 3, 3, 0, 3, 3, 0, 0, 0, 3, 3, 1 };
        finalScreens[4] = new int[12] { 0, 4, 4, 0, 0, 4, 4, 0, 0, 4, 4, 0 };
    }
}
