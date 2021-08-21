using UnityEngine;

[CreateAssetMenu(fileName = "New WinLine", menuName = "Win Line", order = 54)]
public class WinLinesData : ScriptableObject
{
    [SerializeField] private int[] winLine;

    public int[] WinLine => winLine;
}
