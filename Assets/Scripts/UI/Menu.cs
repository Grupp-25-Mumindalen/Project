using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{


    public GameObject menuPanel;
    public GameObject challengemenu;
    public GameObject challenge1;

    public void openMenu(){
        if(!menuPanel.active){
        menuPanel.SetActive(true);
        }
        else {
          menuPanel.SetActive(false);
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
}
