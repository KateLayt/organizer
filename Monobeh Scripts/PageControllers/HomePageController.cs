using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePageController : MonoBehaviour
{
    public TaskPlateDisplayController UnfinishedDisplayController;
    public TaskPlateDisplayController FinishedDisplayController;
    public Image Avatar;
    public RectTransform Layout;
    public Sprite[] AvatarOptions;
    public Text NameHolder;
    void Start()
    {
        UpdatePage();
    }

    private void Update()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(Layout);
    }

    // Update is called once per frame
    public void UpdatePage()
    {
        Avatar.sprite = AvatarOptions[Config.MainPerson.AvatarID];

        NameHolder.text = Config.MainPerson.Name;

        UnfinishedDisplayController.RepresentedGroup = Config.GetUnfinishedTasks();
        FinishedDisplayController.RepresentedGroup = Config.GetFinishedTasks();
        UnfinishedDisplayController.UpdatePlates();
        FinishedDisplayController.UpdatePlates();

        LayoutRebuilder.ForceRebuildLayoutImmediate(Layout);
    }
}
