using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private string key = "HMLanguage";
    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate { ChangeLanguage(); });
    }

    private void ChangeLanguage()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[dropdown.value]; 
    }

    private void OnApplicationQuit()
    {
        
    }
}
