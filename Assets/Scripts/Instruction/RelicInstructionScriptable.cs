using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic Instruction", menuName = "ScriptableObjects/Relic Instruction", order = 1)]
public class RelicInstructionScriptable : ScriptableObject
{
    public string Name;
    public string Info;
    public int cost;

    public Sprite Image;
}
