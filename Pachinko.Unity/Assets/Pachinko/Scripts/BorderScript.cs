using Assets.Pachinko.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var commoner = collision.gameObject.GetComponent<CommonerStateManager>();
        if (commoner != null)
        {
            commoner.TurnAround();
        }
    }
}
