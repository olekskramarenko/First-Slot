using System.Collections;
using UnityEngine;

public class FreeSpinsController : MonoBehaviour
{
    [SerializeField] private MovingReels movingReels;
    [SerializeField] private PopUpsController popUpsController;
    [SerializeField] private ReelsStateController reelsStateController;
    [SerializeField] private int numberOfFreeSpins;
    [SerializeField] private PrizeCalculation prizeCalculation;
    private int freeSpinsCounter;

    public delegate void PlaySoundEvent(SoundType sound);
    public static event PlaySoundEvent OnSoundPLayed;

    public delegate void StopSoundEvent(SoundType sound);
    public static event StopSoundEvent OnSoundStopped;


    public void StartFreeSpins()
    {
        if (OnSoundPLayed != null) OnSoundStopped(SoundType.background);
        if (OnSoundPLayed != null) OnSoundPLayed(SoundType.freeSpins);
        reelsStateController.FreeSpinsGame = true;
        popUpsController.ShowFreeSpinsStart();
        freeSpinsCounter = numberOfFreeSpins;
        popUpsController.CounterText.text = freeSpinsCounter.ToString();
    }

    public void StartAutoSpins()
    {
        StartCoroutine(WaitAndStartAutoSpin());
    }

    IEnumerator WaitAndStartAutoSpin()
    {
        while (freeSpinsCounter > 0)
        {
            yield return new WaitUntil(() => reelsStateController.ReelState == ReelStates.ReadyForSpin);
            movingReels.MovingStart();
            freeSpinsCounter--;
            popUpsController.CounterText.text = freeSpinsCounter.ToString();
        }
        if (freeSpinsCounter == 0)
        {
            reelsStateController.FreeSpinsGame = false;
            yield return new WaitUntil(() => reelsStateController.ReelState == ReelStates.ReadyForSpin);
            FinishFreeSpins();
        }
    }

    private void FinishFreeSpins()
    {
        popUpsController.CloseSpinsCounter();
        popUpsController.ShowAndCloseResultPopUp();
        freeSpinsCounter = numberOfFreeSpins;
        prizeCalculation.ResetFreeSpinsPrize();
    }
}
