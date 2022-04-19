using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialStart : MonoBehaviour
{
    [SerializeField] private Button _yesStartTutorial;
    [SerializeField] private Button _noStartTutorial;

    private void Start()
    {
        if (PlayerPrefs.GetInt("HMTutorial") == 1)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnEnable()
    {
        _yesStartTutorial.onClick.AddListener(LoadTutorial);
        _noStartTutorial.onClick.AddListener(CloseTutorialWindow);

        if (PlayerPrefs.GetInt("HMTutorial") == 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _yesStartTutorial.onClick.RemoveAllListeners();
        _noStartTutorial.onClick.RemoveAllListeners();
    }

    private void LoadTutorial()
    {
        SceneManager.LoadScene(2);
    }

    private void CloseTutorialWindow()
    {
        gameObject.SetActive(false);
    }

}
