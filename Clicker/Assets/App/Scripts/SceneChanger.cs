using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loader());
    }

    private IEnumerator Loader()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main");
    }
}
