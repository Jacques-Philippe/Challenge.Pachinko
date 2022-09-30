using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BallPrefab;

    private Vector3 StartingPosition = new Vector3(0f, 4.0f, 0.0f);

    private GameObject currentBall = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IEnumerator SetUpBall()
    {
        currentBall = GameObject.Instantiate(BallPrefab, position: StartingPosition, rotation: this.transform.rotation, parent: this.transform);
        yield return new WaitUntil(() =>
        {
            return Input.GetMouseButtonDown(0);
        });
        ReleaseBall();
        yield return new WaitUntil(() =>
        {
            return this.currentBall == null;
        });
        yield return SetUpBall();
    }

    private void ReleaseBall()
    {

    }
}
