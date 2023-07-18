using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AttachmentItem : ItemObject
{
    [BoxGroup("Attachment Info")]
    public AttachmentType attachmentType;
    [BoxGroup("Laser Info")]
    [ShowIf("attachmentType", AttachmentType.side)] public LaserType laserType;
}

public enum LaserType
{
    red,
    green,
    blue,
}
public enum AttachmentType
{
    scope,
    barrel,
    grip,
    frontGrip,
    side,
}

[System.Serializable]
public class AttachmentObject
{
    public AttachmentItem attachmentItem;
    public GameObject attachmentObject;
}