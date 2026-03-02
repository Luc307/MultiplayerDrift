using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour
{
    private GameObject[] tires;
    private bool moveDown = false;
    private bool jumpReady = false;
    private Dictionary<GameObject, Vector3> tireBasePositionDic = new Dictionary<GameObject, Vector3>();


    IEnumerator MoveTire(GameObject tire, Vector3 targetPos, bool finsihUp, float duration)
    {
        if (finsihUp)
        {
            while (moveDown)
            {
                yield return null;
            }
        }

        float elapsed = 0f;
        Vector3 startPos = tire.transform.localPosition;

        while (elapsed < duration)
        {
            tire.transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (finsihUp)
        {
            tire.transform.localPosition = targetPos;
        }
        else
        {
            moveDown = false;
        }
    }
    IEnumerator SetJumpReadyTrue(int time)
    {
        yield return new WaitForSeconds(time);
        jumpReady = true;
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
        StartCoroutine(SetJumpReadyTrue(5));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (jumpReady)
            {
                jumpReady = false;
                StartCoroutine(SetJumpReadyTrue(2));
                moveDown = true;

                //move down
                foreach (GameObject tire in tires)
                {
                    StartCoroutine(MoveTire(tire, tire.transform.localPosition + Vector3.down, false, 0.25f));
                }

                //move up
                foreach (GameObject tire in tires)
                {
                    StartCoroutine(MoveTire(tire, tireBasePositionDic[tire], true, 1));
                }
            }
        }
    }
}