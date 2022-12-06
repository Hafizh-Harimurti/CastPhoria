using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateMenu : MonoBehaviour
{
    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SwitchSceneWithTransition(string scene)
    {
        NavigationDataHolder.NextSceneToLoad = scene;
        SceneManager.LoadScene("Transition");
    }

    public void SetTransitionSceneText(string transitionText)
    {
        NavigationDataHolder.TransitionSceneText = transitionText;
    }
}
