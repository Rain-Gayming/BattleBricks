using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text accountNameText;

    // Start is called before the first frame update
    void Start()
    {
        accountNameText.text = UserManager.instance.userInfo.displayName;
    }
}
