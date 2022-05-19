using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerSound;

    private float jumpForce = 700;
    private float gravityModifier =  1.5f;
    public bool isOnGround;
    public bool gameOver;
    
    public ParticleSystem explosionPartical;
    public ParticleSystem dirtyParticle;

    public AudioClip jumpSound, crashSound;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gravityModifier;

        playerAnim = GetComponent<Animator>();

        playerSound = GetComponent<AudioSource>();

        gameOver = false;
        isOnGround = true;

        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            playerAnim.SetTrigger("Jump_trig");

            dirtyParticle.Stop();

            playerSound.PlayOneShot(jumpSound, 0.5f);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtyParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {

            gameOver = true;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            explosionPartical.Play();
            dirtyParticle.Stop();

            playerSound.PlayOneShot(crashSound, 0.5f);
        }
        
    }

    public void RestartGame()
    { 
            SceneManager.LoadScene(0);
    }

}
