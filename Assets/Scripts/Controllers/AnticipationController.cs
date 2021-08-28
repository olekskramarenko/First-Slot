using UnityEngine;
using DG.Tweening;

public class AnticipationController : MonoBehaviour
{
    [SerializeField] private RectTransform antisipationRT;
    private float animationTime = 0.2f;

    public delegate void PlaySoundEvent(SoundType sound);
    public static event PlaySoundEvent OnSoundPLayed;

    public delegate void StopSoundEvent(SoundType sound);
    public static event StopSoundEvent OnSoundStopped;

    public void PlayLongSpinAnimation()
    {
        if (OnSoundPLayed != null) OnSoundPLayed(SoundType.anticipation);
        antisipationRT.DOScale(1, animationTime);
    }

    public void StopLonSpinAnimation()
    {
        if (OnSoundPLayed != null) OnSoundStopped(SoundType.anticipation);
        antisipationRT.DOScale(0, animationTime);
    }

}
