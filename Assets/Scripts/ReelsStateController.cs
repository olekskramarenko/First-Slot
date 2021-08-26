using UnityEngine;

public class ReelsStateController : MonoBehaviour
{

    [SerializeField] private ButtonsView buttonsView;
    [SerializeField] private MovingSymbols[] movingSymbols;
    private ReelStates reelState;
    private bool freeSpinsGame;

    internal ReelStates ReelState { get => reelState; set => reelState = value; }
    public bool FreeSpinsGame { get => freeSpinsGame; set => freeSpinsGame = value; }

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


    public void ResultShowing()
    {
        reelState = ReelStates.ResultShowing;
        buttonsView.SetStopBtnInteractable();
    }


    private void ChangeStateAndBtns(ReelStates state)
    {
        reelState = state;
        if (state == ReelStates.ReadyForSpin)
        {
            buttonsView.DeactivateStopBtn();
            buttonsView.ActivatePlayBtn();
        }
        else if (state == ReelStates.StartSpin)
        {
            buttonsView.DeactivatePlayBtn();
        }
        else if (state == ReelStates.Spin | state == ReelStates.ResultShowing)
        {
            buttonsView.SetStopBtnInteractable();
        }
        else if (state == ReelStates.SlowDown)
        {
            buttonsView.SetStopBtnNonInteractable();
        }
        else if (state == ReelStates.ForceStop)
        {
            buttonsView.SetStopBtnNonInteractable();
        }
    }
}
