using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// Create the start Scene, with the names and student numbers of your team, and at least two buttons: Play Game, and Exit.
/// Play game should lead to the Overworld scene. Players should see the button change when their mouse hovers over. 
/// When they click, it responds visually and then changes scenes. The Exit button should quit the application. 
/// Edited By : Hardik Dipakbhai Shah (101249099)
/// Reference:https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
/// Reference:https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
/// </summary>

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        Debug.Log("PlayButton Pressed..!!");
        StartCoroutine(Wait());                                                         //This function will go to the Line 26 and run that code
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);           //This function will add the BuildIndex as per the Build Settings(For our example it will go to Level1
    }

    public void OnExitButtonPressed()
    {
        Debug.Log("ExitButton Pressed..!!");
        Application.Quit();                                                             //This function will exit out from the game
    }

    IEnumerator Wait()
    {   
      yield return new WaitForSeconds(14);
    }

}
