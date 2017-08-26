using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Controler : Player_Animations
{
    #region Public Coomponets
    public GameObject PlayerPanel;
    public GameObject inventoryPanel;
    public PlayerHUD sprint;
    public CameraController Camera;
    public PlayerCharacter pc;
    public Targeting targetOn;
    public GameObject Anchor { get; set; }
    public Rigidbody rb;
    [System.NonSerialized]
    public AnimatorStateInfo stateInfo;
    public bool MovementDirty { get; set; }
    #endregion

    #region Public Variables
    //public int Damage = 10;
    public float xSpeed = 50.0f;
    public float ySpeed = 50.0f;
    public float rotSpeed = 20f;
    public float meshMoveSpeed = 0.1f;
    public float JumpSpeed = 250.0f;
    public bool panelisShow = false;
    public bool inventoryisShow = false;

    private bool grounded = false;
    private bool falling = false;
    private float TimetoFall = 0;
    private float distanceToGround = 0;
    #endregion

    #region Serializable Variables
    [System.NonSerialized]
    public float animSpeed = 1.5f;
    [System.NonSerialized]
    public float gravity = 9.8f;
    [System.NonSerialized]
    public float maxSlope = 60;
    [System.NonSerialized]
    public float CliffHandMatchStarts = 0.17f;
    [System.NonSerialized]
    public float CliffHandMatchEnd = 0.27f;
    [System.NonSerialized]
    public float CliffFootMatchStarts = 0.25f;
    [System.NonSerialized]
    public float CliffFootMatchEnds = 0.65f;
    [System.NonSerialized]
    public bool inCombat = false;
    [System.NonSerialized]
    public bool inCliffRange = false;
    [System.NonSerialized]
    public bool running = false;
    [System.NonSerialized]
    public bool InCliffRange = false;
    [System.NonSerialized]
    public bool InCliffAnimation = false;
    [System.NonSerialized]
    public bool DoCliffAnimation = false;
    #endregion

    #region Keys
    public KeyCode Jump { get; set; }
    public KeyCode Forward { get; set; }
    public KeyCode Backward { get; set; }
    public KeyCode StrafeLeft { get; set; }
    public KeyCode StrafeRight { get; set; }
    public KeyCode RotateLeft { get; set; }
    public KeyCode RotateRigth { get; set; }
    public KeyCode DranWeaponKey { get; set; }
    public KeyCode Target { get; set; }
    public KeyCode Shild { get; set; }
    public KeyCode Rum { get; set; }

    void Awake()
    {
        Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        Forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        Backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        StrafeLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("strafeLeftKey", "A"));
        StrafeRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("starfeRightKey", "D"));
        RotateLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rotateLeft", "Q"));
        RotateRigth = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rotateRigth", "E"));
        DranWeaponKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dranWeapon", "V"));
        Target = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("target", "Tab"));
        Shild = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Shild", "LeftControl"));
        Rum = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Rum", "LeftShift"));
    }
    #endregion

    void Start()
    {
        MovementDirty = false;
    }

    void FixedUpdate()
    {
        MovimentSistem();
        AnimationSpecial();
    }

    public void MovimentSistem()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb = GetComponent<Rigidbody>();
        float mouseH = Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;          //Mouse Axis

        if (hasWeapon)
        {
            anim.SetBool("HasWeapon", true);
        }
        else {
            anim.SetBool("HasWeapon", false);
        }

        if (isBloking == false)
        {
            if (Input.GetKey(Forward)) v++;

            if (Input.GetKey(Backward)) v--;

            if (Input.GetKey(StrafeRight)) h++;

            if (Input.GetKey(StrafeLeft)) h--;

            anim.SetFloat("Speed", v);                                                  //Axis Direction			
            anim.SetFloat("Direction", h);                                              //		
            anim.speed = animSpeed;                                                   //Aniamtion Speed
            Bloking();

            if (v > 0)                                                              //Move Forward
            {
                Moving();

                if (running == false)
                    gameObject.transform.Translate(0, 0, v * meshMoveSpeed * Time.deltaTime);
                else
                    gameObject.transform.Translate(0, 0, v * meshMoveSpeed * 4 * Time.deltaTime);
            }
            else
                NoMoving();

            if (v < 0)                                                                  //Move Backward
            {
                Moving();

                if (running == false)
                    gameObject.transform.Translate(0, 0, v * meshMoveSpeed * Time.deltaTime);
                else
                    gameObject.transform.Translate(0, 0, v * meshMoveSpeed * 4 * Time.deltaTime);
            }

            if (h > 0)                                                              //Move left
            {
                Moving();
                if (running == false)
                    gameObject.transform.Translate(h * meshMoveSpeed * Time.deltaTime, 0, 0);
                else
                    gameObject.transform.Translate(h * meshMoveSpeed * 4 * Time.deltaTime, 0, 0);
            }

            if (h < 0)                                                              //Move Rigth
            {
                Moving();
                if (running == false)
                    gameObject.transform.Translate(h * meshMoveSpeed * Time.deltaTime, 0, 0);
                else
                    gameObject.transform.Translate(h * meshMoveSpeed * 4 * Time.deltaTime, 0, 0);
            }


            if (Input.GetKey(KeyCode.LeftShift) && v > 0 && sprint.Fatigued == false ||
                Input.GetKey(KeyCode.LeftShift) && v < 0 && sprint.Fatigued == false ||
                Input.GetKey(KeyCode.LeftShift) && h < 0 && sprint.Fatigued == false ||
                Input.GetKey(KeyCode.LeftShift) && h > 0 && sprint.Fatigued == false)
            {//Faz correr
                Running();
                NoMoving();
                running = true;
                sprint.AdjusCurrentSprint(-1);
            }
            else
            {
                NoRunning();
                sprint.AdjusCurrentSprint(+0.5f);
            }

            if (sprint.Fatigued == true)
            {
                NoRunning();
            }

            if (Input.GetKeyDown(Jump) && grounded == true)
            {
                Jumping();
                rb.AddForce(0, JumpSpeed, 0);
            }
        }

        // A culpa so eh sua se seu nome tiver no doc da classe
        if (Input.GetKeyDown(Target))
        {//Seleciona Target
            targetOn.TargetSystemButton();
            targetOn.targetactive = true;
            //se prepare para o sentimento de fracasso eminente
        }

        if (Input.GetMouseButton(1))
        {//rotaciona o personagem e a camera
            gameObject.transform.RotateAround(transform.position, transform.up, mouseH);
            gameObject.transform.Rotate(0, mouseH, 0);
            Camera.RotateCameraAndCharacter();
            Camera.Troca.olhacam = false;
        }
        else
        {
            Camera.Troca.olhacam = true;
        }

        if (Input.GetMouseButton(0) && inventoryisShow == false && panelisShow == false)                                                //Rotação livre da camera
        {
            Camera.RotateCamera();
            Camera.Troca.olhacam = false;
        }
        else
        {
            Camera.Troca.olhacam = true;
        }
    }

    public void ActiveTrigger()
    {
        if (hasWeapon)
            pc.wm_R.GetComponentInChildren<Collider>().enabled = true;
    }

    public void DeActiveTrigger()
    {
        if (hasWeapon)
            pc.wm_R.GetComponentInChildren<Collider>().enabled = false;
    }

    #region Climb Re Do
    void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
            {
                grounded = true;
                falling = false;
                TimetoFall = 0;
                anim.SetBool("Falling", false);
            }
        }
    }

    void OnCollisionExit()
    {
        grounded = false;
        falling = true;
        TimetoFall++;
    }

    public void CliffJump()
    {
        anim.applyRootMotion = true;
        anim.SetTrigger("CliffJump");
    }

    public void ResetCliffParameters()
    {
        InCliffRange = false;
        InCliffAnimation = false;
        //InCliffJump = false;
        ResetRootMotion();
    }

    public void ResetRootMotion()
    {
        anim.applyRootMotion = false;
    }

    private void CheckCliffClimb()
    {
        if (Anchor == null)
            return;

        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layers.Cliff Climb"))
        {
            // Debug.DrawLine
            anim.MatchTarget(Anchor.transform.position,
                             Anchor.transform.rotation,
                             AvatarTarget.RightHand,
                             new MatchTargetWeightMask(new Vector3(0, 1, 0), 0),
                             CliffFootMatchStarts,
                             CliffHandMatchEnd);

            anim.MatchTarget(Anchor.transform.position,
                             Anchor.transform.rotation,
                             AvatarTarget.RightFoot,
                             new MatchTargetWeightMask(new Vector3(0, 1, 0), 0),
                             CliffFootMatchStarts,
                             CliffFootMatchEnds);
        }
    }
    #endregion

    void StopJumping()
    {
        anim.SetBool("Jumping", false);
    }

    private void AnimationSpecial()
    {
        if (pc.Weapon_Holder_R.transform.childCount != 0)
        {
            hasWeapon = true;

            if (Input.GetMouseButtonDown(0) && targetOn.targetactive == true && targetOn.EH.dead == false && hasWeapon == true)
            {
                if (hasWeapon)
                {
                    if (WeaponT2h)
                        Attacking_TwoHand();

                    if (!WeaponT2h)
                        Attacking_OneHand_R();
                }

                inCombat = true;
            }
        }
        else
            hasWeapon = false;

        if (distanceToGround >= 2)
            falling = true;

        if (falling == true)
        {
            Falling();
            DeActiveTrigger();
        }

        if (distanceToGround >= 2)
        {
            falling = true;
        }

        if (!InCliffAnimation)
        {
            if (Input.GetKey(RotateLeft))//Key rotation Left
            {
                transform.Rotate(Vector3.down * Time.deltaTime * 100.0f);
                if ((Input.GetAxis("Vertical") == 0f) && (Input.GetAxis("Horizontal") == 0))
                    TurningLeft();
            }
            else
                NoTurningLeft();

            if (Input.GetKey(RotateRigth))//Key rotation Rigth
            {
                transform.Rotate(Vector3.down * Time.deltaTime * -100.0f);
                if ((Input.GetAxis("Vertical") == 0f) && (Input.GetAxis("Horizontal") == 0))
                    TurningRight();
            }
            else
                NoTurningRight();
        }

        if (inCombat)
            InCombat();
        else
            NotInCombat();

        if (Input.GetKeyDown("1"))
        {
            if (anim.GetInteger("CurrentAction") == 0)
            {
                anim.SetInteger("CurrentAction", 1);
            }
            else if (anim.GetInteger("CurrentAction") == 1)
            {
                anim.SetInteger("CurrentAction", 0);
            }
        }

        if (Input.GetKeyDown("2"))
        {
            if (anim.GetInteger("CurrentAction") == 0)
            {
                anim.SetInteger("CurrentAction", 2);
            }
            else if (anim.GetInteger("CurrentAction") == 2)
            {
                anim.SetInteger("CurrentAction", 0);
            }
        }

        if (Input.GetKeyDown("3"))
        {
            anim.Play("wave");
        }

        if (Input.GetKeyDown(DranWeaponKey))
        {
            if (!hasWeapon)
                DranWeapon();
            else
                UnDrawWeapon();
        }

        if (Input.GetKey(Shild) && hasWeapon)
        {
            isBloking = true;
        }
        else
        {
            isBloking = false;
        }
    }

    public void DrawWeapon()
    {
        if (WeaponT2h)
        {
            pc.DrwHolder_BackR.transform.GetChild(0).transform.SetParent(pc.Weapon_Holder_R.transform);
            pc.Weapon_Holder_R.transform.GetChild(0).transform.position = pc.Weapon_Holder_R.transform.position;
            pc.Weapon_Holder_R.transform.GetChild(0).transform.rotation = pc.Weapon_Holder_R.transform.rotation;
        }
        else
        {
            pc.DrwHolder_L.transform.GetChild(0).transform.SetParent(pc.Weapon_Holder_R.transform);
            pc.Weapon_Holder_R.transform.GetChild(0).transform.position = pc.Weapon_Holder_R.transform.position;
            pc.Weapon_Holder_R.transform.GetChild(0).transform.rotation = pc.Weapon_Holder_R.transform.rotation;
        }
    }
    public void SaveWeapon()
    {
        if (WeaponT2h)
        {
            hasWeapon = false;
            inCombat = false;
            pc.Weapon_Holder_R.transform.GetChild(0).transform.SetParent(pc.DrwHolder_BackR.transform);
            pc.DrwHolder_BackR.transform.GetChild(0).transform.position = pc.DrwHolder_BackR.transform.position;
            pc.DrwHolder_BackR.transform.GetChild(0).transform.rotation = pc.DrwHolder_BackR.transform.rotation;
        }
        else
        {
            hasWeapon = false;
            inCombat = false;
            pc.Weapon_Holder_R.transform.GetChild(0).transform.SetParent(pc.DrwHolder_L.transform);
            pc.DrwHolder_L.transform.GetChild(0).transform.position = pc.DrwHolder_L.transform.position;
            pc.DrwHolder_L.transform.GetChild(0).transform.rotation = pc.DrwHolder_L.transform.rotation;
        }
    }
}
