using UnityEngine;
using UnityEngine.UI;

public class PopUpsController : MonoBehaviour
{
    [SerializeField] private Text spinsLeftText;
    [SerializeField] private StartPopUp startPopUp;
    [SerializeField] private ResultPopUp resultPopUp;
    [SerializeField] private SpinsCounterPopUp spinsCounterPopUp;

    public Text CounterText { get => spinsLeftText; set => spinsLeftText = value; }

    public delegate void PlaySoundEvent(SoundType sound);
    public static event PlaySoundEvent OnSoundPLayed;

    public delegate void StopSoundEvent(SoundType sound);
    public static event StopSoundEvent OnSoundStopped;

    public void ShowFreeSpinsStart()
    {
        startPopUp.ShowPopUp();
    }

    public void ShowSpinsCounter()
    {
        spinsCounterPopUp.ShowPopUp();
    }
    public void CloseSpinsCounter()
    {
        spinsCounterPopUp.ClosePopUp();
    }

    public void ShowAndCloseResultPopUp()
    {
        resultPopUp.ShowPopUp();
    }
}
