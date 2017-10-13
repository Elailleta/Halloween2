using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private int count;
    public Text CountText;
    public Text WinText;
    public Text SpecialText;
    public GameObject wall;

    
    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
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
        if ("special" != null)
        {
            SpecialText.text = "Special: 1";
        }
    }
}
