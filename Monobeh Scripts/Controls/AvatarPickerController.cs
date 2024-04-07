using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPickerController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Options;
    private int _currentOption;
    void Start()
    {
        CurrentOption = 0;
    }

    public int CurrentOption
    {
        set {
            if (value < 0 || value > 5) throw new ArgumentOutOfRangeException("currentoption", "Не существует аватара с заданным id.");
            Options[_currentOption].transform.GetChild(0).gameObject.SetActive(false);
            Options[value].transform.GetChild(0).gameObject.SetActive(true);
            _currentOption = value;
        }
        get
        {
            return _currentOption;
        }
    }
}
