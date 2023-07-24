using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAfterLoad : MonoBehaviour
{
    public GameObject gameObjectToLoad;
    void Start()
    {
        StartCoroutine(LoadCo());
    }

    public IEnumerator LoadCo()
    {
        yield return new WaitForSeconds(0.05f);
        gameObjectToLoad.SetActive(false);
    }
}
