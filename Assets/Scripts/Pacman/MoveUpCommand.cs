using UnityEngine;

public class MoveUpCommand : ICommand
{
    private Pacman _pacMan;

    public MoveUpCommand(Pacman pacMan)
    {
        _pacMan = pacMan;
    }

    public void Execute()
    {
        _pacMan.MoveUp();
    }
}
