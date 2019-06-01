using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapette : MonoBehaviour {

    public bool isActive;
    public bool isBroken;
    public int nbOfUse;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool getIsActive() {

        return this.isActive;
    }

    public void setIsActive(bool isActive_) {

        this.isActive = isActive_;
    }

    public bool getIsBroken() {

        return this.isBroken;
    }

    public void setIsBroken(bool isBroken_) {

        this.isBroken = isBroken_;
    }

    public int getNbOfUse() {

        return this.nbOfUse;
    }

    public void setNbOfUse(int nbOfUse_) {

        this.nbOfUse = nbOfUse_;
    }
}
