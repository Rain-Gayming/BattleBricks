using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIUtilities : MonoBehaviour
{
    public List<GameObject> buttonTabs;
    public List<GameObject> menus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleGameObject(GameObject obj)
    {
        obj.SetActive(!obj.activeInHierarchy);
    }
    public void ChangeButtonTab(GameObject obj)
    {
        for (int i = 0; i < buttonTabs.Count; i++)
        {
            buttonTabs[i].SetActive(false);
        }
        obj.SetActive(true);
    }
    public void ChangeMenu(GameObject obj)
    {
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].SetActive(false);
        }
        obj.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
