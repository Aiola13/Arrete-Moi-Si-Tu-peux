using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (this.tag == "Trou") {
            if (other.tag == "Chat") {
                Chat.currentTrou = this.GetComponent<Trou>();

                if (!this.GetComponent<Trou>().isTraped) {
                    Chat.canPoseTapette = true;
                }
            }
        }

        if (this.tag == "Tapette") {
            if (other.tag == "Chat") {
                Chat.canGetTapette = true;
                Chat.currentTapette = this.gameObject;
            }
        }

        if (this.tag == "Fromage") {
            if (other.tag == "Chat") {
                Chat.canGetFromage = true;
                Chat.currentFromage = this.gameObject;
            }
        }

        if(this.tag== "Piece"){
            if(other.tag == "Chat"){

                if(this.transform.parent.name != "Couloir"){

                    other.GetComponent<Chat>().setCamera(this.GetComponent<Piece>().getCamera());
                    other.GetComponent<Chat>().setCurrentPiece(this.GetComponent<Piece>());
                }
                else{

                    other.GetComponent<Chat>().setCamera(this.GetComponent<Piece>().getCamera());
                    other.GetComponent<Chat>().setCurrentPiece(null);
                }
            }
        }

        if(this.tag == "Object") {

            if (other.tag == "Chat" && this.GetComponent<Objet>().isBroken && !this.GetComponent<Objet>().isDestroy) {
                Chat.canReparer = true;
                Chat.currentObjet = this.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Chat") {
            Chat.canPoseTapette = false;
            Chat.canReparer = false;
            Chat.canGetTapette = false;
            Chat.canGetFromage = false;
            Chat.currentTrou = null;
        }
    }
}
