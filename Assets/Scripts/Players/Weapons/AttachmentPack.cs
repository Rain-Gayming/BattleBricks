using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attachment Pack")]
public class AttachmentPack : ScriptableObject
{
    public List<AttachmentItem> attachments;
}
