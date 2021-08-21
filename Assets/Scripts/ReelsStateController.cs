using UnityEngine;

public class ReelsStateController : MonoBehaviour
{
    private ReelStates reelState;
    [SerializeField] private ButtonsView buttonsView;
    [SerializeField] private MovingSymbols[] movingSymbols;

    internal ReelStates ReelState { get => reelState; set => reelState = value; }

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
        if (state == ReelStates.ReadyForSpin)
        {
            buttonsView.DeactivateStopBtn();
            buttonsView.ActivatePlayBtn();
        }
        else if (state == ReelStates.StartSpin)
        {
            buttonsView.DeactivatePlayBtn();
        }
        else if (state == ReelStates.Spin)
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
