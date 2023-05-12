using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{

    public int numberOfGhostsEaten { get; private set; } = 0;

    public AnimatedSprite deathSequence;
    public SpriteRenderer spriteRenderer { get; private set; }
    public new Collider2D collider { get; private set; }
    public Movement movement { get; private set; }
    
    public PacmanNormal normal { get; private set; }
    public PacmanInvertControl invertControl { get; private set; }
    public PacmanSpeedBoost speedBoost { get; private set; }

    public List<PacmanBehavior> behaviors = new List<PacmanBehavior>();

    public string currentMode { get; private set;}
    public PacmanController _pacmanController { get; private set; }



    private void Start() {
       ResetState();
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        movement = GetComponent<Movement>();

        normal = GetComponent<PacmanNormal>();
        invertControl = GetComponent<PacmanInvertControl>();
        speedBoost = GetComponent<PacmanSpeedBoost>();

        behaviors.Add(invertControl);
        behaviors.Add(speedBoost);
        behaviors.Add(normal);
    }


    public void MoveUp()
    {
        movement.SetDirection(Vector2.up);
    }

    public void MoveDown()
    {
        movement.SetDirection(Vector2.down);
    }

    public void MoveLeft()
    {
        movement.SetDirection(Vector2.left);
    }

    public void MoveRight()
    {
        movement.SetDirection(Vector2.right);
    }

    public bool IsInvertControlEnabled()
    {
        return invertControl.enabled;
    }

    public void IncrementGhostsEaten()
    {
        numberOfGhostsEaten++;
    }

    public void EnableRandomBehavior()
    {
        int randomIndex = Random.Range(0, behaviors.Count);

        behaviors[randomIndex].Enable();
        currentMode = behaviors[randomIndex].Name;
        FindObjectOfType<GameManager>().SetPacmanBehaviourText(currentMode);
    }


    public void ResetState()
    {

        normal.Enable();
        invertControl.Disable();
        speedBoost.Disable();
        currentMode = normal.Name;
        FindObjectOfType<GameManager>().SetPacmanBehaviourText(currentMode);
        
        numberOfGhostsEaten = 0;
        enabled = true;
        spriteRenderer.enabled = true;
        collider.enabled = true;
        deathSequence.enabled = false;
        deathSequence.spriteRenderer.enabled = false;
        movement.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        collider.enabled = false;
        movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.spriteRenderer.enabled = true;

        normal.Enable();
        invertControl.Disable();
        speedBoost.Disable();
        currentMode = normal.Name;

        deathSequence.Restart();
    }

}
