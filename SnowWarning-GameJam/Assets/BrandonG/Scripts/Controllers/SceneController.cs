using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

class SceneController : MonoBehaviour
{
    public int sceneSection = 0;
    public TMP_Text sceneText;
    private MenuController menuController;
    private GameController gameController;
    public Sprite[] images;
    public bool once = false;
    private bool gameOver = false;
    //Map    0
    //Talk   1
    //Travel 2
    //Forest 3

    public void Start()
    {
        //First scene
        sceneText.text = "You were minding your own bussiness as an average wayfarer.";
        menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (once == true)
        {
            GameObject.Find("ScenePanel").GetComponent<Image>().sprite = images[0];
            sceneText.text = "You were minding your own bussiness as an average wayfarer.";
            once = false;
        }
    }


    /// <summary>
    /// Goes to the next in game scene panel
    /// </summary>
    public void Next()
    {
        //Go to the next section/panel based on position
        //Use scene section int in an if/switch to progress

        /*
         * Notes:
         * Change image: GameObject.Find("ScenePanel").GetComponent<Image>().sprite = images[#];
         * Start game: menuController.GameplayStart();
         * Disable scenes: menuController.ScenePanel.SetActive(false);
         * Reenable scenes: menuController.ScenePanel.SetActive(true);
         * Reset application: gameOver = true;
         */
        sceneSection++;

        if (gameOver != true)
        {
            switch (sceneSection)
            {
                case 1:
                    sceneText.text = "One day something changed that would gravely change how you see the world around you.";
                    break;
                case 2:
                    GameObject.Find("ScenePanel").GetComponent<Image>().sprite = images[1];
                    sceneText.text = "An evil witch had appeared before you & had offered you the knowledge of the universe.";
                    break;
                case 3:
                    sceneText.text = "You made the great mistake of trusting her & moments later you went nearly blind, an immensive dark fog now surrounds you.";
                    break;
                case 4:
                    GameObject.Find("ScenePanel").GetComponent<Image>().sprite = images[2];
                    sceneText.text = "After this terrible encounter, you had finally found a great magical stick that could let you finally see through the darkness";
                    break;
                case 5:
                    sceneText.text = "You now go onwards to get back at the witch & regain your sight back!";
                    break;
                case 6:
                    sceneText.text = "";
                    menuController.ScenePanel.SetActive(false);
                    gameController.loadlevel1();
                    break;
                default:
                    sceneText.text = "[Text Failure] \n Scene section: " + sceneSection + "\n Scenes section does not exist.";
                    break;
            }
        }
        else
        {
            menuController.ResetApplication();

        }
    }
}
