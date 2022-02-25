using UnityEngine;

public class CharactorMove_Joystick : MonoBehaviour
{
    public SimpleTouchController MoveController;
    public FollowCamera followCamera;
    public GameObject Charactor;
    public float speed;
    Effect_StepSmoke effect_StepSmoke;
    SimpleTouchController.TouchStateDelegate TouchStateEvent00;
    CharactorEffectController charactorEffectController;
    private bool faceRight;
    private Vector2 movever;
    private Animator animator;
    private bool moveon;
    private int move = Animator.StringToHash("Move");
    private void Awake()
    {       
        TouchStateEvent00 = new SimpleTouchController.TouchStateDelegate(MovePresentContoll);
        MoveController.TouchStateEvent += TouchStateEvent00;
        animator = Charactor.transform.GetComponentInChildren<Animator>();
        charactorEffectController = Charactor.GetComponent<CharactorEffectController>();
        effect_StepSmoke = Charactor.transform.GetComponentInChildren<Effect_StepSmoke>();
    }
    // Update is called once per frame

    void FixedUpdate()
    {
        movever = MoveController.GetTouchPosition;
        this.transform.localPosition += new Vector3(movever.x * speed*2, movever.y * speed, 0);
        if (moveon)
        {
            FaceControll();
            followCamera.CameraMoveOn(this.transform.localPosition);
            CharactorRange();
            followCamera.CameraMove(this.transform.localPosition);
            Charactor.transform.localPosition = this.transform.localPosition;
        }
    }

    void MovePresentContoll(bool touchPresent)
    {
        moveon = touchPresent;
        animator.SetBool(move, touchPresent);
        effect_StepSmoke.Play = touchPresent;
    }
    
    void FaceControll()
    {
        if (movever.x < 0)
        {
            faceRight = false;

        }
        if (movever.x > 0)
        {
            faceRight = true;

        }
        switch (faceRight)
        {
            case false:
                Charactor.transform.localScale = new Vector3(1, 1, 1);
                break;
            case true:
                Charactor.transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
    }
    void CharactorRange()
    {
        int MaxX = 500;
        int MinX = -500;
        int MaxY = 600;
        int MinY = -500;
        if (moveon)
        {
            if (transform.localPosition.x < MinX)
            {
                this.transform.localPosition = new Vector3(MinX, this.transform.localPosition.y, 0);

            }
            if (transform.localPosition.x > MaxX)
            {
                this.transform.localPosition = new Vector3(MaxX, this.transform.localPosition.y, 0);

            }
             if (transform.localPosition.y < MinY)
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, MinY, 0);

            }
            if (transform.localPosition.y > MaxY)
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, MaxY, 0);
            }          
        }
    }
}
