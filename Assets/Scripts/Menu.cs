using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public virtual void Toggle()
    {
        Toggle(!gameObject.activeInHierarchy);
    }

    public virtual void Toggle(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
