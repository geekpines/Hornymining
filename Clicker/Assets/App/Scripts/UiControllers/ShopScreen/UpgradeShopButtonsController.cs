using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;


public class UpgradeShopButtonsController : MonoBehaviour
{
    private Button button;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private LocalizeStringEvent descriptionLine;
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(SetDescription);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDescription()
    {
         text.text = descriptionLine.StringReference.GetLocalizedString();
    }
}
