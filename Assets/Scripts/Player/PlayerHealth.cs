using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
	public int currentHealth;
    public int instantHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(255, 255, 255, 10);
	//private Color test=new Color(2,5,6);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        instantHealth = startingHealth;
		currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
			flashColour.a=((startingHealth-instantHealth)*0.008f);
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed *(instantHealth+10)*0.01f* Time.deltaTime);
        }
        damaged = false;

		currentHealth = (int)Mathf.Lerp (currentHealth, instantHealth, 0.2f);
		healthSlider.value = currentHealth;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        instantHealth -= amount;

        

        playerAudio.Play ();

        if(instantHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
}
