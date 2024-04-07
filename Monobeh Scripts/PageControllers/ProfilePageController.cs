using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System;
using FamilyOrganizer;

public class ProfilePageController : MonoBehaviour
{

    public InputField NameHolder;
    public Text BirthdayHolder;
    public AvatarPickerController avatarPickerController;
    public GameObject nameWarning, birthdayWarning, avatarWarning;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void DisableWarnings()
    {
        nameWarning.SetActive(false);
        birthdayWarning.SetActive(false);
        avatarWarning.SetActive(false);
    }


    public void ChangeName()
    {
        try
        {
            Config.MainPerson.Name = NameHolder.text;
            nameWarning.SetActive(false);
        }
        catch
        {
            nameWarning.SetActive(true);
        }
    }

    public void ChangeAvatar(int avatarID)
    {
        try
        {
            Config.MainPerson.AvatarID = avatarID;
            avatarPickerController.CurrentOption = avatarID;
            avatarWarning.SetActive(false);
        }
        catch
        {
            avatarWarning.SetActive(true) ;
        }
    }

    public void ChangeBirthdate(DateTime birthdate)
    {
        try
        {
            Config.MainPerson.BirthDate = birthdate;
            BirthdayHolder.text =  DateConverter.RepresentDate(birthdate);
            birthdayWarning.SetActive(false);
        }
        catch
        {
            birthdayWarning.SetActive(true);
        }
    }

    public void UpdatePage()
    {
        DisableWarnings();
        NameHolder.text = Config.MainPerson.Name;
        BirthdayHolder.text =  DateConverter.RepresentDate(Config.MainPerson.BirthDate);
        avatarPickerController.CurrentOption = Config.MainPerson.AvatarID;
    }

}
