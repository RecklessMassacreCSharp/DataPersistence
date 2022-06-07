using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
    using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] GameObject usernameInput;

    public void StartNew() {
        DataManager.Instance.playerName = usernameInput.GetComponent<TMP_InputField>().text;
        SceneManager.LoadScene(1);
    }

    public void Exit() {
        DataManager.Instance.Save();
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}

