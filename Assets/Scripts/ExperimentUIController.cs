using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ExperimentUIController : MonoBehaviour
{
    public static ExperimentUIController current;
    private UIInterface uiInterface;
    private LevelManager levelManager;
    [SerializeField] private Menu menu;
    [SerializeField] private Text challengeTitle;
    [SerializeField] private Text success;
    [SerializeField] private Text hint;
    [SerializeField] private GameObject changeweight;
    [SerializeField] private GameObject changelength;
    [SerializeField] private GameObject changeairresistance;
    [SerializeField] private GameObject nextlevel;



    public void Start()
    {
        if (!menu)
            Debug.LogError("No Menu has been provided to UI-control. Assign a Menu-reference ASAP. Come on, " + Environment.UserName + ", you can do this");
        current = this;
        levelManager = LevelManager.current;
    }

    public void SetInterface(UIInterface uiInterface)
    {
        this.uiInterface = uiInterface;
    }

    public void LoadLevelUI(Level level)
    {
        challengeTitle.text = level.challenge;
        success.text= level.successMessage;
        hint.text = level.hint;
        ToggleElement(changeweight, level.canAdjustWeight);
        ToggleElement(changelength, level.canAdjustLength);
        ToggleElement(changeairresistance, level.canControlAirResistance);

    }

    public void RemoveElementsReplaceObject()
    {
        ToggleElement(changeweight, false);
        ToggleElement(changelength, false);
        ToggleElement(changeairresistance, false);
    }

    public void AddElementsPlaceObject()
    {
        Level level = levelManager.GetLevel();
        ToggleElement(changeweight, level.canAdjustWeight);
        ToggleElement(changelength, level.canAdjustLength);
        ToggleElement(changeairresistance, level.canControlAirResistance);
    }

    public void ToggleElement(GameObject element, bool active)
    {
        if (!active)
            StartCoroutine(DeactivateUIElement(element));
        else
            StartCoroutine(ActivateUIElement(element));
    }

    private IEnumerator ActivateUIElement(GameObject element)
    {
        element.SetActive(true);
        //Room for animation
        yield break;
    }

    private IEnumerator DeactivateUIElement(GameObject element)
    {
        element.SetActive(false);
        //Room for animation
        yield break;
    }

    public void DisplayLevelInfo()
    {

    }

    public void DisplayTip()
    {

    }

    public void DisplayLevelSwitch()
    {

    }
    public void ClearLevel ()
    {
       nextlevel.SetActive(true);
    }
    public void NextLevel()
    {
      nextlevel.SetActive(false);
      GeneralEventHandler.current.GoToNextLevel();
    }

}
