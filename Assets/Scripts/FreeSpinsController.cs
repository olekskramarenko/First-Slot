using System.Collections;
using UnityEngine;

public class FreeSpinsController : MonoBehaviour
{
    [SerializeField] private MovingReels movingReels;
    [SerializeField] private PopUpsController popUpsController;
    [SerializeField] private ReelsStateController reelsStateController;
    private int freeSpinsCounter = 3;

    public void StartFreeSpins()
    {
        popUpsController.ShowFreeSpinsStart();
        popUpsController.CounterText.text = freeSpinsCounter.ToString();
        reelsStateController.FreeSpinsGame = true;
    }

    public void StartAutoSpins()
    {
        StartCoroutine(WaitAndStartAutoSpin());
    } 

    IEnumerator WaitAndStartAutoSpin()
    {
        
        while ( freeSpinsCounter > 0)
        {
            yield return new WaitUntil(() => reelsStateController.ReelState == ReelStates.ReadyForSpin);
            movingReels.MovingStart();
            freeSpinsCounter--;
            popUpsController.CounterText.text = freeSpinsCounter.ToString();
            if ( freeSpinsCounter == 0 )
            {
                FinishFreeSpins();
            }
        } 
    }

    private void FinishFreeSpins()
    {
        reelsStateController.FreeSpinsGame = false;
        popUpsController.CloseSpinsCounter();
    }

}
