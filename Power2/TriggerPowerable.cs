﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPowerable : PowerableObj2
{
    /// <summary>
    /// derived class from PowerableObj that can be picked up by player.
    /// Could add additional condition to virtual functions PowerUp() and PowerDown()
    /// for additional functionalities (i.e. visual effect)
    /// </summary>
    private void Awake()
    {
        IsPowered = true; // powered initially untill player picks it up
        PowerSource = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerSource>(); // hooks it up to Player's PowerSource
    }

    /// <summary>
    /// example of how to pick up and draw power.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            this.PowerDown(); // only goes through when PowerSource has enough power and this object is turned on.
    }
}
