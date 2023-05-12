using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanNormal : PacmanBehavior
{
    public override string Name  { get;} = "Normal";
    // Start is called before the first frame update
    private void OnDisable() {
        pacman.behaviors.Remove(this);
        pacman.EnableRandomBehavior();
        pacman.behaviors.Add(this);
        pacman.normal.Enable();
    }
}
