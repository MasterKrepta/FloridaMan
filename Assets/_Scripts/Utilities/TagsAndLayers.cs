using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagsAndLayers : MonoBehaviour
{
    public const  string LEFTPUNCH = "LeftPunch";
    public const string RIGHTPUNCH = "RightPunch";
    public const string KICKING = "Kick";
    public const string STABBING = "Stabbing";
    public const string MOVING = "Moving";

    public class Tags
    {
        public const string Player = "Player";
        public const string Enemy = "Enemy";
    }

    public class Layers
    {
        public const string Player = "Player";
        public const string Enemy = "Enemies";
    }
}
