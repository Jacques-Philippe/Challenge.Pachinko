using Assets.Pachinko.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var manager = collision.gameObject.GetComponent<CommonerStateManager>();
        if (manager != null)
        {
            manager.HandleTransition(new HitOnHeadTransitionEvent());
        }
    }
}
