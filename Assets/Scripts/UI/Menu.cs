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
    [SerializeField] private GameObject showHints;

    public void openMenu(){
        if(!menuPanel.activeSelf){
          menuPanel.SetActive(true);
        }
        else {
          menuPanel.SetActive(false);
        }
    }
    public void showHint(){
        if(!showHints.activeSelf){
          showHints.SetActive(true);
        }
        else {
          showHints.SetActive(false);
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
