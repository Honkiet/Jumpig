using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

	private AudioSource _audioSource;
	// Start is called before the first frame update
	private Rigidbody2D myBody;

	private SpriteRenderer spriteRenderer;
	private float move_Speed = 3.5f;
	private bool direction;

	private Animator anim;

	private float jump_Force = 5f;


	void Awake()
	{
		myBody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		_audioSource = GetComponent<AudioSource>();
	}
    private void Update()
    {
		Move();
		HandleAnimations();
       
	}

    private void HandleAnimations()
    {
        if(myBody.velocity.y > 0.1)
        {
			anim.SetBool("Falling", false);
			anim.SetBool("Jumping", true);
        }
		if(myBody.velocity.y < -0.1)
        {
			anim.SetBool("Jumping", false);
			anim.SetBool("Falling", true);	
        }
    }

    void Move()
	{
		if (direction)
		{
			myBody.velocity = new Vector2(-move_Speed, myBody.velocity.y);
		}
		else
		{
			myBody.velocity = new Vector2(move_Speed, myBody.velocity.y);
		}

	}

	void Jump()
    {
		
		myBody.velocity = new Vector2(myBody.velocity.x, jump_Force);
		_audioSource.Play();

	}

    private void OnCollisionEnter2D(Collision2D target)
    {
		if (target.gameObject.tag == "Border")
		{

			direction = !direction;
			spriteRenderer.flipX = !direction;
			Jump();
		}
		if (target.gameObject.tag == "Border2")
		{

			direction = !direction;
			spriteRenderer.flipX = !direction;
			
		}

		if (target.gameObject.tag == "Ground")
        {
			anim.SetTrigger("OnGround");
        }
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		

		if (target.tag == "jump")
		{

			Jump();
		}

		if (target.tag == "Diamond")
		{



			//GameplayController.instance.DisplayScore(0, 1);

			StartCoroutine(DisableAfterTime(target));
			target.GetComponent<Animator>().SetTrigger("Collected");

		}


		if (target.tag == "Goal")
		{

				SceneManager.LoadScene(2);
			
		}
		if (target.tag == "Goal2")
		{

			SceneManager.LoadScene(3);

		}

		if (target.tag == "spike")
		{
			
			SceneManager.LoadScene(1);

		}

		if (target.tag == "spike2")
		{
			SceneManager.LoadScene(2);

		}
	}

	IEnumerator DisableAfterTime(Collider2D target)
	{
		yield return new WaitForSeconds(0.2f);

		target.gameObject.SetActive(false);
	}


}
