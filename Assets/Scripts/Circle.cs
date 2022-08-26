using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public GameObject _whichStand;
    public GameObject _whichCircleSocket;
    public bool movement;
    public string color;
    public GameManager _gameManager;

    GameObject movementPosition;
    GameObject standToGo;

    bool selected, posChange, socketSet, socketBack;
    int circleNumber;

    public void MovementControl(string duty, GameObject Stand = null, GameObject socket = null, GameObject targetObject = null)
    {
        switch (duty)
        {
            case "Selection":
                movementPosition = targetObject;
                selected = true;
                break;
            case "PositionChange":
                standToGo = Stand;
                _whichStand = socket;
                movementPosition = targetObject;
                posChange = true;
                break;
            case "BackToSocket":
                socketBack = true;
                break;
          
        }
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if (selected)
        {
            transform.position = Vector3.Lerp(transform.position, movementPosition.transform.position, .2f);
            if (Vector3.Distance(transform.position, movementPosition.transform.position) < .10f)
            {
                selected = false;
            }
        }
        if (posChange)
        {
            transform.position = Vector3.Lerp(transform.position, movementPosition.transform.position, .2f);
            if (Vector3.Distance(transform.position, movementPosition.transform.position) < .10f)
            {
                posChange = false;
                socketSet = true;
            }
        }
        if (socketSet)
        {
            transform.position = Vector3.Lerp(transform.position, _whichStand.transform.position, .2f);
            if (Vector3.Distance(transform.position, _whichStand.transform.position) < .10f)
            {
                transform.position = _whichStand.transform.position;
                socketSet = false;

                _whichStand = standToGo;

                if (_whichStand.GetComponent<Stand>()._circles.Count > 1)
                {
                    circleNumber = _whichStand.GetComponent<Stand>()._circles.Count;
                    _whichStand.GetComponent<Stand>()._circles[circleNumber - 2].GetComponent<Circle>().movement = false;
                }
                _gameManager.isMovement = false;
            }
        }
        if (socketBack)
        {
            transform.position = Vector3.Lerp(transform.position, _whichStand.transform.position, .2f);
            if (Vector3.Distance(transform.position, _whichStand.transform.position) < .10)
            {
                Debug.Log("work");
                transform.position = _whichStand.transform.position;
                socketBack = false;
                _gameManager.isMovement = false;
            }
        }
    }
}
