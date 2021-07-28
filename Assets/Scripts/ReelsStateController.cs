using UnityEngine;

public class ReelsStateController : MonoBehaviour
{
    private ReelStates reelState;
    [SerializeField] private ButtonsView buttonsView;
    [SerializeField] private MovingSymbols[] movingSymbols;

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
    public void ForceStopped()
    {
        reelState = ReelStates.ForceStop;
        buttonsView.SetStopBtnNonInteractable();
    }

    void Update()
    {
        foreach ( MovingSymbols movingSymbol in movingSymbols)
        {
            movingSymbol.ChangeSymbolAndSprite();
        }
    }
}
