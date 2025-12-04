using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToWin : MonoBehaviour
{
    private void OnDestroy()
    {
        SceneManager.LoadScene("WinScene");
    }
}
