using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class StartLanguage : MonoBehaviour
{
    [SerializeField] private Button _ru;
    [SerializeField] private Button _en;
    void Start()
    {
        _ru.onClick.AddListener(SetRu);
        _en.onClick.AddListener(SetEn);
    }
    private void SetRu()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        Destroy(gameObject);
    }

    private void SetEn()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }
    
}
