using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonControll : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}