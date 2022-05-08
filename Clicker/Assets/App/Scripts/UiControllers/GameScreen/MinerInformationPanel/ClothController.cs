using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothController : MonoBehaviour
{
    [SerializeField] private List<Button> clothButtons;
    

    public event Action<int> OnCloth;

    public void OnEnable()
    {
        
            clothButtons[0].onClick.AddListener(delegate { ClothOn(0); });
            clothButtons[1].onClick.AddListener(delegate { ClothOn(1); });
            clothButtons[2].onClick.AddListener(delegate { ClothOn(2); });
            clothButtons[3].onClick.AddListener(delegate { ClothOn(3); });
            clothButtons[4].onClick.AddListener(delegate { ClothOn(4); });
            clothButtons[5].onClick.AddListener(delegate { ClothOn(5); });
        
        
    }

    private void OnDisable()
    {
        foreach (var button in clothButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }



    public void ClothOn(int i)
    {
        OnCloth?.Invoke(i);
    }
}
