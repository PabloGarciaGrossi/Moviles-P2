﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data", menuName = "ScriptableObjects/LevelGroup", order = 1)]
public class LevelPackage : ScriptableObject
{
    public TextAsset[] levels;

    public Color pathColor;

    public Color hintColor;

    public Color iceColor;

    public Sprite imgLevel;

    public string packName;
}
