using UnityEngine;

[CreateAssetMenu(fileName = "New Final Screen", menuName = "Final Screen", order = 53)]
public class FinalScreenData : ScriptableObject
{
    [SerializeField] private int[] finalSymbols;

    public int[] FinalSymbols  => finalSymbols; 
}
