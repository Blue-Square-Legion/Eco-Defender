using UnityEngine.Events;

public class UsableEvent : BaseUsable
{
    public UnityEvent OnUse;

    public override void Use()
    {
        OnUse.Invoke();
    }
}
