using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ArmourItem : ItemObject
{
    [BoxGroup("Armour Info")]
    public int armourValue;
    [BoxGroup("Armour Info")]
    public int radiationProtection;
    [BoxGroup("Armour Info")]
    public int extraSlots;
}
