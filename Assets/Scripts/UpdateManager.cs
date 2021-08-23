using System.Collections.Generic;
using UnityEngine;

public interface IUpdatable
{
    void GiveToUpdate();
}
public class UpdateManager : MonoBehaviour
{
    private static List<IUpdatable> updatesList = new List<IUpdatable>();

    public static void Register(IUpdatable obj)
    {
        if (obj == null) throw new System.ArgumentNullException();

        updatesList.Add(obj);
    }
    public static void Unregister(IUpdatable obj)
    {
        if (obj == null) throw new System.ArgumentNullException();

        updatesList.Remove(obj);
    }

    void Update()
    {
        foreach (IUpdatable obj in updatesList)
        {
            obj.GiveToUpdate();
        }
    }
}
