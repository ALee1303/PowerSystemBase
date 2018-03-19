using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerable
{
    PowerSource PowerSource { get; }
    int RequiredPower { get; }
    bool IsPowered { get; }

    void PowerUp();
    void PowerDown();
}
