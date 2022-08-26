using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject movementPosition;
    public GameObject[] sockets;
    public int emptySocket;
    public List<GameObject> _circles = new List<GameObject>();
    [SerializeField] private GameManager _gameManager;

    int circleCompletedNumber;
    public GameObject GiveTheTopCircle()
    {

        return _circles[_circles.Count - 1];
    }

    public GameObject GiveFreeSocket()
    {
        return sockets[emptySocket];
    }

    public void SocketChange(GameObject deletedObject)
    {
        _circles.Remove(deletedObject);
        if (_circles.Count != 0)
        {
            emptySocket--;
            _circles[_circles.Count - 1].GetComponent<Circle>().movement = true;
        }
        else
        {
            emptySocket = 0;
        }
    }
    public void CheckCircleColor()
    {
        if(_circles.Count==4)
        {
            string color = _circles[0].GetComponent<Circle>().color;

            foreach (var item in _circles)
            {
                
                if(color== item.GetComponent<Circle>().color)
                {
                    circleCompletedNumber++;
                }
            }
            if (circleCompletedNumber == 4)
            {
                _gameManager.StandCompleted();
                CompletedStandOperations();
            }
            else
            {
                circleCompletedNumber = 0;
            }
        }
    }

    void CompletedStandOperations()
    {
        foreach (var item in _circles)
        {
            item.GetComponent<Circle>().movement = false;

            Color32 standColor = item.GetComponent<MeshRenderer>().material.GetColor("_Color");
            standColor.a = 150;
            item.GetComponent<MeshRenderer>().material.SetColor("_Color",standColor);
            gameObject.tag = ("CompletedStand");
        }
    }
}
