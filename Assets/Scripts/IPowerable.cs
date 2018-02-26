using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerable
{
    SourceTag SourceTag { get; }
    MainPowerStatus MPS { get; }
    PowerSource PowerSource { get; }
    int requiredPower { get; }
    bool isPowered { get; }

    void PowerUp();
    void PowerDown();
}
