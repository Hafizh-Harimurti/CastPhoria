using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    void Start()
    {
        textMeshProUGUI.text = NavigationDataHolder.TransitionSceneText;
    }

    void MoveToNextScene()
    {
        SceneManager.LoadScene(NavigationDataHolder.NextSceneToLoad);
    }
}
