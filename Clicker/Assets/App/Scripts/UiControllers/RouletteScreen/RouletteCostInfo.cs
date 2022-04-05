using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RouletteCostInfo : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _spinCost;

    private string spinKey = "HMSpins";
    private void OnEnable()
    {
        int spin = PlayerPrefs.GetInt(spinKey);
        _spinCost.text = ShowSpinCost(spin) + " Horny Bucks";
    }

    

    private int ShowSpinCost(int spin)
    {
        switch (spin)
        {
            case 0: return 1;
            case 1: return 15;
            case 2: return 50;
            case 3: return 100;
            case 4: return 300;

            default:
                return 0;
        }
    }
}
