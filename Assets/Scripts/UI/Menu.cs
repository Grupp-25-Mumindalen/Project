using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject challengemenu;
    [SerializeField] private GameObject challenge1;
    [SerializeField] private GameObject exitconfirmation;

    public void ToggleObject(GameObject element){
        if(!element.activeSelf){
        element.SetActive(true);
        }
        else {
          element.SetActive(false);
        }
    }
    public void showAllChallenges(){
      challengemenu.SetActive(true);
      challenge1.SetActive(false);
    }
    public void closeAllChallenges(){
      challengemenu.SetActive(false);
      challenge1.SetActive(true);
    }
    public void showCurrentChallenge(){
      challenge1.SetActive(true);
    }
    public void openexitmenu(){
      exitconfirmation.SetActive(true);
    }
    public void exitgame(){

    }
    public void continuegame(){
      exitconfirmation.SetActive(false);
    }
}
