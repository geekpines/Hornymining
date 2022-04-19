using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class TutorialController : MonoBehaviour
{
    private PlayerProfile _playerProfile;
    private bool _isOpenFlag = false;


    [Title("������")]
    [SerializeField] private Image Background;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _rouletteScreen;
    [SerializeField] private List<GameObject> _shopScreen;

    [Title("������� ��������")]
    [SerializeField] private DialogContainer _dialogContainer;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    [Title ("�������� �������")]
    [SerializeField] private GameObject _buttonHolder;
    [SerializeField] private Button _ruButton;
    [SerializeField] private Button _enButton;
    private string _key = "HMLanguage";
    private int _dialogueNumber = 0;
    private int _languageIndex = 0;

    [Title("���� ������")]
    [SerializeField] private Button _closeButton;
    [SerializeField] private List<Button> _shopButtons;
    [SerializeField] private Button _offZoneButton;


    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }

    void Start()
    {
        _ruButton.onClick.AddListener(RuLanguageSet);
        _enButton.onClick.AddListener(EnLanguageSet);

        _closeButton.onClick.AddListener(TutorialTextActiveController);
        _playerProfile.OnActiveMinersCountChanged += TutorialTextActiveController;


        foreach (var _shopButton in _shopButtons)
        {
            _shopButton.onClick.AddListener(TutorialTextActiveController);
        } 

        _offZoneButton.onClick.AddListener(TutorialTextActiveController);
    }

    private void RuLanguageSet()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        _languageIndex = 1;
        PlayerPrefs.SetInt(_key, _languageIndex);
        PlayerPrefs.Save();
        _buttonHolder.SetActive(false);
        _closeButton.gameObject.SetActive(true);
        NextDialogue();
    }
    
    private void EnLanguageSet()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        _languageIndex = 0;
        PlayerPrefs.SetInt(_key, _languageIndex);
        PlayerPrefs.Save();
        _buttonHolder.SetActive(false);
        _closeButton.gameObject.SetActive(true);
        NextDialogue();
    } 

    private void NextDialogue()
    {
        _dialogueText.text = _dialogContainer.dialogDataControllers[0].Dialogs[0].Dialog[_dialogueNumber].GetLocalizedString();
        GameScreenesController();
        _dialogueNumber++;
    }

    private void TutorialTextActiveController()
    {
        gameObject.SetActive(_isOpenFlag);
        _isOpenFlag = !_isOpenFlag;
        if (_isOpenFlag)
        {
            NextDialogue();
        }
    }

    private void TutorialTextActiveController(Miner miner)
    {
        gameObject.SetActive(_isOpenFlag);
        _isOpenFlag = !_isOpenFlag;
        if (_isOpenFlag)
        {
            NextDialogue();
        }
    }

    private void GameScreenesController()
    {
        switch (_dialogueNumber)
        {
            case 1:
                Background.color = new Color32(0, 0, 0, 100);
                _gameScreen.SetActive(true);
                break;
            case 2:
                _offZoneButton.gameObject.SetActive(true);
                _shopScreen[0].SetActive(true);
                break;
            case 3:
                _shopScreen[0].SetActive(false);
                _shopScreen[1].SetActive(true);
                break;
            case 4:
                _gameScreen.SetActive(false);
                _offZoneButton.gameObject.SetActive(false);
                _shopScreen[1].SetActive(false);
                _rouletteScreen.SetActive(true);
                _playerProfile.OnAllMinersCountChanged += TutorialTextActiveController;
                break;
            case 5:
                Background.color = new Color32(255, 255, 255, 255);
                _rouletteScreen.SetActive(false);
                gameObject.SetActive(true);
                EndingTutorial();

                break;
          
            default:
                break;
        }

        
    }

    private void ClearAllListeners()
    {
        _playerProfile.OnAllMinersCountChanged -= TutorialTextActiveController;
        _playerProfile.OnActiveMinersCountChanged -= TutorialTextActiveController;
        _ruButton.onClick.RemoveAllListeners();
        _enButton.onClick.RemoveAllListeners();
        _offZoneButton.onClick.RemoveAllListeners();

        foreach (var button in _shopButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void EndingTutorial()
    {        
        PlayerPrefs.DeleteAll();
        ClearAllListeners();
        PlayerPrefs.SetInt("HBTutorial", 1);
        PlayerPrefs.SetInt(_key, _languageIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    public void NextStep()
    {
        TutorialTextActiveController();
    }
}
