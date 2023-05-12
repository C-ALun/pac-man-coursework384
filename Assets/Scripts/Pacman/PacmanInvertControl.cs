using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanInvertControl : PacmanBehavior
{   
    public override string Name { get;} = "Invert";

    private void OnDisable() {
        pacman.behaviors.Remove(this);
        pacman.EnableRandomBehavior();
        pacman.behaviors.Add(this);
    }

}
