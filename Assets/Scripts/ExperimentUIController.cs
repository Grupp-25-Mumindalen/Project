using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ExperimentUIController : MonoBehaviour
{
    public static ExperimentUIController current;
    private UIInterface uiInterface;
    [SerializeField] private Menu menu;
    [SerializeField] private Text challengeTitle;



    public void Start()
    {
        if (!menu)
            Debug.LogError("No Menu has been provided to UI-control. Assign a Menu-reference ASAP. Come on, " + Environment.UserName + ", you can do this");
        current = this;
    }

    public void SetInterface(UIInterface uiInterface)
    {
        this.uiInterface = uiInterface;
    }

    public void LoadLevelUI(Level level)
    {
        challengeTitle.text = level.name;
        throw new System.Exception("not implemented");

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
}
