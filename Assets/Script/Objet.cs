using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Objet : MonoBehaviour
{
    public float timeForFixing; //Temps pour réaliser la réparation
    public float timeToFix; //Limite de temps pour la réparation
    public bool isBroken;
    public bool isDestroy;
    public int nbTimesBroken; //Nombre de fois que l'objet s'est casse
    public float timeToBroke;
    public float[] tabLuckOfChoice = new float[2];

    public GameObject alertLink;
    public GameObject textMesh;
    public IEnumerator coroutineTempsReparation;
    private float j;

    void Start() {
        textMesh.GetComponent<TextMeshProUGUI>().text = "";
        alertLink.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    IEnumerator timeLeft(float maxTime_) {
        alertLink.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        for (int i = 0; i < maxTime_; i++) {
            
            j = maxTime_ - i;
            textMesh.GetComponent<TextMeshProUGUI>().text = "Temps Restant : " + j.ToString();
            yield return new WaitForSeconds(1f);
        }
        textMesh.GetComponent<TextMeshProUGUI>().text = "";
        alertLink.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    public bool getIsBroken(){

        return this.isBroken;
    }

    public void setIsBroken(bool isBroken_){

        this.isBroken = isBroken_;
        coroutineTempsReparation = timeLeft(timeToFix);
        StartCoroutine(coroutineTempsReparation);
    }

    public bool getIsDestroy(){

        return this.isDestroy;
    }

    public void setIsDestroy(bool isDestroy_) {

        this.isDestroy = isDestroy_;
    }
}
