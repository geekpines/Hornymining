using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WarningScreenController : MonoBehaviour
{
    [SerializeField] private Button _yesButton;

    void Start()
    {
        _yesButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        PlayerPrefs.DeleteAll();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

   
}
