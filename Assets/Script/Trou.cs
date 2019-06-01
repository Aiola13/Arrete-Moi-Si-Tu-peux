using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trou : MonoBehaviour {

    public bool isTraped;
    public float[] tabLuckOfChoice = new float[2];

    public Tapette tapette;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool getIsTrap() {

        return this.isTraped;
    }

    public void setIsTrap(bool isTraped_) {

        this.isTraped = isTraped_;
    }

    public Tapette setTapette(){

        return this.tapette;
    }

    public void setTapette(Tapette tapette_){

        this.tapette = tapette_;
    }
}
