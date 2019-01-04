using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action<Unit> OnUnitHit;

    public static Action OnPlayerHit;

    public static Action<Unit, float> OnGooseHit;
    public static Action<Unit> OnGooseDied;
}
