using UnityEngine;

public class ReelsStateController : MonoBehaviour
{

    [SerializeField] private ButtonsView buttonsView;
    [SerializeField] private MovingSymbols[] movingSymbols;
    private ReelStates reelState;
    private bool freeSpinsGame;

    internal ReelStates ReelState { get => reelState; set => reelState = value; }
    public bool FreeSpinsGame { get => freeSpinsGame; set => freeSpinsGame = value; }

    public void StartGame()
    {
        reelState = ReelStates.ReadyForSpin;
        if (freeSpinsGame)
        {
            return;
        }
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

    public void ResultShowing()
    {
        reelState = ReelStates.ResultShowing;
        buttonsView.SetStopBtnInteractable();
    }

    void Update()
    {
        foreach ( MovingSymbols movingSymbol in movingSymbols)
        {
            movingSymbol.ChangeSymbolAndSprite();
        }
    }
}
