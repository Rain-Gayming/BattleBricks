using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public List<Menu> menus;

    private void Awake() {
        instance = this;
    }
    public void OpenMenu(string menu)
    {
        for (int i = 0; i < menus.Count; i++)
        {
            if(menus[i].menuName == menu){
                menus[i].open = true;
            }else{
                menus[i].open = false;
            }
        }
    }
}
