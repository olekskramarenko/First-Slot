using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelsStateController : MonoBehaviour
{
    private ReelStates reelState = ReelStates.Stop;
    [SerializeField] private ButtonsView buttonsView;
     
    internal ReelStates ReelState { get => reelState; set => reelState = value; }

    public void StartGame()
    {
        reelState = ReelStates.ReadyForSpin;
        buttonsView.DeactivateStopBtn();
        buttonsView.ActivatePlayBtn();
    }

    public void StartSpinning()
    {
        reelState = ReelStates.StartSpin;
        buttonsView.DeactivatePlayBtn();
    }

    public void MainWaySpinning()
    {
        reelState = ReelStates.Spin;
        buttonsView.SetStopBtnInteractable();
    }
    public void SlowDownSpinning()
    {
        reelState = ReelStates.SlowDown;
        buttonsView.SetStopBtnNonInteractable();
    }
    public void EndOfSpinning()
    {
        reelState = ReelStates.Stop;
    }
    public void ForceStopped()
    {
        reelState = ReelStates.ForceStop;
        buttonsView.SetStopBtnNonInteractable();
    }


}
