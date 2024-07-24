using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighbourSquareCollector : MonoBehaviour
{
    public bool squareIsUp;
    public bool squareIsDown;
    public bool squareIsLeft;
    public bool squareIsRight;

    public GameObject squareUp;
    public NeighborSquareDetector neighborSquareDetectorUP;

    public GameObject squareDown;
    public NeighborSquareDetector neighborSquareDetectorDOWN;
    public GameObject squareLeft;
    public NeighborSquareDetector neighborSquareDetectorLEFT;
    public GameObject squareRight;
    public NeighborSquareDetector neighborSquareDetectorRIGHT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (neighborSquareDetectorUP.hasNeighbourSquare)
        {
            squareIsUp = true;
        }
        else squareIsUp = false;

        if (neighborSquareDetectorDOWN.hasNeighbourSquare)
        {
            squareIsDown = true;
        }
        else squareIsDown = false;

        if (neighborSquareDetectorLEFT.hasNeighbourSquare)
        {
            squareIsLeft = true;
        }
        else squareIsLeft = false;

        if (neighborSquareDetectorRIGHT.hasNeighbourSquare)
        {
            squareIsRight = true;
        }
        else squareIsRight = false;
    }
}
