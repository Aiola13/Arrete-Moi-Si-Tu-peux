using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Trou[] tabHole = new Trou[2];
    public float[] tabLuckOfChoice = new float[2];
    public Objet[] tabObject = new Objet[4];
    public GameObject camera;

    void Update()
    {
        
    }

    public void setLuckOfChoice(float luckOfChoice_){

        tabLuckOfChoice[1] = luckOfChoice_;
    }

    public GameObject getCamera(){

        return camera;
    }
}
