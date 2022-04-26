using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialStart : MonoBehaviour
{
    [SerializeField] private Button _yesStartTutorial;
    [SerializeField] private Button _noStartTutorial;
    [SerializeField] private GameObject _startTutorial;

    private void Start()
    {
        if (PlayerPrefs.GetInt("HBTutorial") == 1)
        {
            _startTutorial.SetActive(false);
        }
        else _startTutorial.SetActive(true);
    }


    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("HBTutorial") == 1)
        {
            _startTutorial.SetActive(false);
        }
        else _startTutorial.SetActive(true);

        _yesStartTutorial.onClick.AddListener(LoadTutorial);
        _noStartTutorial.onClick.AddListener(CloseTutorialWindow);
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
        PlayerPrefs.SetInt("HBTutorial", 1);
    }

}
