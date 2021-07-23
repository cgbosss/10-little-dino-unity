using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private PlayerCtrl m_playerCtrl;

	public Rigidbody2D r_rb2d;
	private Vector2 _moveAxis;
	public float speed;
	public float jumpTime;
	private bool jumpInAir;
	public float moveHorizontal;
	public float moveVertical;

	// Start is called before the first frame update
	void Start()
    {
		r_rb2d = gameObject.GetComponent<Rigidbody2D>();
		Debug.Log(r_rb2d);
		jumpInAir = false;
    }

	private void Awake()
	{
		m_playerCtrl = new PlayerCtrl(); //New Class Object for the Controls Object
	}

	private void OnEnable()
	{
		m_playerCtrl.PlayerMove.Enable();
		Debug.Log(m_playerCtrl + " _Player Controls Enabled ");
		m_playerCtrl.PlayerMove.Jump.performed += JumpPerformed;
		m_playerCtrl.PlayerMove.Movement.performed += MovePerformed;
	}

	private void MovePerformed(InputAction.CallbackContext context)
	{
		_moveAxis = context.ReadValue<Vector2>();
		Debug.Log("Movement Performed " + _moveAxis);
		//throw new NotImplementedException();

	}

	//Player Jump Code goes here
	private void JumpPerformed(InputAction.CallbackContext obj)
	{

		//Set Player in air true only turn off whenit touch the ground
		jumpInAir = true;
		Vector2 position;
		position = gameObject.transform.position;

		Debug.Log("Jump Player Start");
		//throw new NotImplementedException(); // Remove this during the Auto Complete

		//Make sure Jump Key is pressed onced per Jump.
		if( jumpInAir == true) 
		{
			
			Debug.Log("Jump Air " + jumpInAir);
			r_rb2d.velocity = transform.up * jumpTime;
			//r_rb2d.AddForce(transform.up * jumpTime, ForceMode2D.Impulse);
			Debug.Log(position);
		}
		else
		{
			jumpInAir = false;
			jumpTime = 0;
			Debug.Log("Reset Jump in Air" + jumpInAir);
		}


	}

	private void OnDisable()
	{
		m_playerCtrl.PlayerMove.Disable();
	}


	void FixedUpdate()
	{
		//Perform the Player Movement here after getting the input.
		//Vector2 PlayerMove = new Vector2(moveHorizontal, moveVertical);
		//r_rb2d.AddForce(PlayerMove * (_moveAxis.x + moveHorizontal));

		Vector2 PlayerMove = new Vector2(_moveAxis.x, _moveAxis.y);

		r_rb2d.AddForce(transform.right * (PlayerMove * moveHorizontal), ForceMode2D.Impulse);
		//Debug.Log(PlayerMove + " Player Move FixUpdate " + r_rb2d.position + " _Time_" + Time.deltaTime);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

}
