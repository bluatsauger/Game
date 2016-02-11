using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float moveSpeed; 
	public float jumpHeight;
	private bool doubleJumped;
	private float moveVelocity;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;

	public Transform firePoint;
	public Transform shoot;

	public float shootDelay;
	private float shotDelayCounter;

	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;

	private Rigidbody2D myrigidbody2D;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

		myrigidbody2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}


	// Update is called once per frame
	void Update () {

        if (grounded) 
		{
            doubleJumped = false;
        }

		anim.SetBool ("Grounded", grounded);

		if (Input.GetKeyDown (KeyCode.Space) && grounded) 
		{
            Jump();
		}
        //doble
        if (Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !grounded)
        {
            Jump ();
            doubleJumped = true;
        }

		moveVelocity = 0f;

		if (Input.GetKey (KeyCode.D)) 
		{
			//GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			moveVelocity = moveSpeed;
		}

		if (Input.GetKey (KeyCode.A)) 
		{
			//GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			moveVelocity = -moveSpeed;
		}

		if (knockbackCount <= 0) {
			myrigidbody2D.velocity = new Vector2 (moveVelocity, myrigidbody2D.velocity.y);
		} else {
			if (knockFromRight) 
				myrigidbody2D.velocity = new Vector2 (-knockback, knockback);
			if (!knockFromRight) 
				myrigidbody2D.velocity = new Vector2 (-knockback, knockback);
			knockbackCount -= Time.deltaTime;
		}

		anim.SetFloat ("Speed", Mathf.Abs (myrigidbody2D.velocity.x));

		if (GetComponent<Rigidbody2D> ().velocity.x < 0) 
			transform.localScale = new Vector3 (-0.5f, 0.5f, 0.5f);
		else 
			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);


		if (Input.GetKeyDown (KeyCode.S)) {
			Instantiate (shoot, firePoint.position, firePoint.rotation);
			shotDelayCounter = shootDelay;
		}

		if (Input.GetKey (KeyCode.S)) {
			shotDelayCounter -= Time.deltaTime;
			if (shotDelayCounter <= 0) {
				shotDelayCounter = shootDelay;
				Instantiate (shoot, firePoint.position, firePoint.rotation);
			}
		}
	}

    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }
}
