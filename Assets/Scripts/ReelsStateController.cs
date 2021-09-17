using UnityEngine;
using System.Collections.Generic;
using System;

public class ReelsStateController : MonoBehaviour
{

    [SerializeField] private ButtonsView buttonsView;
    [SerializeField] private MovingSymbols[] movingSymbols;
    private ReelStates reelState;
    private bool freeSpinsGame;
    private Dictionary<ReelStates, Action> stateChangesDictionary;
    internal ReelStates ReelState { get => reelState; set => reelState = value; }
    public bool FreeSpinsGame { get => freeSpinsGame; set => freeSpinsGame = value; }

    private void Awake()
    {
        stateChangesDictionary = new Dictionary<ReelStates, Action>
        {
            { ReelStates.ReadyForSpin, () => { buttonsView.DeactivateStopBtn(); buttonsView.ActivatePlayBtn(); } },
            { ReelStates.StartSpin, buttonsView.DeactivatePlayBtn},
            { ReelStates.Spin, buttonsView.SetStopBtnInteractable},
            { ReelStates.SlowDown, buttonsView.SetStopBtnNonInteractable},
            { ReelStates.ForceStop, buttonsView.SetStopBtnNonInteractable},
            { ReelStates.ResultShowing, buttonsView.SetStopBtnInteractable}
        };
    }

    void OnEnable()
    {
        MovingReels.OnStateChanged += ChangeStateAndBtns;
        WinLinesChecker.OnStateChanged += ChangeStateAndBtns;
    }
    void OnDisable()
    {
        MovingReels.OnStateChanged -= ChangeStateAndBtns;
        WinLinesChecker.OnStateChanged -= ChangeStateAndBtns;
    }

    private void ChangeStateAndBtns(ReelStates state)
    {
        reelState = state;
        stateChangesDictionary[state]();
    }
    public void ResultShowing()
    {
        reelState = ReelStates.ResultShowing;
        buttonsView.SetStopBtnInteractable();
    }

}
