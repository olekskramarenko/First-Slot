using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioSource freeSpinsAudio;
    [SerializeField] private AudioSource buttonClickAudio;
    [SerializeField] private AudioSource reelChangedAudio;
    [SerializeField] private AudioSource prizeCounterAudio;
    [SerializeField] private AudioSource scatterAudio;
    [SerializeField] private AudioSource reelStoppedAudio;
    [SerializeField] private AudioSource prizeAudio;
    [SerializeField] private AudioSource anticipationAudio;
    [SerializeField] private Button playButton;
    [SerializeField] private Button stopButton;

    private Dictionary<SoundType, AudioSource> audioChangesDictionary;

    private void Awake()
    {
        audioChangesDictionary = new Dictionary<SoundType, AudioSource>
        {
            { SoundType.background, backgroundAudio },
            { SoundType.freeSpins, freeSpinsAudio },
            { SoundType.buttonClick, buttonClickAudio },
            { SoundType.reelChanged, reelChangedAudio },
            { SoundType.prizeCounter, prizeCounterAudio },
            { SoundType.scatter, scatterAudio },
            { SoundType.reelStop, reelStoppedAudio },
            { SoundType.prize, prizeAudio },
            { SoundType.anticipation, anticipationAudio }
        };
    }

    void OnEnable()
    {
        PrizeAnimator.OnSoundPLayed += PlaySound;
        MovingSymbols.OnSoundPLayed += PlaySound;
        MovingReels.OnSoundPLayed += PlaySound;
        FreeSpinsController.OnSoundPLayed += PlaySound;
        FreeSpinsController.OnSoundStopped += StopSound;
        ResultPopUp.OnSoundPLayed += PlaySound;
        ResultPopUp.OnSoundStopped += StopSound;
        AnticipationController.OnSoundPLayed += PlaySound;
        AnticipationController.OnSoundStopped += StopSound;

        playButton.onClick.AddListener( () => PlaySound(SoundType.buttonClick) );
        stopButton.onClick.AddListener( () => PlaySound(SoundType.buttonClick) );
    }


    private void PlaySound(SoundType sound)
    {
        audioChangesDictionary[sound].Play();
    }

    private void StopSound(SoundType sound)
    {
        audioChangesDictionary[sound].Stop();
    }


}
