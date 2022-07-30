using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerLook : MonoBehaviour
{
    public Text numberText;
    int number;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            float xScale = collision.gameObject.transform.localScale.x, yScale = collision.gameObject.transform.localScale.y, zScale = collision.gameObject.transform.localScale.z;
            number = int.Parse(numberText.text);
            if(gameObject.tag == "Thicker")
            {
                xScale += (float) number / 100;
                zScale += (float) number / 100;
            }
            else if(gameObject.tag == "Thinner")
            {
                xScale += (float)number / 100;
                zScale += (float)number / 100;      
            }
            else if (gameObject.tag == "Taller")
            {
                yScale += (float)number / 100;

            }
            else if (gameObject.tag == "Smaller")
            {
                yScale += (float)number / 100;
            }
            if (xScale > 0 && zScale > 0 && yScale > 0)
                collision.gameObject.transform.localScale = new Vector3(xScale, yScale, zScale);
            else
            {
                collision.gameObject.transform.localScale = new Vector3(0.2f, 0.246f, 0.2f);
                Debug.Log("Game Over ");
            }

            Destroy(gameObject);
        }
    }
}
