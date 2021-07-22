using UnityEngine;
using UnityEngine.UI;

public class BtnsBehaviour : MonoBehaviour
{
    [SerializeField] private Button playButton, stopButton;
    [SerializeField] private RectTransform playButtonRT, stopButtonRT;
    [SerializeField] private MovingSymbols movingSymbolsLastReel;
    void Start()
    {
        //stopButton.interactable = false;
        //stopButtonRT.localScale = Vector3.zero;
    }

    void Update()
    {
        //var reelState = movingSymbolsLastReel.ReelState;
        //if ( reelState == ReelState.StartSpin)
        //{
        //    playButton.interactable = false;
        //    playButtonRT.localScale = Vector3.zero;
        //    stopButtonRT.localScale = Vector3.one;
        //}
        //if (reelState == ReelState.Spin)
        //{
        //    stopButton.interactable = true;
        //}
        //if (reelState == ReelState.SlowDown)
        //{
        //    stopButton.interactable = false;
        //}
        //if (reelState == ReelState.Stop)
        //{
        //    stopButtonRT.localScale = Vector3.zero;
        //    playButtonRT.localScale = Vector3.one;
        //    playButton.interactable = true;
        //}
        //if (reelState == ReelState.ForceStop)
        //{
        //    stopButton.interactable = false;
        //}
    }
}
