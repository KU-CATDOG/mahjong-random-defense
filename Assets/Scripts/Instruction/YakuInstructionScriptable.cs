using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Yaku Instruction", menuName = "ScriptableObjects/Yaku Instruction", order = 1)]
public class YakuInstructionScriptable : ScriptableObject
{
    public string Name;
    public string Condition;

    public SpriteList[] Image;
}
[Serializable]
public class SpriteList
{
    public Sprite[] sprite;
    public enum GroundType { Triple, Complete, Yakuman }
    public GroundType groundtype;
}
