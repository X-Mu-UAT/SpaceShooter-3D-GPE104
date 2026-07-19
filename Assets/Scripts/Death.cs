using UnityEngine;

public class Death : MonoBehaviour
{
    public virtual void DoDeath()
    {
        Destroy(gameObject);
    }
}