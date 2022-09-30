using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var ballScript = collision.gameObject.GetComponent<BallScript>();
        if (ballScript != null)
        {
            GameObject.Destroy(ballScript.gameObject);
        }
    }
}
