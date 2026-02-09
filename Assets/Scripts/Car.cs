using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private GameObject[] tires;
    private bool moveDown = false;
    private int ready = 0;
    private Dictionary<GameObject, Vector3> tireBasePositionDic = new Dictionary<GameObject, Vector3>();


    IEnumerator MoveTireToBasePosition(GameObject tire)
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startPos = tire.transform.position;
        Vector3 targetPos = tireBasePositionDic[tire];

        while (elapsed < duration)
        {
            tire.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; //wartet bis zum nächsten frame
        }

        tire.transform.position = targetPos;
        ready--;
    }
    IEnumerator SetMoveDownFalse()
    {
        yield return new WaitForSeconds(0.5f);
    }

    private void Awake()
    {
        tires = GameObject.FindGameObjectsWithTag("Tire");
        foreach (GameObject tire in tires)
        {
            tireBasePositionDic.Add(tire, tire.transform.localPosition);
        }
    }
    private void Start()
    {
        Debug.LogWarning("der move down teil ist nicht sequenziell: moveDown = true und die corountine wird gestartet");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (ready == 0)
            {
                ready = 4;
                moveDown = true;
                StartCoroutine(SetMoveDownFalse());

                foreach(GameObject tire in tires)
                {
                    StartCoroutine(MoveTireToBasePosition(tire));

                    //chill weil wechsel
                }
            }
        }

        if (moveDown)
        {
            foreach (GameObject tire in tires)
            {
                tire.transform.position = Vector3.Lerp(tire.transform.position, tire.transform.position + Vector3.down * 2, Time.deltaTime);
            }
        }
    }
}