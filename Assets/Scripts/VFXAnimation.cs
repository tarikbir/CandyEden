using UnityEngine;

public class VFXAnimation : MonoBehaviour
{
    public void AlertObservers(string message)
    {
        if (message.Equals("Ended"))
        {
            Destroy(gameObject);
        }
    }
}