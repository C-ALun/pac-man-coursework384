
using UnityEngine;

public class MoveRightCommand : ICommand
{
    private Pacman _pacMan;

    public MoveRightCommand(Pacman pacMan)
    {
        _pacMan = pacMan;
    }

    public void Execute()
    {
        _pacMan.MoveRight();
    }
}
