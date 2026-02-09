using UnityEngine;
using UnityEngine.UI;

public class tmp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.Find("Canvas/Button").GetComponent<Button>().onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene("SpMenu"));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
