
using UnityEngine;

public class MoveLeftCommand : ICommand
{
    private Pacman _pacMan;

    public MoveLeftCommand(Pacman pacMan)
    {
        _pacMan = pacMan;
    }

    public void Execute()
    {
        _pacMan.MoveLeft();
    }
}
