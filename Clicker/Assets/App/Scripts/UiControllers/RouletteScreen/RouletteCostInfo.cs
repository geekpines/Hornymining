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
            case 1: return 3;
            case 2: return 7;
            case 3: return 10;
            case 4: return 15;

            default:
                return 0;
        }
    }
}
