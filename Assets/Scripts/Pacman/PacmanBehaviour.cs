using UnityEngine;

[RequireComponent(typeof(Pacman))]
public abstract class PacmanBehavior : MonoBehaviour
{
    public Pacman pacman { get; private set; }
    public float duration;

    public abstract string Name { get; }

    private void Awake()
    {
        pacman = GetComponent<Pacman>();
    }

    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;

        CancelInvoke();
    }


}
