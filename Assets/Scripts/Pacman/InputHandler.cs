using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private Pacman _pacMan;

    private ICommand _moveUp;
    private ICommand _moveDown;
    private ICommand _moveLeft;
    private ICommand _moveRight;

    public InputHandler(Pacman pacMan)
    {
        _pacMan = pacMan;
        _moveUp = new MoveUpCommand(pacMan);
        _moveDown = new MoveDownCommand(pacMan);
        _moveLeft = new MoveLeftCommand(pacMan);
        _moveRight = new MoveRightCommand(pacMan);
    }

    public void HandleInput()
    {
        if (_pacMan.IsInvertControlEnabled())
        {
            // Swap the up/down and left/right commands when invertControl is enabled
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                _moveDown.Execute();
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                _moveUp.Execute();
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                _moveRight.Execute();
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                _moveLeft.Execute();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                _moveUp.Execute();
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                _moveDown.Execute();
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                _moveLeft.Execute();
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                _moveRight.Execute();
        }
    }
}

