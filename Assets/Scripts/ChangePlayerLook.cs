using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerLook : MonoBehaviour
{
    public Text numberText;
    int number;
    int willDie = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            float xScale = collision.gameObject.transform.localScale.x, yScale = collision.gameObject.transform.localScale.y, zScale = collision.gameObject.transform.localScale.z;
            number = int.Parse(numberText.text);
            if(gameObject.tag == "Thicker")
            {
                xScale += (float) number / 1000;
                zScale += (float) number / 1000;
                willDie++;
            }
            else if(gameObject.tag == "Thinner")
            {
                xScale += (float)number / 1000;
                zScale += (float)number / 1000;
                willDie--;
            }
            else if (gameObject.tag == "Taller")
            {
                yScale += (float)number / 1000;
                willDie++;

            }
            else if (gameObject.tag == "Smaller")
            {
                yScale += (float)number / 1000;
                willDie--;
            }
            if (willDie >= 0)
                collision.gameObject.transform.localScale = new Vector3(xScale, yScale, zScale);
            else
            {
                collision.gameObject.transform.localScale = new Vector3(0.5f, 0.246f, 0.5f);
                Debug.Log("Game Over ");
            }

            Destroy(gameObject);
        }
    }
}
