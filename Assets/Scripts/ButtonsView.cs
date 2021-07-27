using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsView : MonoBehaviour
{
    [SerializeField] private Button playButton, stopButton;
    [SerializeField] private RectTransform playButtonRT, stopButtonRT;

    public void DeactivateStopBtn()
    {
        stopButton.interactable = false;
        stopButtonRT.localScale = Vector3.zero;
    }
    public void ActivatePlayBtn()
    {
        playButtonRT.localScale = Vector3.one;
        playButton.interactable = true;
    }
    public void DeactivatePlayBtn()
    {
        playButton.interactable = false;
        playButtonRT.localScale = Vector3.zero;
        stopButtonRT.localScale = Vector3.one;
    }
    public void SetStopBtnInteractable()
    {
        stopButton.interactable = true;
    }
    public void SetStopBtnNonInteractable()
    {
        stopButton.interactable = false;
    }
}
