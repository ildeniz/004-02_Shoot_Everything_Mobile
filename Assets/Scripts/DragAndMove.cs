using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMove : MonoBehaviour
{
    [SerializeField] float offset = 1f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float diffXPos;
    float diffYPos;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    void OnMouseDown()
    {
        var mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        var mousePosY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        diffXPos = transform.position.x - mousePosX;
        diffYPos = transform.position.y - mousePosY;
    }

    void OnMouseDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Clamp(mousePos.x + diffXPos, xMin, xMax), Mathf.Clamp(mousePos.y + diffYPos, yMin, yMax));
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        //DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (other.GetComponent<BossBehaviour>())
        {
            PushBack();
        }
    }

    private void PushBack()
    {
        var currentPosition = transform.position;
        transform.position = new Vector2(currentPosition.x, currentPosition.y + pushBackValue);
    }
    */

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + offset;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - offset;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + offset;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - offset;
    }
}
