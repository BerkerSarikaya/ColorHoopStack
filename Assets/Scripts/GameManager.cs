using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject selectedObject;
    GameObject selectedStand;
    Circle _circles;
    public bool isMovement;
    public int targetStandNumber;
    int completedStandNumber;


    // Update is called once per frame
    void Update()
    {
        signalControl();
    }

    public void signalControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (selectedObject != null && selectedStand != hit.collider.gameObject)
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>();

                        if (_Stand._circles.Count != 4 && _Stand._circles.Count != 0)
                        {
                            if (_circles.color == _Stand._circles[_Stand._circles.Count - 1].GetComponent<Circle>().color)
                            {

                                selectedStand.GetComponent<Stand>().SocketChange(selectedObject);
                                _circles.MovementControl("PositionChange", hit.collider.gameObject, _Stand.GiveFreeSocket(), _Stand.movementPosition);
                                _Stand.emptySocket++;
                                _Stand._circles.Add(selectedObject);
                                _Stand.CheckCircleColor();
                                selectedObject = null;
                                selectedStand = null;

                            }
                            else
                            {
                                _circles.MovementControl("BackToSocket");
                                selectedObject = null;
                                selectedStand = null;
                            }

                        }
                        else if (_Stand._circles.Count == 0)
                        {
                            selectedStand.GetComponent<Stand>().SocketChange(selectedObject);
                            _circles.MovementControl("PositionChange", hit.collider.gameObject, _Stand.GiveFreeSocket(), _Stand.movementPosition);
                            _Stand.emptySocket++;
                            _Stand._circles.Add(selectedObject);
                            _Stand.CheckCircleColor();
                            selectedObject = null;
                            selectedStand = null;
                        }
                        else
                        {
                            Debug.Log(123);
                            _circles.MovementControl("BackToSocket");
                            selectedObject = null;
                            selectedStand = null;
                        }
                    }
                    else if (selectedStand == hit.collider.gameObject)
                    {
                        Debug.Log(412);
                        _circles.MovementControl("BackToSocket");
                        selectedObject = null;
                        selectedStand = null;
                    }
                    else
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>();
                        selectedObject = _Stand.GiveTheTopCircle();
                        _circles = selectedObject.GetComponent<Circle>();
                        isMovement = true;

                        if (_circles.movement)
                        {
                            _circles.MovementControl("Selection", null, null, _circles._whichStand.GetComponent<Stand>().movementPosition);

                            selectedStand = _circles._whichStand;
                        }
                    }
                }
            }
        }
    }

    public void StandCompleted()
    {
        completedStandNumber++;
        if(completedStandNumber ==targetStandNumber)
        {
            Debug.Log("win");
        }
    }
}
