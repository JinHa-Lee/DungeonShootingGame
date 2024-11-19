using UnityEngine;

public class Potal : MonoBehaviour
{
    Transform transformPosition;


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {

        }
    }
}
