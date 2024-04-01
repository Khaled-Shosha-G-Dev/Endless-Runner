using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] private float gravityModifire;
    [SerializeField] private float jumpForce = 10;

    private Animator playerAnim;

    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtSplatter;

    private AudioSource playerAudio;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;

    public bool isOnGround = true;
    private int doubleJump = 2;

    public bool gameOver = false;
    public bool startRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();  
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity*=gravityModifire;
        transform.position = new Vector3(-6, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        EntryWalk();
        PlayerJump();
        PlayerDrop();
    }
    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && isOnGround|| Input.GetKeyDown(KeyCode.Space) && !gameOver  && doubleJump>0)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);//???????? 
            isOnGround = false;
            dirtSplatter.Stop();
            playerAudio.PlayOneShot(jumpSound);
            doubleJump--;
            if(doubleJump==1)
                playerAnim.SetBool("Jump_b",true);
        }
    }
    void PlayerDrop()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && !gameOver) 
        { 
            playerRb.AddForce(Vector3.down * jumpForce*5, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")&&!gameOver)
        {
            isOnGround = true;
            doubleJump = 2;
            dirtSplatter.Play();
            playerAnim.SetBool("Jump_b", false);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAudio.PlayOneShot(crashSound);
            dirtSplatter.Stop();
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
        }

    }
    void EntryWalk()
    {
        if (transform.position.x < 1.3f)
        {
            transform.Translate(Vector3.forward * 3 * Time.deltaTime);
            playerAnim.SetFloat("Speed_f", .3f);
        }
        else
        { 
            playerAnim.SetFloat("Speed_f", .7f);
            startRunning = true;    
        }
    }
}
