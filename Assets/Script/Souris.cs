using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souris : MonoBehaviour
{
    public float timeToChoosePiece;
    public float timeToChooseHole;
    public float timeToChooseObject;
    public Piece pieceChosen;
    Objet objectChosen;
    public Trou holeChosen; //1er en sortie et le deuxième en sortie
    public bool isTrap;
    static public float timeToEscape;
    public uint hp; //0 1 2 ou 3 
    public GameObject reference;
    System.Random rdm;

    void Start(){
        
        rdm = new System.Random();
        choosePiece();
    }
    
    public Piece getPieceChosen(){

        return this.pieceChosen;
    }

    public void setPieceChose(Piece pieceChosen_){

        this.pieceChosen = pieceChosen_;
    }

    public Objet getObjectChosen(){

        return this.objectChosen;
    }

    public void SetObjectChosen(Objet objectChose_){

        this.objectChosen = objectChose_;
    }
    
    public Trou getHoleChosen(){

        return holeChosen;
    }

    public bool getIsTrap(){

        return this.isTrap;
    }

    public void setIsTrap(bool isTrap_){

        this.isTrap = isTrap_;
    }

    public uint getHp(){

        return this.hp;
    }

    public void setHp(uint hp_){

        this.hp = hp_;
    }

    public void choosePiece(){

        StartCoroutine(choosePiece(timeToChoosePiece));
    }

    IEnumerator choosePiece(float timeToChoosePiece_){

        yield return new WaitForSeconds(timeToChoosePiece_);
        int range = rdm.Next(100);

        for(int i=0; i<reference.GetComponent<Reference>().tabPiece.Length; i++){

            if(reference.GetComponent<Reference>().tabPiece[i].tabLuckOfChoice[0]<=range && range<reference.GetComponent<Reference>().tabPiece[i].tabLuckOfChoice[1]){
                
                pieceChosen = reference.GetComponent<Reference>().tabPiece[i].GetComponent<Piece>();
            }
        }
        chooseHole();
    }

    public void chooseHole(){

        StartCoroutine(chooseHole(timeToChooseHole));
    }

    IEnumerator chooseHole(float timeToChooseHole_){

        yield return new WaitForSeconds(timeToChooseHole_);
        int range = rdm.Next(100);

        for(int i=0; i<pieceChosen.tabHole.Length; i++){

            if(pieceChosen.tabHole[i].tabLuckOfChoice[0]<=range && range<pieceChosen.tabHole[i].tabLuckOfChoice[1]){

                holeChosen = pieceChosen.tabHole[i];
            }
        }
        chooseObject();
    }

    public void chooseObject(){

        StartCoroutine(chooseObject(timeToChooseObject));
    }

    IEnumerator chooseObject(float timeToChooseObject_){

        yield return new WaitForSeconds(timeToChooseObject_);
        int range = rdm.Next(100);

        for(int i=0; i<pieceChosen.tabObject.Length; i++){

            if(((pieceChosen.tabObject[i].tabLuckOfChoice[0]<=range) && (range<pieceChosen.tabObject[i].tabLuckOfChoice[1])) && (!(pieceChosen.tabObject[i].isBroken) && !(pieceChosen.tabObject[i].isDestroy))){
                
                objectChosen = pieceChosen.tabObject[i];
                
                if(holeChosen.getIsTrap()){

                    this.transform.position = holeChosen.gameObject.transform.position;
                }
                else{

                    objectChosen.setIsBroken(true);
                }
            }
            else{

              choosePiece();
            }
        }
    }
}
