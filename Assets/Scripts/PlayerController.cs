using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private int count;
    private int health;
    private int special;
    public Text CountText;
    public Text WinText;
    public Text SpecialText;
    public Text HealthText;
    public Text LoseText;
    public Text DontText;
    public GameObject wall;
    public bool isArmed;
    public bool armour;
    public bool isGrounded;

    void OnCollisionStay(Collision coll)
    {
        isGrounded = true;
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(0, 10, 0);
        }
    }
    void OnCollisionExit(Collision coll)
    {
        if (isGrounded)
        {
            isGrounded = false;
        }
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        special = 0;
        health = 100;
        SetCountText();
        SetHealthText();
        SetSpecialText();
        isArmed = false;
    }

    
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("specialpickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 100;
            SetCountText();
        }
        if (other.gameObject.CompareTag("special"))
        {
            other.gameObject.SetActive(false);
            special = special + 1;
            SetSpecialText();
            wall.SetActive(false);
            
        }
        if (other.gameObject.CompareTag("enemy"))
        {
            if (armour == false)
            {
                health = health - 3;
                SetHealthText();
            }
        }
        if (other.gameObject.CompareTag("lava"))
        {
            health = health - health;
            SetHealthText();
            SetLoseText();
        }
        if (other.gameObject.CompareTag("weapon"))
        { 
            this.isArmed = true;
            other.gameObject.SetActive(false);
        }
        if (isArmed == true)
        {
           if (other.gameObject.CompareTag("enemy"))
            {
                other.gameObject.SetActive(false);
                health = health + 3;
            }
        }
        if (other.gameObject.CompareTag("health"))
        {
            other.gameObject.SetActive(false);
            health = 100;
            SetHealthText();
        }
        if (other.gameObject.CompareTag("base"))
        {
            health = health - health;
            SetHealthText();
            speed = speed - 10;
            SetLoseText();
            SetDontText();
        }
        if (other.gameObject.CompareTag("shield"))
        {
            this.armour = true;
            other.gameObject.SetActive(false);
        }
        if (health <= 0)
        {
            speed = speed - speed;
            SetLoseText();
        }
    }

    void SetCountText(){
    CountText.text = "Count : " + count.ToString();
        if (count >= 46)
        {
            WinText.text = "You Win!";
            speed = speed - 10; 
        }
    }
    void SetSpecialText()
    {
        SpecialText.text = "Special: ";
        if (special == 1)
        {
            SpecialText.text = "Special: 1";
        }
    }
    
    void SetHealthText()
    {
        HealthText.text = "Health : " + health.ToString() + " / 100";
    }

    void SetLoseText()
    {
        if (health <= 0)
        {
            LoseText.text = "You have lost.";
            count = count-count ;
        }
    }
    void SetDontText()
    {
        DontText.text = "Dont jump out.";
    }
}
