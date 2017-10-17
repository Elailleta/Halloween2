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
    public GameObject wall;

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
}
