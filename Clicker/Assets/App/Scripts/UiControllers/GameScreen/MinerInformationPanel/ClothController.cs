using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothController : MonoBehaviour
{
    [SerializeField] private Button clothOn;
    [SerializeField] private Button clothOff;

    public event Action OnClothOn;
    public event Action OnClothOff;

    public void OnEnable()
    {
        clothOff.onClick.AddListener(ClothOff);
        clothOn.onClick.AddListener(ClothOn);
    }

    private void OnDisable()
    {
        clothOff.onClick.RemoveListener(ClothOff);
        clothOn.onClick.RemoveListener(ClothOn);
    }


    public void ClothOff()
    {
        OnClothOff?.Invoke();
    }


    public void ClothOn()
    {
        OnClothOn?.Invoke();
        
    }
}
