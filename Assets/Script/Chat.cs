using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chat : MonoBehaviour {

    static public int score;
    public Tapette trap;
    public GameObject prefabTapette;
    public GameObject prefabFromage;
    public Fromage[] tabFromage = new Fromage[2];
    public GameObject[] tabCanvasFromage = new GameObject[2];
    public GameObject canvasTapette;
    public GameObject textMesh;
    public uint nbFromage;
    public Piece currentPiece;
    public GameObject camera;
    public Reference reference;
    public float moveSpeed = 5.0f;
    private Animator animatorChat;
    public GameObject sphereTolook;
    static public Trou currentTrou;
    static public GameObject currentTapette;    //Pour destroy lors du ramassage
    static public GameObject currentFromage;    //Pour destroy lors du ramassage
    static public GameObject currentObjet;
    static public bool canPoseTapette;  //Poser devant un trou
    static public bool canGetTapette;
    static public bool canGetFromage;
    static public bool canReparer;
    private IEnumerator coroutine;
    private bool wait = true;

    private Rigidbody rb;
    
	// Use this for initialization
	void Start () {
        score = 0;
        tabCanvasFromage[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
        tabCanvasFromage[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
        textMesh.GetComponent<TextMeshProUGUI>().text = "Score : " + score.ToString();

        trap = prefabTapette.GetComponent<Tapette>();
        rb = GetComponent<Rigidbody>();

        animatorChat = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Dash1")) {
            Ramasser();
        }

		if (Input.GetButtonDown("Tir1")) {
            PoserFromage();
        }

        if (Input.GetButtonDown("Block1")) {
            PoserTapette();
        }

        if (Input.GetButtonDown("Cac1") && canReparer) {
            
            coroutine = reparerObjet(currentObjet.GetComponent<Objet>().timeForFixing);
            StartCoroutine(coroutine);
        }

        if (Input.GetButtonUp("Cac1")) {
            
            if(canReparer){

                StopCoroutine(coroutine);
            }
        }
	}

    void FixedUpdate() {

        float cordX = Input.GetAxis("Horizontal1");
        float cordZ = Input.GetAxis("Vertical1");

            if(cordX !=0 || cordZ !=0){

                animatorChat.SetBool("Run", true);
            }
            else{

                animatorChat.SetBool("Run", false);
            }

            transform.Translate(cordX * moveSpeed, 0f, -cordZ * moveSpeed, Space.World);
            sphereTolook.transform.position = new Vector3(transform.position.x + cordX, transform.position.y, transform.position.z - cordZ);
            transform.LookAt(sphereTolook.transform.position);

        camera.transform.LookAt(this.transform.position);
    }

    IEnumerator animatorPicked(){

        yield return new WaitForSeconds(1);        
        animatorChat.SetBool("Picked", false);
    }

    IEnumerator animatorCan(){

        yield return new WaitForSeconds(1);        
        animatorChat.SetBool("Can", false);
    }

    public void Ramasser() {

        if (canGetTapette && trap == null) {

            animatorChat.SetBool("Picked", true);
            StartCoroutine(animatorPicked());
            canvasTapette.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            trap = prefabTapette.GetComponent<Tapette>(); 
            Destroy(currentTapette);


            if (!canPoseTapette && currentTrou != null) {

                currentTrou.isTraped = false;
                currentTrou.setTapette(null);
            }
        }
        else{

            animatorChat.SetBool("Can", true);
            StartCoroutine(animatorCan());
        }

        if(wait){

            if (canGetFromage && nbFromage < 2) {

                wait = false;
                StartCoroutine(waiforFromage());
                animatorChat.SetBool("Picked", true);
                StartCoroutine(animatorPicked());
     
                tabCanvasFromage[nbFromage].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                tabFromage[nbFromage] = prefabFromage.GetComponent<Fromage>();
                Destroy(currentFromage);
                nbFromage++;
                
                for(int i=0; i<reference.tabPiece.Length; i++){

                    if(currentPiece != null){

                        if(currentPiece == reference.tabPiece[i]){

                            currentPiece.setLuckOfChoice(currentPiece.tabLuckOfChoice[1] - Fromage.LuckInfluence);
                        }
                        else{

                            reference.tabPiece[i].setLuckOfChoice(reference.tabPiece[i].tabLuckOfChoice[1] + (Fromage.LuckInfluence/2));
                        }
                    }
                }
            }
        }
    }

    IEnumerator waiforFromage(){

        yield return new WaitForSeconds(1);
        wait = true;
    }

    public void PoserFromage() {
        
        if (nbFromage != 0 && currentPiece != null) {

            Instantiate(tabFromage[nbFromage - 1], gameObject.transform.position, Quaternion.identity);
            tabCanvasFromage[nbFromage - 1].GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
            nbFromage--;

            for(int i=0; i<reference.tabPiece.Length; i++){

                if(currentPiece.transform.parent.name == reference.tabPiece[i].transform.parent.name){

                    reference.tabPiece[i].setLuckOfChoice(currentPiece.tabLuckOfChoice[1] + Fromage.LuckInfluence);
                }
                else{

                    reference.tabPiece[i].setLuckOfChoice(reference.tabPiece[i].tabLuckOfChoice[1] - (Fromage.LuckInfluence/2));
                }
            }
        }
    }

    public void PoserTapette() {

        if (canPoseTapette && trap != null) {

            canvasTapette.GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
            Instantiate(prefabTapette, gameObject.transform.position, Quaternion.identity);
            currentTrou.isTraped = true;
            canPoseTapette = false;
            currentTrou.setTapette(trap);
            trap = null;
        }
    }

    IEnumerator reparerObjet(float timeWait_) {
        
        yield return new WaitForSeconds(timeWait_);
        currentObjet.GetComponent<Objet>().isBroken = false;
        currentObjet.GetComponent<Objet>().nbTimesBroken++;

        score += 5;
        StopCoroutine(currentObjet.GetComponent<Objet>().coroutineTempsReparation);
        

        if(currentObjet.GetComponent<Objet>().nbTimesBroken > 2){

            currentObjet.GetComponent<Objet>().setIsDestroy(true);
            score -= 2;
        }
        textMesh.GetComponent<TextMeshProUGUI>().text = "Score : " + score.ToString();
        
    }

    public Tapette getTrap() {

        return this.trap;
    }

    public void setTrap(Tapette trap_) {

        this.trap = trap_;
    }

    public uint getNbFromage() {

        return this.nbFromage;
    }

    public void setNbFromage(uint nbFromage_) {

        this.nbFromage = nbFromage_;
    }

    public void setCamera(GameObject camera_){

        camera.transform.position = camera_.transform.position;
    }

    public Piece getCurrentPiece(){

        return this.currentPiece;
    }

    public void setCurrentPiece(Piece piece_){

        this.currentPiece = piece_;
    }
}
