using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{


    private int totalYellowInHouse, totalGreenInHouse, totalBlueInHouse;

    public GameObject frameBlue, frameYellow, frameGreen;

    public GameObject bluePlayer1Border, bluePlayer2Border, bluePlayer3Border;
    public GameObject greenPlayer1Border, greenPlayer2Border, greenPlayer3Border;
    public GameObject yellowPlayer1Border, yellowPlayer2Border, yellowPlayer3Border;

    public Vector3 bluePlayer1Pos, bluePlayer2Pos, bluePlayer3Pos;
    public Vector3 yellowPlayer1Pos, yellowPlayer2Pos, yellowPlayer3Pos;
    public Vector3 greenPlayer1Pos, greenPlayer2Pos, greenPlayer3Pos;

    public Transform diceRoll;
    public Transform blueDiceRoll, greenDiceRoll, yellowDiceRoll;

    public Button bluePlayer1Button, bluePlayer2Button, bluePlayer3Button;
    public Button greenPlayer1Button, greenPlayer2Button, greenPlayer3Button;
    public Button yellowPlayer1Button, yellowPlayer2Button, yellowPlayer3Button;

    public GameObject blueScreen, yellowScreen, greenScreen;

    private string playerTurn = "BLUE";
    private string currentPlayer = "none";
    private string currentPlayerName = "none";

    //player movement controllers iin the game
    public GameObject bluePlayer1, bluePlayer2, bluePlayer3;
    public GameObject greenPlayer1, greenPlayer2, greenPlayer3;
    public GameObject yellowPlayer1, yellowPlayer2, yellowPlayer3;

    //tracking the players steps
    private int bluePlayer1Steps, bluePlayer2Steps, bluePlayer3Steps;
    private int greenPlayer1Steps, greenPlayer2Steps, greenPlayer3Steps;
    private int yellowPlayer1Steps, yellowPlayer2Steps, yellowPlayer3Steps;


    //hold data about dice... particulary number animation
    private int selectDiceNUmAnimation;

    //dice animations
    public GameObject dice1RollAnimation;
    public GameObject dice2RollAnimation;
    public GameObject dice3RollAnimation;
    public GameObject dice4RollAnimation;
    public GameObject dice5RollAnimation;
    public GameObject dice6RollAnimation;

    //players movement corresponding to blocks
    public List<GameObject> blueMovement = new List<GameObject>();
    public List<GameObject> greenMovement = new List<GameObject>();
    public List<GameObject> yellowMovement = new List<GameObject>();

    //for generating random number
    private System.Random randNo;
    public GameObject confirmScreen;
    public GameObject gameCompletedScreen;


    public Button diceRollButton;

    public void ExitMethod()
    {
        SoundManager.buttonAudioSource.Play();
        confirmScreen.SetActive(true);
    }
    public void NoMethod()
    {
        SoundManager.buttonAudioSource.Play();
        confirmScreen.SetActive(false);
    }
    public void YesMethod()
    {
        SoundManager.buttonAudioSource.Play();
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator GameCompletedRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        gameCompletedScreen.SetActive(true);
    }
    public void YesGameCompleted()
    {
        SoundManager.buttonAudioSource.Play();
        SceneManager.LoadScene("ngendo");
    }

    public void NoGameCompleted()
    {
        SoundManager.buttonAudioSource.Play();
        SceneManager.LoadScene("MainMenu");
    }


    void InitializeDice()
    {
        diceRollButton.interactable = true;

        dice1RollAnimation.SetActive(false);
        dice2RollAnimation.SetActive(false);
        dice3RollAnimation.SetActive(false);
        dice4RollAnimation.SetActive(false);
        dice5RollAnimation.SetActive(false);
        dice6RollAnimation.SetActive(false);

        switch (Menu.howManyPlayers)
        {
            case 2:

                if (totalBlueInHouse > 2)
                {
                    SoundManager.winAudioSource.Play();
                    blueScreen.SetActive(true);

                    _ = StartCoroutine("GameCompletedRoutine");
                    playerTurn = "none";
                }
                if (totalGreenInHouse > 2)
                {
                    SoundManager.winAudioSource.Play();
                    greenScreen.SetActive(true);

                    _ = StartCoroutine("GameCompletedRoutine");
                    playerTurn = "none";
                }
                break;

            case 3:

                if (totalGreenInHouse > 2 && totalBlueInHouse < 3 && totalYellowInHouse < 3 && playerTurn == "GREEN")
                {
                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    greenScreen.SetActive(true);
                    Debug.Log("Green Player WON");
                    playerTurn = "BLUE";
                }

                if (totalBlueInHouse > 2 && totalGreenInHouse < 3 && totalYellowInHouse < 3 && playerTurn == "BLUE")
                {
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    blueScreen.SetActive(true);
                    Debug.Log("Blue Player WON");
                    playerTurn = "YELLOW";
                }

                if (totalYellowInHouse > 2 && totalGreenInHouse < 3 && totalBlueInHouse < 3 && playerTurn == "YELLOW")
                {
                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    yellowScreen.SetActive(true);
                    Debug.Log("Yellow Player WON");
                    playerTurn = "GREEN";
                }

                //==============when a player win the game============
                if (totalBlueInHouse > 2 && totalGreenInHouse < 3 && totalYellowInHouse < 3 && playerTurn == "BLUE")
                {
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    blueScreen.SetActive(true);
                    Debug.Log("blue player has won");
                    playerTurn = "GREEN";

                }

                if (totalGreenInHouse > 2 && totalBlueInHouse < 3 && totalYellowInHouse < 3 && playerTurn == "GREEN")
                {
                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    greenScreen.SetActive(true);
                    Debug.Log("green player has won");
                    playerTurn = "YELLOW";

                }

                if (totalYellowInHouse > 2 && totalBlueInHouse < 3 && totalGreenInHouse < 3 && playerTurn == "YELLOW")
                {
                    if (!yellowScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    yellowScreen.SetActive(true);
                    Debug.Log("yellow player has won");
                    playerTurn = "BLUE";

                }
                //===============if any two of three win the game first================
                if (totalBlueInHouse > 2 && totalGreenInHouse > 2 && totalYellowInHouse < 3)
                {
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    blueScreen.SetActive(true);
                    greenScreen.SetActive(true);

                    _ = StartCoroutine("GameCompletedRoutine");
                    Debug.Log("Game is over!");
                    playerTurn = "none";

                }

                if (totalGreenInHouse > 2 && totalBlueInHouse > 2 && totalYellowInHouse < 3)
                {
                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    greenScreen.SetActive(true);
                    blueScreen.SetActive(true);

                    _ = StartCoroutine("GameCompletedRoutine");
                    Debug.Log("Game is over!");
                    playerTurn = "none";

                }

                if (totalGreenInHouse > 2 && totalYellowInHouse > 2 && totalBlueInHouse < 3)
                {
                    if (!yellowScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManager.winAudioSource.Play();
                    }

                    yellowScreen.SetActive(true);
                    greenScreen.SetActive(true);

                    _ = StartCoroutine("GameCompletedRoutine");
                    Debug.Log("Game is over!");
                    playerTurn = "none";

                }
                break;
            default:
                break;
        }


        // -=======for hitting other players=========================
        if (currentPlayerName.Contains("BLUE PLAYER"))
        {
            if (currentPlayerName == "BLUE PLAYER 1")
            {
                currentPlayer = BluePlayerOne.bluePlayerOneColName;
            }

            if (currentPlayerName == "BLUE PLAYER 2")
            {
                currentPlayer = BluePlayerTwo.bluePlayerTwoColName;
            }

            if (currentPlayerName == "BLUE PLAYER 3")
            {
                currentPlayer = BluePlayerThree.bluePlayerThreeColName;
            }

        }

        if (currentPlayerName.Contains("GREEN PLAYER"))
        {
            if (currentPlayerName == "GREEN PLAYER 1")
            {
                currentPlayer = GreenPlayerOne.greenPlayerOneColName;
            }

            if (currentPlayerName == "GREEN PLAYER 2")
            {
                currentPlayer = GreenPlayerTwo.greenPlayerTwoColName;

            }

            if (currentPlayerName == "GREEN PLAYER 3")
            {
                currentPlayer = GreenPlayerThree.greenPlayerThreeColName;
            }

        }

        if (currentPlayerName.Contains("YELLOW PLAYER"))
        {
            if (currentPlayerName == "YELLOW PLAYER 1")
            {
                currentPlayer = YellowPlayerOne.yellowPlayerOneColName;
            }

            if (currentPlayerName == "YELLOW PLAYER 2")
            {
                currentPlayer = YellowPlayerTwo.yellowPlayerTwoColName;
            }

            if (currentPlayerName == "YELLOW PLAYER 3")
            {
                currentPlayer = YellowPlayerThree.yellowPlayerThreeColName;
            }

        }

        //=============FOR HITTING EACH OTHER==========

        if (currentPlayerName != "none")
        {
            switch (Menu.howManyPlayers)
            {
                case 2:
                    if (currentPlayerName.Contains("BLUE PLAYER"))
                    {
                        if (currentPlayer == GreenPlayerOne.greenPlayerOneColName && currentPlayer != "stop" && GreenPlayerOne.greenPlayerOneColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            greenPlayer1.transform.position = greenPlayer1Pos;
                            GreenPlayerOne.greenPlayerOneColName = "none";
                            greenPlayer1Steps = 0;
                            playerTurn = "BLUE";
                        }

                        if (currentPlayer == GreenPlayerTwo.greenPlayerTwoColName && currentPlayer != "stop" && GreenPlayerTwo.greenPlayerTwoColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            greenPlayer2.transform.position = greenPlayer2Pos;
                            GreenPlayerTwo.greenPlayerTwoColName = "none";
                            greenPlayer2Steps = 0;
                            playerTurn = "BLUE";
                        }

                        if (currentPlayer == GreenPlayerThree.greenPlayerThreeColName && currentPlayer != "stop" && GreenPlayerThree.greenPlayerThreeColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            greenPlayer3.transform.position = greenPlayer3Pos;
                            GreenPlayerThree.greenPlayerThreeColName = "none";
                            greenPlayer3Steps = 0;
                            playerTurn = "BLUE";
                        }
                    }

                    if (currentPlayerName.Contains("GREEN PLAYER"))
                    {
                        if (currentPlayer == BluePlayerOne.bluePlayerOneColName && currentPlayer != "stop" && BluePlayerOne.bluePlayerOneColName != "Stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            bluePlayer1.transform.position = bluePlayer1Pos;
                            BluePlayerOne.bluePlayerOneColName = "none";
                            bluePlayer1Steps = 0;
                            playerTurn = "GREEN";
                        }

                        if (currentPlayer == BluePlayerTwo.bluePlayerTwoColName && currentPlayer != "stop" && BluePlayerTwo.bluePlayerTwoColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            bluePlayer2.transform.position = bluePlayer2Pos;
                            BluePlayerTwo.bluePlayerTwoColName = "none";
                            bluePlayer2Steps = 0;
                            playerTurn = "GREEN";
                        }

                        if (currentPlayer == BluePlayerThree.bluePlayerThreeColName && currentPlayer != "stop" && BluePlayerThree.bluePlayerThreeColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            bluePlayer3.transform.position = bluePlayer3Pos;
                            BluePlayerThree.bluePlayerThreeColName = "none";
                            bluePlayer3Steps = 0;
                            playerTurn = "GREEN";
                        }
                    }
                    break;
                case 3:
                    if (currentPlayerName.Contains("BLUE PLAYER"))
                    {
                        if (currentPlayer == GreenPlayerOne.greenPlayerOneColName && currentPlayer != "stop" && GreenPlayerOne.greenPlayerOneColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            greenPlayer1.transform.position = greenPlayer1Pos;
                            GreenPlayerOne.greenPlayerOneColName = "none";
                            greenPlayer1Steps = 0;
                            playerTurn = "BLUE";
                        }

                        if (currentPlayer == GreenPlayerTwo.greenPlayerTwoColName && currentPlayer != "stop" && GreenPlayerTwo.greenPlayerTwoColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            greenPlayer2.transform.position = greenPlayer2Pos;
                            GreenPlayerTwo.greenPlayerTwoColName = "none";
                            greenPlayer2Steps = 0;
                            playerTurn = "BLUE";
                        }

                        if (currentPlayer == GreenPlayerThree.greenPlayerThreeColName && currentPlayer != "stop" && GreenPlayerThree.greenPlayerThreeColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            greenPlayer3.transform.position = greenPlayer3Pos;
                            GreenPlayerThree.greenPlayerThreeColName = "none";
                            greenPlayer3Steps = 0;
                            playerTurn = "BLUE";
                        }

                        if (currentPlayer == YellowPlayerOne.yellowPlayerOneColName && currentPlayer != "stop" && YellowPlayerOne.yellowPlayerOneColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            yellowPlayer1.transform.position = yellowPlayer1Pos;
                            YellowPlayerOne.yellowPlayerOneColName = "none";
                            yellowPlayer1Steps = 0;
                            playerTurn = "BLUE";
                        }

                        if (currentPlayer == YellowPlayerTwo.yellowPlayerTwoColName && currentPlayer != "stop" && YellowPlayerTwo.yellowPlayerTwoColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            yellowPlayer2.transform.position = yellowPlayer2Pos;
                            YellowPlayerTwo.yellowPlayerTwoColName = "none";
                            yellowPlayer2Steps = 0;
                            playerTurn = "BLUE";
                        }

                        if (currentPlayer == YellowPlayerThree.yellowPlayerThreeColName && currentPlayer != "stop" && YellowPlayerThree.yellowPlayerThreeColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            yellowPlayer3.transform.position = yellowPlayer3Pos;
                            YellowPlayerThree.yellowPlayerThreeColName = "none";
                            yellowPlayer3Steps = 0;
                            playerTurn = "BLUE";
                        }
                    }
                    if (currentPlayerName.Contains("YELLOW PLAYER"))
                    {
                        if (currentPlayer == BluePlayerOne.bluePlayerOneColName && currentPlayer != "stop" && BluePlayerOne.bluePlayerOneColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            bluePlayer1.transform.position = bluePlayer1Pos;
                            BluePlayerOne.bluePlayerOneColName = "none";
                            bluePlayer1Steps = 0;
                            playerTurn = "YELLOW";
                        }

                        if (currentPlayer == BluePlayerTwo.bluePlayerTwoColName && currentPlayer != "stop" && BluePlayerTwo.bluePlayerTwoColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            bluePlayer2.transform.position = bluePlayer2Pos;
                            BluePlayerTwo.bluePlayerTwoColName = "none";
                            bluePlayer2Steps = 0;
                            playerTurn = "YELLOW";
                        }

                        if (currentPlayer == BluePlayerThree.bluePlayerThreeColName && currentPlayer != "stop" && BluePlayerThree.bluePlayerThreeColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            bluePlayer3.transform.position = bluePlayer3Pos;
                            BluePlayerThree.bluePlayerThreeColName = "none";
                            bluePlayer3Steps = 0;
                            playerTurn = "YELLOW";
                        }

                        if (currentPlayer == GreenPlayerOne.greenPlayerOneColName && currentPlayer != "stop" && GreenPlayerOne.greenPlayerOneColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            greenPlayer1.transform.position = greenPlayer1Pos;
                            GreenPlayerOne.greenPlayerOneColName = "none";
                            greenPlayer1Steps = 0;
                            playerTurn = "YELLOW";
                        }

                        if (currentPlayer == GreenPlayerTwo.greenPlayerTwoColName && currentPlayer != "stop" && GreenPlayerTwo.greenPlayerTwoColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            greenPlayer2.transform.position = greenPlayer2Pos;
                            GreenPlayerTwo.greenPlayerTwoColName = "none";
                            greenPlayer2Steps = 0;
                            playerTurn = "YELLOW";
                        }

                        if (currentPlayer == GreenPlayerThree.greenPlayerThreeColName && currentPlayer != "stop" && GreenPlayerThree.greenPlayerThreeColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            greenPlayer3.transform.position = greenPlayer3Pos;
                            GreenPlayerThree.greenPlayerThreeColName = "none";
                            greenPlayer3Steps = 0;
                            playerTurn = "YELLOW";
                        }
                    }
                    if (currentPlayerName.Contains("GREEN PLAYER"))
                    {
                        if (currentPlayer == BluePlayerOne.bluePlayerOneColName && currentPlayer != "stop" && BluePlayerOne.bluePlayerOneColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            bluePlayer1.transform.position = greenPlayer1Pos;
                            BluePlayerOne.bluePlayerOneColName = "none";
                            bluePlayer1Steps = 0;
                            playerTurn = "GREEN";
                        }

                        if (currentPlayer == BluePlayerTwo.bluePlayerTwoColName && currentPlayer != "stop" && BluePlayerTwo.bluePlayerTwoColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            bluePlayer2.transform.position = greenPlayer2Pos;
                            BluePlayerTwo.bluePlayerTwoColName = "none";
                            bluePlayer2Steps = 0;
                            playerTurn = "GREEN";
                        }

                        if (currentPlayer == BluePlayerThree.bluePlayerThreeColName && currentPlayer != "stop" && BluePlayerThree.bluePlayerThreeColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            bluePlayer3.transform.position = greenPlayer3Pos;
                            BluePlayerThree.bluePlayerThreeColName = "none";
                            bluePlayer3Steps = 0;
                            playerTurn = "GREEN";
                        }

                        if (currentPlayer == YellowPlayerOne.yellowPlayerOneColName && currentPlayer != "stop" && YellowPlayerOne.yellowPlayerOneColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            yellowPlayer1.transform.position = yellowPlayer1Pos;
                            YellowPlayerOne.yellowPlayerOneColName = "none";
                            yellowPlayer1Steps = 0;
                            playerTurn = "GREEN";
                        }

                        if (currentPlayer == YellowPlayerTwo.yellowPlayerTwoColName && currentPlayer != "stop" && YellowPlayerTwo.yellowPlayerTwoColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            yellowPlayer2.transform.position = yellowPlayer2Pos;
                            YellowPlayerTwo.yellowPlayerTwoColName = "none";
                            yellowPlayer2Steps = 0;
                            playerTurn = "GREEN";
                        }

                        if (currentPlayer == YellowPlayerThree.yellowPlayerThreeColName && currentPlayer != "stop" && YellowPlayerThree.yellowPlayerThreeColName != "stop")
                        {
                            SoundManager.dismissalAudioSource.Play();
                            yellowPlayer3.transform.position = yellowPlayer3Pos;
                            YellowPlayerThree.yellowPlayerThreeColName = "none";
                            yellowPlayer3Steps = 0;
                            playerTurn = "GREEN";
                        }


                    }
                    break;
                default:
                    break;
            }
        }

        switch (Menu.howManyPlayers)
        {
            case 2:
                if (playerTurn == "BLUE")
                {
                    diceRoll.position = blueDiceRoll.position;

                    frameBlue.SetActive(true);
                    frameGreen.SetActive(false);
                    frameYellow.SetActive(false);
                }

                if (playerTurn == "GREEN")
                {
                    diceRoll.position = greenDiceRoll.position;

                    frameBlue.SetActive(false);
                    frameGreen.SetActive(true);
                    frameYellow.SetActive(false);
                }

                bluePlayer1Button.interactable = false;
                bluePlayer2Button.interactable = false;
                bluePlayer3Button.interactable = false;

                greenPlayer1Button.interactable = false;
                greenPlayer2Button.interactable = false;
                greenPlayer3Button.interactable = false;

                //deactivate the borders
                bluePlayer1Border.SetActive(false);
                bluePlayer2Border.SetActive(false);
                bluePlayer3Border.SetActive(false);

                greenPlayer1Border.SetActive(false);
                greenPlayer2Border.SetActive(false);
                greenPlayer3Border.SetActive(false);

                break;

            case 3:
                if (playerTurn == "YELLOW")
                {
                    diceRoll.position = yellowDiceRoll.position;

                    frameYellow.SetActive(true);
                    frameGreen.SetActive(false);
                    frameBlue.SetActive(false);
                }

                if (playerTurn == "BLUE")
                {
                    diceRoll.position = blueDiceRoll.position;

                    frameBlue.SetActive(true);
                    frameGreen.SetActive(false);
                    frameYellow.SetActive(false);
                }

                if (playerTurn == "GREEN")
                {
                    diceRoll.position = greenDiceRoll.position;

                    frameGreen.SetActive(true);
                    frameBlue.SetActive(false);
                    frameYellow.SetActive(false);
                }
                bluePlayer1Button.interactable = false;
                bluePlayer2Button.interactable = false;
                bluePlayer3Button.interactable = false;

                greenPlayer1Button.interactable = false;
                greenPlayer2Button.interactable = false;
                greenPlayer3Button.interactable = false;

                yellowPlayer1Button.interactable = false;
                yellowPlayer2Button.interactable = false;
                yellowPlayer3Button.interactable = false;



                bluePlayer1Border.SetActive(false);
                bluePlayer2Border.SetActive(false);
                bluePlayer3Border.SetActive(false);

                greenPlayer1Border.SetActive(false);
                greenPlayer2Border.SetActive(false);
                greenPlayer3Border.SetActive(false);

                yellowPlayer1Border.SetActive(false);
                yellowPlayer2Border.SetActive(false);
                yellowPlayer3Border.SetActive(false);

                break;
            default:           ////i did this
                break;


        }
        selectDiceNUmAnimation = 0;

    }



    public void DiceRoll()
    {

        SoundManager.diceAudioSource.Play();
        diceRollButton.interactable = false;

        selectDiceNUmAnimation = randNo.Next(1, 7);


        switch (selectDiceNUmAnimation)
        {
            case 1:
                dice1RollAnimation.SetActive(true);
                dice2RollAnimation.SetActive(false);
                dice3RollAnimation.SetActive(false);
                dice4RollAnimation.SetActive(false);
                dice5RollAnimation.SetActive(false);
                dice6RollAnimation.SetActive(false);

                break;
            case 2:
                dice1RollAnimation.SetActive(false);
                dice2RollAnimation.SetActive(true);
                dice3RollAnimation.SetActive(false);
                dice4RollAnimation.SetActive(false);
                dice5RollAnimation.SetActive(false);
                dice6RollAnimation.SetActive(false);

                break;
            case 3:
                dice1RollAnimation.SetActive(false);
                dice2RollAnimation.SetActive(false);
                dice3RollAnimation.SetActive(true);
                dice4RollAnimation.SetActive(false);
                dice5RollAnimation.SetActive(false);
                dice6RollAnimation.SetActive(false);

                break;
            case 4:
                dice1RollAnimation.SetActive(false);
                dice2RollAnimation.SetActive(false);
                dice3RollAnimation.SetActive(false);
                dice4RollAnimation.SetActive(true);
                dice5RollAnimation.SetActive(false);
                dice6RollAnimation.SetActive(false);

                break;
            case 5:
                dice1RollAnimation.SetActive(false);
                dice2RollAnimation.SetActive(false);
                dice3RollAnimation.SetActive(false);
                dice4RollAnimation.SetActive(false);
                dice5RollAnimation.SetActive(true);
                dice6RollAnimation.SetActive(false);

                break;
            case 6:
                dice1RollAnimation.SetActive(false);
                dice2RollAnimation.SetActive(false);
                dice3RollAnimation.SetActive(false);
                dice4RollAnimation.SetActive(false);
                dice5RollAnimation.SetActive(false);
                dice6RollAnimation.SetActive(true);

                break;
            default:
                break;
        }

        _ = StartCoroutine("PlayersNotInitialized");

    }

    IEnumerator PlayersNotInitialized()        ///added = n private
    {
        yield return new WaitForSeconds(1f);

        switch (playerTurn)
        {
            case "BLUE":
                //borders show which turn it is for players
                if ((blueMovement.Count - bluePlayer1Steps) >= selectDiceNUmAnimation && bluePlayer1Steps > 0 && (blueMovement.Count > bluePlayer1Steps))
                {
                    bluePlayer1Border.SetActive(true);
                    bluePlayer1Button.interactable = true;
                }
                else
                {
                    bluePlayer1Border.SetActive(false);
                    bluePlayer1Button.interactable = false;
                }

                if ((blueMovement.Count - bluePlayer2Steps) >= selectDiceNUmAnimation && bluePlayer2Steps > 0 && (blueMovement.Count > bluePlayer2Steps))
                {
                    bluePlayer2Border.SetActive(true);
                    bluePlayer2Button.interactable = true;
                }
                else
                {
                    bluePlayer2Border.SetActive(false);
                    bluePlayer2Button.interactable = false;
                }

                if ((blueMovement.Count - bluePlayer3Steps) >= selectDiceNUmAnimation && bluePlayer3Steps > 0 && (blueMovement.Count > bluePlayer3Steps))
                {
                    bluePlayer3Border.SetActive(true);
                    bluePlayer3Button.interactable = true;
                }
                else
                {
                    bluePlayer3Border.SetActive(false);
                    bluePlayer3Button.interactable = false;
                }

                if (selectDiceNUmAnimation == 6 && bluePlayer1Steps == 0)
                {
                    bluePlayer1Border.SetActive(true);
                    bluePlayer1Button.interactable = true;
                }

                if (selectDiceNUmAnimation == 6 && bluePlayer2Steps == 0)
                {
                    bluePlayer2Border.SetActive(true);
                    bluePlayer2Button.interactable = true;
                }

                if (selectDiceNUmAnimation == 6 && bluePlayer3Steps == 0)
                {
                    bluePlayer3Border.SetActive(true);
                    bluePlayer3Button.interactable = true;
                }

                //for switching the borders
                if (!bluePlayer1Border.activeInHierarchy && !bluePlayer2Border.activeInHierarchy && !bluePlayer3Border.activeInHierarchy)
                {

                    bluePlayer1Button.interactable = false;
                    bluePlayer2Button.interactable = false;
                    bluePlayer3Button.interactable = false;

                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "GREEN";
                            InitializeDice();
                            break;

                        case 3:
                            playerTurn = "GREEN";
                            InitializeDice();
                            break;
                        default:          //did the same shit here
                            break;



                    }
                }
                break;
            default:
                break;

            case "GREEN":
                //CONDITION for border glow
                if ((greenMovement.Count - greenPlayer1Steps) >= selectDiceNUmAnimation && greenPlayer1Steps > 0 && (greenMovement.Count > greenPlayer1Steps))
                {
                    greenPlayer1Border.SetActive(true);
                    greenPlayer1Button.interactable = true;
                }
                else
                {
                    greenPlayer1Border.SetActive(false);
                    greenPlayer1Button.interactable = false;
                }

                if ((greenMovement.Count - greenPlayer2Steps) >= selectDiceNUmAnimation && greenPlayer2Steps > 0 && (greenMovement.Count > greenPlayer2Steps))
                {
                    greenPlayer2Border.SetActive(true);
                    greenPlayer2Button.interactable = true;
                }
                else
                {
                    greenPlayer2Border.SetActive(false);
                    greenPlayer2Button.interactable = false;
                }

                if ((greenMovement.Count - greenPlayer3Steps) >= selectDiceNUmAnimation && greenPlayer3Steps > 0 && (greenMovement.Count > greenPlayer3Steps))
                {
                    greenPlayer3Border.SetActive(true);
                    greenPlayer3Button.interactable = true;
                }
                else
                {
                    greenPlayer3Border.SetActive(false);
                    greenPlayer3Button.interactable = false;
                }



                //borders show which turn it is for players


                if (selectDiceNUmAnimation == 6 && greenPlayer1Steps == 0)
                {
                    greenPlayer1Border.SetActive(true);
                    greenPlayer1Button.interactable = true;
                }

                if (selectDiceNUmAnimation == 6 && greenPlayer2Steps == 0)
                {
                    greenPlayer2Border.SetActive(true);
                    greenPlayer2Button.interactable = true;
                }

                if (selectDiceNUmAnimation == 6 && greenPlayer3Steps == 0)
                {
                    greenPlayer3Border.SetActive(true);
                    greenPlayer3Button.interactable = true;
                }

                //for switching the borders

                if (!greenPlayer1Border.activeInHierarchy && !greenPlayer2Border.activeInHierarchy && !greenPlayer3Border.activeInHierarchy)
                {
                    greenPlayer1Button.interactable = false;
                    greenPlayer2Button.interactable = false;
                    greenPlayer3Button.interactable = false;

                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "BLUE";
                            InitializeDice();
                            break;

                        case 3:
                            playerTurn = "YELLOW";
                            InitializeDice();
                            break;
                        default:
                            break;      //again here
                    }
                }
                break;

            case "YELLOW":

                if ((yellowMovement.Count - yellowPlayer1Steps) >= selectDiceNUmAnimation && yellowPlayer1Steps > 0 && (yellowMovement.Count > yellowPlayer1Steps))
                {
                    yellowPlayer1Border.SetActive(true);
                    yellowPlayer1Button.interactable = true;
                }
                else
                {
                    yellowPlayer1Border.SetActive(false);
                    yellowPlayer1Button.interactable = false;
                }

                if ((yellowMovement.Count - yellowPlayer2Steps) >= selectDiceNUmAnimation && yellowPlayer2Steps > 0 && (yellowMovement.Count > yellowPlayer2Steps))
                {
                    yellowPlayer2Border.SetActive(true);
                    yellowPlayer2Button.interactable = true;
                }
                else
                {
                    yellowPlayer2Border.SetActive(false);
                    yellowPlayer2Button.interactable = false;
                }

                if ((yellowMovement.Count - yellowPlayer3Steps) >= selectDiceNUmAnimation && yellowPlayer3Steps > 0 && (yellowMovement.Count > yellowPlayer3Steps))
                {
                    yellowPlayer3Border.SetActive(true);
                    yellowPlayer3Button.interactable = true;
                }
                else
                {
                    yellowPlayer3Border.SetActive(false);
                    yellowPlayer3Button.interactable = false;
                }

                if (selectDiceNUmAnimation == 6 && yellowPlayer1Steps == 0)
                {
                    yellowPlayer1Border.SetActive(true);
                    yellowPlayer1Button.interactable = true;
                }

                if (selectDiceNUmAnimation == 6 && yellowPlayer2Steps == 0)
                {
                    yellowPlayer2Border.SetActive(true);
                    yellowPlayer2Button.interactable = true;
                }

                if (selectDiceNUmAnimation == 6 && yellowPlayer3Steps == 0)
                {
                    yellowPlayer3Border.SetActive(true);
                    yellowPlayer3Button.interactable = true;
                }


                //for switching the borders

                if (!yellowPlayer1Border.activeInHierarchy && !yellowPlayer2Border.activeInHierarchy && !yellowPlayer3Border.activeInHierarchy)
                {
                    yellowPlayer1Button.interactable = false;
                    yellowPlayer2Button.interactable = false;
                    yellowPlayer3Button.interactable = false;

                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            //playerTurn = "GREEN";
                            //InitializeDice();
                            break;

                        case 3:
                            playerTurn = "BLUE";
                            InitializeDice();
                            break;
                        default:
                            break;

                    }
                }
                break;
        }

    }

    public void GreenPlayer1Movement()
    {
        SoundManager.playerAudioSource.Play();
        greenPlayer1Border.SetActive(false);
        greenPlayer2Border.SetActive(false);
        greenPlayer3Border.SetActive(false);

        greenPlayer1Button.interactable = false;
        greenPlayer2Button.interactable = false;
        greenPlayer3Button.interactable = false;

        if (playerTurn == "GREEN" && (greenMovement.Count - greenPlayer1Steps) > selectDiceNUmAnimation)
        {
            if (greenPlayer1Steps > 0)
            {
                Vector3[] greenPlayer1Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    greenPlayer1Path[i] = greenMovement[greenPlayer1Steps + i].transform.position;
                }

                greenPlayer1Steps += selectDiceNUmAnimation;

                if (selectDiceNUmAnimation == 6)
                {
                    playerTurn = "GREEN";
                }
                else
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "BLUE";
                            break;
                        case 3:
                            playerTurn = "YELLOW";
                            break;
                        default:
                            break;

                    }

                }
                //
                currentPlayerName = "GREEN PLAYER 1";

                if (greenPlayer1Path.Length > 1)
                {
                    iTween.MoveTo(greenPlayer1, iTween.Hash("path", greenPlayer1Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(greenPlayer1, iTween.Hash("position", greenPlayer1Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

            }

            else
            {
                if (selectDiceNUmAnimation == 6 && greenPlayer1Steps == 0)
                {
                    Vector3[] greenPlayer1Path = new Vector3[selectDiceNUmAnimation];
                    greenPlayer1Path[0] = greenMovement[greenPlayer1Steps].transform.position;
                    greenPlayer1Steps += 1;
                    playerTurn = "GREEN";
                    currentPlayerName = "GREEN PLAYER 1";
                    iTween.MoveTo(greenPlayer1, iTween.Hash("position", greenPlayer1Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
            }
        }
        else
        {
            //moves left to enter the home color
            if (playerTurn == "GREEN" && (greenMovement.Count - greenPlayer1Steps) == selectDiceNUmAnimation)
            {
                Vector3[] greenPlayer1Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    greenPlayer1Path[i] = greenMovement[greenPlayer1Steps + i].transform.position;
                }

                greenPlayer1Steps += selectDiceNUmAnimation;

                if (greenPlayer1Path.Length > 1)
                {
                    iTween.MoveTo(greenPlayer1, iTween.Hash("path", greenPlayer1Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(greenPlayer1, iTween.Hash("position", greenPlayer1Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

                playerTurn = "GREEN";
                totalGreenInHouse += 1;
                Debug.Log("well done!");
                greenPlayer1Button.enabled = false;
            }
            else
            {
                Debug.Log("You need " + (greenMovement.Count - greenPlayer1Steps).ToString() + " to enter into the house");

                if (greenPlayer2Steps + greenPlayer3Steps == 0 && selectDiceNUmAnimation != 6)
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "BLUE";
                            break;

                        case 3:
                            playerTurn = "YELLOW";
                            break;
                        default:
                            break;
                    }
                    InitializeDice();
                }
            }
        }
    }

    public void GreenPlayer2Movement()
    {
        SoundManager.playerAudioSource.Play();
        greenPlayer1Border.SetActive(false);
        greenPlayer2Border.SetActive(false);
        greenPlayer3Border.SetActive(false);

        greenPlayer1Button.interactable = false;
        greenPlayer2Button.interactable = false;
        greenPlayer3Button.interactable = false;

        if (playerTurn == "GREEN" && (greenMovement.Count - greenPlayer2Steps) > selectDiceNUmAnimation)
        {
            if (greenPlayer2Steps > 0)
            {
                Vector3[] greenPlayer2Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    greenPlayer2Path[i] = greenMovement[greenPlayer2Steps + i].transform.position;
                }

                greenPlayer2Steps += selectDiceNUmAnimation;

                if (selectDiceNUmAnimation == 6)
                {
                    playerTurn = "GREEN";
                }
                else
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "BLUE";
                            break;
                        case 3:
                            playerTurn = "YELLOW";
                            break;
                        default:
                            break;

                    }

                }
                //
                currentPlayerName = "GREEN PLAYER 2";

                if (greenPlayer2Path.Length > 1)
                {
                    iTween.MoveTo(greenPlayer2, iTween.Hash("path", greenPlayer2Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(greenPlayer2, iTween.Hash("position", greenPlayer2Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

            }

            else
            {
                if (selectDiceNUmAnimation == 6 && greenPlayer2Steps == 0)
                {
                    Vector3[] greenPlayer2Path = new Vector3[selectDiceNUmAnimation];
                    greenPlayer2Path[0] = greenMovement[greenPlayer2Steps].transform.position;
                    greenPlayer2Steps += 1;
                    playerTurn = "GREEN";
                    currentPlayerName = "GREEN PLAYER 2";
                    iTween.MoveTo(greenPlayer2, iTween.Hash("position", greenPlayer2Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
            }
        }
        else
        {
            //moves left to enter the home color
            if (playerTurn == "GREEN" && (greenMovement.Count - greenPlayer2Steps) == selectDiceNUmAnimation)
            {
                Vector3[] greenPlayer2Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    greenPlayer2Path[i] = greenMovement[greenPlayer2Steps + i].transform.position;
                }

                greenPlayer2Steps += selectDiceNUmAnimation;

                if (greenPlayer2Path.Length > 1)
                {
                    iTween.MoveTo(greenPlayer2, iTween.Hash("path", greenPlayer2Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(greenPlayer2, iTween.Hash("position", greenPlayer2Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

                playerTurn = "GREEN";
                totalGreenInHouse += 1;
                Debug.Log("well done!");
                greenPlayer2Button.enabled = false;
            }
            else
            {
                Debug.Log("You need " + (greenMovement.Count - greenPlayer2Steps).ToString() + " to enter into the house");

                if (greenPlayer1Steps + greenPlayer3Steps == 0 && selectDiceNUmAnimation != 6)
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "BLUE";
                            break;

                        case 3:
                            playerTurn = "YELLOW";
                            break;
                        default:
                            break;
                    }
                    InitializeDice();
                }
            }
        }
    }

    public void GreenPlayer3Movement()
    {
        SoundManager.playerAudioSource.Play();
        greenPlayer1Border.SetActive(false);
        greenPlayer2Border.SetActive(false);
        greenPlayer3Border.SetActive(false);

        greenPlayer1Button.interactable = false;
        greenPlayer2Button.interactable = false;
        greenPlayer3Button.interactable = false;

        if (playerTurn == "GREEN" && (greenMovement.Count - greenPlayer3Steps) > selectDiceNUmAnimation)
        {
            if (greenPlayer3Steps > 0)
            {
                Vector3[] greenPlayer3Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    greenPlayer3Path[i] = greenMovement[greenPlayer3Steps + i].transform.position;
                }

                greenPlayer3Steps += selectDiceNUmAnimation;

                if (selectDiceNUmAnimation == 6)
                {
                    playerTurn = "GREEN";
                }
                else
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "BLUE";
                            break;
                        case 3:
                            playerTurn = "YELLOW";
                            break;
                        default:
                            break;

                    }

                }
                //
                currentPlayerName = "GREEN PLAYER 3";

                if (greenPlayer3Path.Length > 1)
                {
                    iTween.MoveTo(greenPlayer3, iTween.Hash("path", greenPlayer3Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(greenPlayer3, iTween.Hash("position", greenPlayer3Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

            }

            else
            {
                if (selectDiceNUmAnimation == 6 && greenPlayer3Steps == 0)
                {
                    Vector3[] greenPlayer3Path = new Vector3[selectDiceNUmAnimation];
                    greenPlayer3Path[0] = greenMovement[greenPlayer3Steps].transform.position;
                    greenPlayer3Steps += 1;
                    playerTurn = "GREEN";
                    currentPlayerName = "GREEN PLAYER 3";
                    iTween.MoveTo(greenPlayer3, iTween.Hash("position", greenPlayer3Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
            }
        }
        else
        {
            //moves left to enter the home color
            if (playerTurn == "GREEN" && (greenMovement.Count - greenPlayer3Steps) == selectDiceNUmAnimation)
            {
                Vector3[] greenPlayer3Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    greenPlayer3Path[i] = greenMovement[greenPlayer3Steps + i].transform.position;
                }

                greenPlayer3Steps += selectDiceNUmAnimation;

                if (greenPlayer3Path.Length > 1)
                {
                    iTween.MoveTo(greenPlayer3, iTween.Hash("path", greenPlayer3Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(greenPlayer3, iTween.Hash("position", greenPlayer3Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

                playerTurn = "GREEN";
                totalGreenInHouse += 1;
                Debug.Log("well done!");
                greenPlayer3Button.enabled = false;
            }
            else
            {
                Debug.Log("You need " + (greenMovement.Count - greenPlayer3Steps).ToString() + " to enter into the house");

                if (greenPlayer1Steps + greenPlayer2Steps == 0 && selectDiceNUmAnimation != 6)
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "BLUE";
                            break;

                        case 3:
                            playerTurn = "YELLOW";
                            break;
                        default:

                            break;
                    }
                    InitializeDice();
                }
            }
        }
    }

    public void BluePlayer1Movement()
    {
        SoundManager.playerAudioSource.Play();
        bluePlayer1Border.SetActive(false);
        bluePlayer2Border.SetActive(false);
        bluePlayer3Border.SetActive(false);

        bluePlayer1Button.interactable = false;
        bluePlayer2Button.interactable = false;
        bluePlayer3Button.interactable = false;

        if (playerTurn == "BLUE" && (blueMovement.Count - bluePlayer1Steps) > selectDiceNUmAnimation)
        {
            if (bluePlayer1Steps > 0)
            {
                Vector3[] bluePlayer1Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    bluePlayer1Path[i] = blueMovement[bluePlayer1Steps + i].transform.position;
                }

                bluePlayer1Steps += selectDiceNUmAnimation;

                if (selectDiceNUmAnimation == 6)
                {
                    playerTurn = "BLUE";
                }
                else
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "GREEN";
                            break;
                        case 3:
                            playerTurn = "GREEN";
                            break;
                        default:
                            break;

                    }

                }
                //
                currentPlayerName = "BLUE PLAYER 1";

                if (bluePlayer1Path.Length > 1)
                {
                    iTween.MoveTo(bluePlayer1, iTween.Hash("path", bluePlayer1Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(bluePlayer1, iTween.Hash("position", bluePlayer1Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

            }

            else
            {
                if (selectDiceNUmAnimation == 6 && bluePlayer1Steps == 0)
                {
                    Vector3[] bluePlayer1Path = new Vector3[selectDiceNUmAnimation];
                    bluePlayer1Path[0] = blueMovement[bluePlayer1Steps].transform.position;
                    bluePlayer1Steps += 1;
                    playerTurn = "BLUE";
                    currentPlayerName = "BLUE PLAYER 1";
                    iTween.MoveTo(bluePlayer1, iTween.Hash("position", bluePlayer1Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
            }
        }
        else
        {
            //moves left to enter the home color
            if (playerTurn == "BLUE" && (blueMovement.Count - bluePlayer1Steps) == selectDiceNUmAnimation)
            {
                Vector3[] bluePlayer1Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    bluePlayer1Path[i] = blueMovement[bluePlayer1Steps + i].transform.position;
                }

                bluePlayer1Steps += selectDiceNUmAnimation;

                if (bluePlayer1Path.Length > 1)
                {
                    iTween.MoveTo(bluePlayer1, iTween.Hash("path", bluePlayer1Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(bluePlayer1, iTween.Hash("position", bluePlayer1Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

                playerTurn = "BLUE";
                totalBlueInHouse += 1;
                Debug.Log("well done!");
                bluePlayer1Button.enabled = false;
            }
            else
            {
                Debug.Log("You need " + (blueMovement.Count - bluePlayer1Steps).ToString() + " to enter into the house");

                if (bluePlayer2Steps + bluePlayer3Steps == 0 && selectDiceNUmAnimation != 6)
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "GREEN";
                            break;

                        case 3:
                            playerTurn = "GREEN";
                            break;
                        default:
                            break;
                    }
                    InitializeDice();
                }
            }
        }
    }

    public void BluePlayer2Movement()
    {
        SoundManager.playerAudioSource.Play();
        bluePlayer1Border.SetActive(false);
        bluePlayer2Border.SetActive(false);
        bluePlayer3Border.SetActive(false);

        bluePlayer1Button.interactable = false;
        bluePlayer2Button.interactable = false;
        bluePlayer3Button.interactable = false;

        if (playerTurn == "BLUE" && (blueMovement.Count - bluePlayer2Steps) > selectDiceNUmAnimation)
        {
            if (bluePlayer2Steps > 0)
            {
                Vector3[] bluePlayer2Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    bluePlayer2Path[i] = blueMovement[bluePlayer2Steps + i].transform.position;
                }

                bluePlayer2Steps += selectDiceNUmAnimation;

                if (selectDiceNUmAnimation == 6)
                {
                    playerTurn = "BLUE";
                }
                else
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "GREEN";
                            break;
                        case 3:
                            playerTurn = "GREEN";
                            break;
                        default:
                            break;

                    }

                }
                //
                currentPlayerName = "BLUE PLAYER 2";

                if (bluePlayer2Path.Length > 1)
                {
                    iTween.MoveTo(bluePlayer2, iTween.Hash("path", bluePlayer2Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(bluePlayer2, iTween.Hash("position", bluePlayer2Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

            }

            else
            {
                if (selectDiceNUmAnimation == 6 && bluePlayer2Steps == 0)
                {
                    Vector3[] bluePlayer2Path = new Vector3[selectDiceNUmAnimation];
                    bluePlayer2Path[0] = blueMovement[bluePlayer2Steps].transform.position;
                    bluePlayer2Steps += 1;
                    playerTurn = "BLUE";
                    currentPlayerName = "BLUE PLAYER 2";
                    iTween.MoveTo(bluePlayer2, iTween.Hash("position", bluePlayer2Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
            }
        }
        else
        {
            //moves left to enter the home color
            if (playerTurn == "BLUE" && (blueMovement.Count - bluePlayer2Steps) == selectDiceNUmAnimation)
            {
                Vector3[] bluePlayer2Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    bluePlayer2Path[i] = blueMovement[bluePlayer2Steps + i].transform.position;
                }

                bluePlayer2Steps += selectDiceNUmAnimation;

                if (bluePlayer2Path.Length > 1)
                {
                    iTween.MoveTo(bluePlayer2, iTween.Hash("path", bluePlayer2Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(bluePlayer2, iTween.Hash("position", bluePlayer2Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

                playerTurn = "BLUE";
                totalBlueInHouse += 1;
                Debug.Log("well done!");
                bluePlayer2Button.enabled = false;
            }
            else
            {
                Debug.Log("You need " + (blueMovement.Count - bluePlayer2Steps).ToString() + " to enter into the house");

                if (bluePlayer1Steps + bluePlayer3Steps == 0 && selectDiceNUmAnimation != 6)
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "GREEN";
                            break;

                        case 3:
                            playerTurn = "GREEN";
                            break;
                        default:
                            break;
                    }
                    InitializeDice();
                }
            }
        }
    }

    public void BluePlayer3Movement()
    {
        SoundManager.playerAudioSource.Play();
        bluePlayer1Border.SetActive(false);
        bluePlayer2Border.SetActive(false);
        bluePlayer3Border.SetActive(false);

        bluePlayer1Button.interactable = false;
        bluePlayer2Button.interactable = false;
        bluePlayer3Button.interactable = false;

        if (playerTurn == "BLUE" && (blueMovement.Count - bluePlayer3Steps) > selectDiceNUmAnimation)
        {
            if (bluePlayer3Steps > 0)
            {
                Vector3[] bluePlayer3Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    bluePlayer3Path[i] = blueMovement[bluePlayer3Steps + i].transform.position;
                }

                bluePlayer3Steps += selectDiceNUmAnimation;

                if (selectDiceNUmAnimation == 6)
                {
                    playerTurn = "BLUE";
                }
                else
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "GREEN";
                            break;
                        case 3:
                            playerTurn = "GREEN";
                            break;
                        default:
                            break;

                    }

                }
                //
                currentPlayerName = "BLUE PLAYER 3";

                if (bluePlayer3Path.Length > 1)
                {
                    iTween.MoveTo(bluePlayer3, iTween.Hash("path", bluePlayer3Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(bluePlayer3, iTween.Hash("position", bluePlayer3Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

            }

            else
            {
                if (selectDiceNUmAnimation == 6 && bluePlayer3Steps == 0)
                {
                    Vector3[] bluePlayer3Path = new Vector3[selectDiceNUmAnimation];
                    bluePlayer3Path[0] = blueMovement[bluePlayer3Steps].transform.position;
                    bluePlayer3Steps += 1;
                    playerTurn = "BLUE";
                    currentPlayerName = "BLUE PLAYER 3";
                    iTween.MoveTo(bluePlayer3, iTween.Hash("position", bluePlayer3Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
            }
        }
        else
        {
            //===========shivgh=moves left to enter the home color===============
            if (playerTurn == "BLUE" && (blueMovement.Count - bluePlayer3Steps) == selectDiceNUmAnimation)
            {
                Vector3[] bluePlayer3Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    bluePlayer3Path[i] = blueMovement[bluePlayer3Steps + i].transform.position;
                }

                bluePlayer3Steps += selectDiceNUmAnimation;

                if (bluePlayer3Path.Length > 1)
                {
                    iTween.MoveTo(bluePlayer3, iTween.Hash("path", bluePlayer3Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(bluePlayer3, iTween.Hash("position", bluePlayer3Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

                playerTurn = "BLUE";
                totalBlueInHouse += 1;
                Debug.Log("well done!");
                bluePlayer3Button.enabled = false;
            }
            else
            {
                Debug.Log("You need " + (blueMovement.Count - bluePlayer3Steps).ToString() + " to enter into the house");

                if (bluePlayer1Steps + bluePlayer2Steps == 0 && selectDiceNUmAnimation != 6)
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "GREEN";
                            break;

                        case 3:
                            playerTurn = "GREEN";
                            break;
                        default:
                            break;


                    }
                    InitializeDice();
                }
            }
        }
    }

    public void YellowPlayer1Movement()
    {
        SoundManager.playerAudioSource.Play();
        yellowPlayer1Border.SetActive(false);
        yellowPlayer2Border.SetActive(false);
        yellowPlayer3Border.SetActive(false);

        yellowPlayer1Button.interactable = false;
        yellowPlayer2Button.interactable = false;
        yellowPlayer3Button.interactable = false;

        if (playerTurn == "YELLOW" && (yellowMovement.Count - yellowPlayer1Steps) > selectDiceNUmAnimation)
        {
            if (yellowPlayer1Steps > 0)
            {
                Vector3[] yellowPlayer1Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    yellowPlayer1Path[i] = yellowMovement[yellowPlayer1Steps + i].transform.position;
                }

                yellowPlayer1Steps += selectDiceNUmAnimation;

                if (selectDiceNUmAnimation == 6)
                {
                    playerTurn = "YELLOW";
                }
                else
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            //playerTurn = "BLUE";
                            break;
                        case 3:
                            playerTurn = "BLUE";
                            break;
                        default:
                            break;

                    }

                }
                //
                currentPlayerName = "YELLOW PLAYER 1";

                if (yellowPlayer1Path.Length > 1)
                {
                    iTween.MoveTo(yellowPlayer1, iTween.Hash("path", yellowPlayer1Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(yellowPlayer1, iTween.Hash("position", yellowPlayer1Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

            }

            else
            {
                if (selectDiceNUmAnimation == 6 && yellowPlayer1Steps == 0)
                {
                    Vector3[] yellowPlayer1Path = new Vector3[selectDiceNUmAnimation];
                    yellowPlayer1Path[0] = yellowMovement[yellowPlayer1Steps].transform.position;
                    yellowPlayer1Steps += 1;
                    playerTurn = "YELLOW";
                    currentPlayerName = "YELLOW PLAYER 1";
                    iTween.MoveTo(yellowPlayer1, iTween.Hash("position", yellowPlayer1Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
            }
        }
        else
        {
            //moves left to enter the home color
            if (playerTurn == "YELLOW" && (yellowMovement.Count - yellowPlayer1Steps) == selectDiceNUmAnimation)
            {
                Vector3[] yellowPlayer1Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    yellowPlayer1Path[i] = yellowMovement[yellowPlayer1Steps + i].transform.position;
                }

                yellowPlayer1Steps += selectDiceNUmAnimation;

                if (yellowPlayer1Path.Length > 1)
                {
                    iTween.MoveTo(yellowPlayer1, iTween.Hash("path", yellowPlayer1Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(yellowPlayer1, iTween.Hash("position", yellowPlayer1Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

                playerTurn = "YELLOW";
                totalYellowInHouse += 1;
                Debug.Log("well done!");
                yellowPlayer1Button.enabled = false;
            }
            else
            {
                Debug.Log("You need " + (yellowMovement.Count - yellowPlayer1Steps).ToString() + " to enter into the house");

                if (yellowPlayer2Steps + yellowPlayer3Steps == 0 && selectDiceNUmAnimation != 6)
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            //playerTurn = "BLUE";
                            break;

                        case 3:
                            playerTurn = "BLUE";
                            break;
                        default:
                            break;
                    }
                    InitializeDice();
                }
            }
        }
    }

    public void YellowPlayer2Movement()
    {
        SoundManager.playerAudioSource.Play();
        yellowPlayer1Border.SetActive(false);
        yellowPlayer2Border.SetActive(false);
        yellowPlayer3Border.SetActive(false);

        yellowPlayer1Button.interactable = false;
        yellowPlayer2Button.interactable = false;
        yellowPlayer3Button.interactable = false;

        if (playerTurn == "YELLOW" && (yellowMovement.Count - yellowPlayer2Steps) > selectDiceNUmAnimation)
        {
            if (yellowPlayer2Steps > 0)
            {
                Vector3[] yellowPlayer2Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    yellowPlayer2Path[i] = yellowMovement[yellowPlayer2Steps + i].transform.position;
                }

                yellowPlayer2Steps += selectDiceNUmAnimation;

                if (selectDiceNUmAnimation == 6)
                {
                    playerTurn = "YELLOW";
                }
                else
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            //playerTurn = "BLUE";
                            break;
                        case 3:
                            playerTurn = "BLUE";
                            break;
                        default:
                            break;

                    }

                }
                //
                currentPlayerName = "YELLOW PLAYER 2";

                if (yellowPlayer2Path.Length > 1)
                {
                    iTween.MoveTo(yellowPlayer2, iTween.Hash("path", yellowPlayer2Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(yellowPlayer2, iTween.Hash("position", yellowPlayer2Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

            }

            else
            {
                if (selectDiceNUmAnimation == 6 && yellowPlayer2Steps == 0)
                {
                    Vector3[] yellowPlayer2Path = new Vector3[selectDiceNUmAnimation];
                    yellowPlayer2Path[0] = yellowMovement[yellowPlayer2Steps].transform.position;
                    yellowPlayer2Steps += 1;
                    playerTurn = "YELLOW";
                    currentPlayerName = "YELLOW PLAYER 2";
                    iTween.MoveTo(yellowPlayer2, iTween.Hash("position", yellowPlayer2Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
            }
        }
        else
        {
            //moves left to enter the home color
            if (playerTurn == "YELLOW" && (yellowMovement.Count - yellowPlayer2Steps) == selectDiceNUmAnimation)
            {
                Vector3[] yellowPlayer2Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    yellowPlayer2Path[i] = yellowMovement[yellowPlayer2Steps + i].transform.position;
                }

                yellowPlayer2Steps += selectDiceNUmAnimation;

                if (yellowPlayer2Path.Length > 1)
                {
                    iTween.MoveTo(yellowPlayer2, iTween.Hash("path", yellowPlayer2Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(yellowPlayer2, iTween.Hash("position", yellowPlayer2Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

                playerTurn = "YELLOW";
                totalYellowInHouse += 1;
                Debug.Log("well done!");
                yellowPlayer2Button.enabled = false;
            }
            else
            {
                Debug.Log("You need " + (yellowMovement.Count - yellowPlayer2Steps).ToString() + " to enter into the house");

                if (yellowPlayer1Steps + yellowPlayer3Steps == 0 && selectDiceNUmAnimation != 6)
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            //playerTurn = "BLUE";
                            break;

                        case 3:
                            playerTurn = "BLUE";
                            break;
                        default:
                            break;
                    }
                    InitializeDice();
                }
            }
        }
    }

    public void YellowPlayer3Movement()
    {
        SoundManager.playerAudioSource.Play();
        yellowPlayer1Border.SetActive(false);
        yellowPlayer2Border.SetActive(false);
        yellowPlayer3Border.SetActive(false);

        yellowPlayer1Button.interactable = false;
        yellowPlayer2Button.interactable = false;
        yellowPlayer3Button.interactable = false;

        if (playerTurn == "YELLOW" && (yellowMovement.Count - yellowPlayer3Steps) > selectDiceNUmAnimation)
        {
            if (yellowPlayer3Steps > 0)
            {
                Vector3[] yellowPlayer3Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    yellowPlayer3Path[i] = yellowMovement[yellowPlayer3Steps + i].transform.position;
                }

                yellowPlayer3Steps += selectDiceNUmAnimation;

                if (selectDiceNUmAnimation == 6)
                {
                    playerTurn = "YELLOW";
                }
                else
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            //playerTurn = "BLUE";
                            break;
                        case 3:
                            playerTurn = "BLUE";
                            break;
                        default:
                            break;

                    }

                }
                //
                currentPlayerName = "YELLOW PLAYER 3";

                if (yellowPlayer3Path.Length > 1)
                {
                    iTween.MoveTo(yellowPlayer3, iTween.Hash("path", yellowPlayer3Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(yellowPlayer3, iTween.Hash("position", yellowPlayer3Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

            }

            else
            {
                if (selectDiceNUmAnimation == 6 && yellowPlayer3Steps == 0)
                {
                    Vector3[] yellowPlayer3Path = new Vector3[selectDiceNUmAnimation];
                    yellowPlayer3Path[0] = yellowMovement[yellowPlayer3Steps].transform.position;
                    yellowPlayer3Steps += 1;
                    playerTurn = "YELLOW";
                    currentPlayerName = "YELLOW PLAYER 3";
                    iTween.MoveTo(yellowPlayer3, iTween.Hash("position", yellowPlayer3Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
            }
        }
        else
        {
            //moves left to enter the home color
            if (playerTurn == "YELLOW" && (yellowMovement.Count - yellowPlayer3Steps) == selectDiceNUmAnimation)
            {
                Vector3[] yellowPlayer3Path = new Vector3[selectDiceNUmAnimation];

                for (int i = 0; i < selectDiceNUmAnimation; i++)
                {
                    yellowPlayer3Path[i] = yellowMovement[yellowPlayer3Steps + i].transform.position;
                }

                yellowPlayer3Steps += selectDiceNUmAnimation;

                if (yellowPlayer3Path.Length > 1)
                {
                    iTween.MoveTo(yellowPlayer3, iTween.Hash("path", yellowPlayer3Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }
                else
                {
                    iTween.MoveTo(yellowPlayer3, iTween.Hash("position", yellowPlayer3Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", gameObject));
                }

                playerTurn = "YELLOW";
                totalYellowInHouse += 1;
                Debug.Log("well done!");
                yellowPlayer3Button.enabled = false;
            }
            else
            {
                Debug.Log("You need " + (yellowMovement.Count - yellowPlayer3Steps).ToString() + " to enter into the house");

                if (yellowPlayer1Steps + yellowPlayer2Steps == 0 && selectDiceNUmAnimation != 6)
                {
                    switch (Menu.howManyPlayers)
                    {
                        case 2:
                            //playerTurn = "BLUE";
                            break;

                        case 3:
                            playerTurn = "BLUE";
                            break;
                        default:
                            break;
                    }
                    InitializeDice();
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;

        randNo = new System.Random();

        dice1RollAnimation.SetActive(false);
        dice2RollAnimation.SetActive(false);
        dice3RollAnimation.SetActive(false);
        dice4RollAnimation.SetActive(false);
        dice5RollAnimation.SetActive(false);
        dice6RollAnimation.SetActive(false);

        //player 
        bluePlayer1Pos = bluePlayer1.transform.position;
        bluePlayer2Pos = bluePlayer2.transform.position;
        bluePlayer3Pos = bluePlayer3.transform.position;

        greenPlayer1Pos = greenPlayer1.transform.position;
        greenPlayer2Pos = greenPlayer2.transform.position;
        greenPlayer3Pos = greenPlayer3.transform.position;

        yellowPlayer1Pos = yellowPlayer1.transform.position;
        yellowPlayer2Pos = yellowPlayer2.transform.position;
        yellowPlayer3Pos = yellowPlayer3.transform.position;

        //dont forget to write the transform.position for the colors
        bluePlayer1Border.SetActive(false);
        bluePlayer2Border.SetActive(false);
        bluePlayer3Border.SetActive(false);

        greenPlayer1Border.SetActive(false);
        greenPlayer2Border.SetActive(false);
        greenPlayer3Border.SetActive(false);

        yellowPlayer1Border.SetActive(false);
        yellowPlayer2Border.SetActive(false);
        yellowPlayer3Border.SetActive(false);

        blueScreen.SetActive(false);
        greenScreen.SetActive(false);
        yellowScreen.SetActive(false);

        switch (Menu.howManyPlayers)
        {
            case 2:
                playerTurn = "BLUE";

                frameBlue.SetActive(true);
                frameGreen.SetActive(false);
                frameYellow.SetActive(false);

                diceRoll.position = blueDiceRoll.position;

                yellowPlayer1.SetActive(false);
                yellowPlayer2.SetActive(false);
                yellowPlayer3.SetActive(false);

                break;

            case 3:
                playerTurn = "YELLOW";

                frameYellow.SetActive(true);
                frameGreen.SetActive(false);
                frameBlue.SetActive(false);

                diceRoll.position = yellowDiceRoll.position;
                //keep all the players active
                break;
            default:
                break;

        }


    }


    // Update is called once per frame
    void Update()
    {

    }
}

