using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanSpeedBoost : PacmanBehavior
{
    public override string Name { get;}  = "Speed";

    private float speedMultiplier = 2f;
    private float initialSpeed;

    private void OnEnable() {
        initialSpeed = pacman.movement.speedMultiplier;
        pacman.movement.speedMultiplier = speedMultiplier;
    }
    private void OnDisable() {
        pacman.behaviors.Remove(this);
        pacman.movement.speedMultiplier = initialSpeed;
        pacman.EnableRandomBehavior();
        pacman.behaviors.Add(this);
    }  
}
