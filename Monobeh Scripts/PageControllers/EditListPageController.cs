using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FamilyOrganizer;
using System;


public class EditListPageController : MonoBehaviour
{
    // Start is called before the first frame update
    public TaskGroup RepresentedGroup;
    public InputField NameHolder, DescriptionHolder;
    public Text NameWarning, DescriptionWarning;
    public ListSubpageController SubpageController;
    void Start()
    {
    }

    // Update is called once per frame
    public void UpdatePage()
    {
        if (RepresentedGroup == null)
        {
            Debug.Log("У страницы пока нет назначенной группы.");
            return;
        }

        NameHolder.text = RepresentedGroup.GroupName;
        DescriptionHolder.text = RepresentedGroup.Description;
    }

    public void ChangeName()
    {
        NameWarning.gameObject.SetActive(false);
        try { RepresentedGroup.GroupName = NameHolder.text; }
        catch(ArgumentException e)
        {
            NameWarning.gameObject.SetActive(true);
            NameWarning.text = e.Message;
        }
        catch (Exception e) {  Debug.LogException(e); }
    }

    public void ChangeDescription()
    {
        DescriptionWarning.gameObject.SetActive(false);
        try { RepresentedGroup.Description = DescriptionHolder.text; }
        catch (ArgumentException e)
        {
            DescriptionWarning.gameObject.SetActive(true);
            DescriptionWarning.text = e.Message;
        }
        catch (Exception e) { Debug.LogException(e); }
    }

    public void GoBack()
    {
        PageController.OpenSublist(RepresentedGroup);
    }
}
