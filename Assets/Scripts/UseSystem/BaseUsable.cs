using UnityEngine;

public class BaseUsable : MonoBehaviour, IUsable
{
    public virtual void AltUse() { }

    public virtual void Use() { }
}
