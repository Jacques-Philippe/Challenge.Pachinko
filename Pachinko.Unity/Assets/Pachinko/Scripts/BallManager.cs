using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BallPrefab;


    private Vector3 StartingPosition = new Vector3(0f, 4.0f, 0.0f);

    private GameObject currentBall = null;

    [SerializeField]
    private Transform borderLeft;
    [SerializeField]
    private Transform borderRight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SetUpBall");
    }

    private IEnumerator SetUpBall()
    {
        currentBall = GameObject.Instantiate(BallPrefab, position: StartingPosition, rotation: this.transform.rotation, parent: this.transform);
        currentBall.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitUntil(() =>
        {
            var ballX = BallXPositionFromMouseX(borderLeft, borderRight);
            var originX = borderLeft.position.x;
            //convert mousePositionX into a position

            currentBall.transform.position = new Vector3(originX + ballX, StartingPosition.y, StartingPosition.z);

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
        currentBall.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    /// <summary>
    /// Return the ball's x coordinate given where the mouse is on the screen
    /// </summary>
    /// <param name="distanceBetweenBorders">the distance between the borders on the left and right sides of the screen</param>
    /// <returns></returns>
    private float BallXPositionFromMouseX(Transform borderLeft, Transform borderRight)
    {
        //mouse position is in pixels, given with respect to Screen.width
        //we need to express this measure with respect to our distance between our two borders
        float distanceBetweenBorders = (borderLeft.transform.position - borderRight.transform.position).magnitude;
        float ratio = distanceBetweenBorders / Screen.width;
        return Input.mousePosition.x * ratio;
    }
}
