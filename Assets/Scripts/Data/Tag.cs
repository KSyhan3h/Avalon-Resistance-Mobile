﻿using UnityEngine;

namespace AvalonResistance
{
    public abstract class Tag : ScriptableObject
    {
        public new string name;
        public string description;
        public Sprite sprite;
    }
}