using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanController : MonoBehaviour
{
    private Pacman _pacMan;
    private InputHandler _inputHandler;

    void Start()
    {
        _pacMan = GetComponent<Pacman>();
        _inputHandler = new InputHandler(_pacMan);
    }

    void Update()
    {
        _inputHandler.HandleInput();

        if (!FindObjectOfType<GameManager>().isRewinding)
        {
        // Rotate pacman to face the movement direction
            float angle = Mathf.Atan2(_pacMan.movement.direction.y, _pacMan.movement.direction.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }
    }
}

