using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    public string objectName;
    public TMP_Text objectNameText;
    public bool selected;
    public GameObject selectedObject;
    void Start()
    {
        objectNameText.text = objectName;
    }
    private void Update() {
        selectedObject.SetActive(selected);
    }
}
