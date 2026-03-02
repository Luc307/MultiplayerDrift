using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<CarActions>().StartCoroutine("OnFinishTrigger");
        //wird erst in car gecheckt, ob schon gefinished, wegen checkpoint abgleich
    }
}
