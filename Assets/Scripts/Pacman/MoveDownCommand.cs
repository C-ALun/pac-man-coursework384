
using UnityEngine;

public class MoveDownCommand : ICommand
{
    // Start is called before the first frame update
    private Pacman _pacMan;

    public MoveDownCommand(Pacman pacMan)
    {
        _pacMan = pacMan;
    }

    public void Execute()
    {
        _pacMan.MoveDown();
    }
}
