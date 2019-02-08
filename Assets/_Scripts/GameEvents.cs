using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action<Unit> OnUnitHit = delegate { };

    public static Action OnPlayerHit = delegate { };

    //public static Action<Unit> OnGooseHit = delegate { };
    public static Action<Unit> OnGooseHit = delegate { };
    public static Action<Unit> OnKnockBack = delegate { };
    public static Action<Unit> OnGooseDied = delegate { };

}
