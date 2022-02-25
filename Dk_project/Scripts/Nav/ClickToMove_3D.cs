using UnityEngine;
using PolyNav;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class ClickToMove_3D : MonoBehaviour
{	
	public BgNav bgNav;
	private GameObject Charactor;
	private bool faceRight;
	private bool ClosePos;
	public bool ClickLock;
	private PolyNavAgent _agent;
	private Animator animator;
	private int move = Animator.StringToHash("Move");
	private FollowCamera followCamera;
	private int effect_ClickCount;
	Effect_StepSmoke effect_StepSmoke;
	Effect_ClickPos effect_ClickPos;

	void Awake()
    {
		Charactor = GameObject.FindGameObjectWithTag("Player");
		followCamera = Camera.main.GetComponent< FollowCamera>();
		animator = Charactor.GetComponentInChildren<Animator>();
		effect_StepSmoke = Charactor.GetComponentInChildren<Effect_StepSmoke>();
		bgNav.Pos = Charactor.transform.localPosition;
		effect_ClickPos = bgNav.pos.GetComponentInChildren<Effect_ClickPos>();
	}
    private void Start()
    {
		for (int i = 0; i < effect_ClickPos.Click_Pos.Count; i++)
		{
			effect_ClickPos.Click_Pos[i].SetActive(false);
		}
	}

    private PolyNavAgent agent
	{
		get { return _agent != null ? _agent : _agent = GetComponent<PolyNavAgent>(); }
	}

    private void Update()
    {
		if (Input.GetMouseButtonDown(0)&&!ClickLock)
		{
			bgNav.GetInputPos();
			
			CancelInvoke("TrunClickLock");
		}
		
	}
    void LateUpdate()
	{
		if(Input.GetMouseButtonDown(0))
        {
			ClickPosEffectOn();
			Invoke(nameof(TrunClickLock), bgNav.locktime);
		}
		if (Input.GetMouseButton(0) && ClosePos == false)
		{
			agent.SetDestination(bgNav.Pos);
			
			//Debug.Log("bgNav.Pos" + bgNav.Pos);
			//Debug.Log("CharactorPos" + Charactor.transform.localPosition);
		}
		if (agent.hasPath)
		{
			Charactor.transform.localPosition = this.transform.localPosition;
			followCamera.CameraMoveOn(this.transform.localPosition);
			followCamera.CameraMove(this.transform.localPosition);
			animator.SetBool(move, true);
			effect_StepSmoke.Play = true;
			FaceControll();
			CharatorWithPos();
			
		}
		if (!agent.hasPath)
        {
			animator.SetBool(move, false);
			effect_StepSmoke.Play = false;		

		}
		if(Input.touchCount == 0 && !agent.hasPath)
        {
			bgNav.Pos = Charactor.transform.localPosition;
			ClosePos = false;
		}
	}
	void FaceControll()
	{
		if (bgNav.Pos.x < this.transform.position.x)
		{
			faceRight = false;
		}
		if (bgNav.Pos.x > this.transform.position.x)
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
	void CharatorWithPos()
    {
		float lhs = Mathf.Sqrt((Charactor.transform.position.x - bgNav.Pos.x) * (Charactor.transform.position.x - bgNav.Pos.x));
		float rhs = Mathf.Sqrt((Charactor.transform.position.y - bgNav.Pos.y) * (Charactor.transform.position.y - bgNav.Pos.y));
		float withPos = Mathf.Sqrt(lhs + rhs);		
		if (withPos < 5)
        {
			ClosePos = true;
			for (int i = 0; i < effect_ClickPos.Click_Pos.Count; i++)
			{
				effect_ClickPos.Click_Pos[i].SetActive(false);
			}
		}
	}
	void ClickPosEffectOn()
    {
		switch (ClickLock)
        {
			case true:			
				break;
			case false:
				effect_ClickPos.Click_Pos[effect_ClickCount].SetActive(true);
				effect_ClickCount++;
				if (effect_ClickCount == effect_ClickPos.Click_Pos.Count)
				{
					effect_ClickCount = 0;
				}
				ClickLock = true;
				break;
		}
	}

	void TrunClickLock()
    {
		ClickLock = false;
	}
}