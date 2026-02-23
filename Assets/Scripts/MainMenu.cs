using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button singleplayerBtn;
    [SerializeField] private Button multiplayerBtn;

    private void Start()
    {
        singleplayerBtn.onClick.AddListener(() =>
        {
            Data.singleplayer = true;
            SceneManager.LoadScene("LevelMenu");
        });
        multiplayerBtn.onClick.AddListener(() => 
        {
            Data.singleplayer = false;
            SceneManager.LoadScene("LevelMenu");
        });
    }
}
