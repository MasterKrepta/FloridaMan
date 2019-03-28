using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action<Unit> OnUnitHit = delegate { };

    public static Action<IDamaggable> OnPlayerHit = delegate { };

    //public static Action<Unit> OnGooseHit = delegate { };
    public static Action<IDamaggable> OnGooseHit = delegate { };
    public static Action<Unit> OnKnockBack = delegate { };
    public static Action<Unit> OnGooseDied = delegate { };
    public static Action OnPlayerHealthChange = delegate { };
    public static Action OnPlayerDied = delegate { };
    public static Action OnPlayerRespawn = delegate { };
    public static Action OnPlayerAlive = delegate { };

    public static Action OnEffectMode_ON = delegate { };
    public static Action OnEffectMode_OFF = delegate { };

}
