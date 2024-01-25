using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    // Example method to change the scene
    public void ChangeToScene(string sceneName)
    {
        Debug.Log("Change the scene");
        SceneManager.LoadScene(sceneName);
    }
}