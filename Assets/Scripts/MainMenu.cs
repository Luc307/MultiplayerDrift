using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button singleplayerBtn;
    [SerializeField] private Button multiplayerBtn;

    private void Start()
    {
        singleplayerBtn.onClick.AddListener(() => SceneManager.LoadScene("SpMenu"));
        multiplayerBtn.onClick.AddListener(() => SceneManager.LoadScene("Multiplayer"));
    }
}
