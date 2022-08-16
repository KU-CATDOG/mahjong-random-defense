using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic Instruction", menuName = "ScriptableObjects/Relic Instruction", order = 1)]
public class RelicInstructionScriptable : ScriptableObject
{
    public string Name;
    public string OriginName;
    public string Info;
    public string Rank;

    public Sprite Image;
}
