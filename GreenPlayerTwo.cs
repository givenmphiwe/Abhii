using System.Collections;
using UnityEngine;

public class GreenPlayerTwo : MonoBehaviour
{
    public static string greenPlayerTwoColName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            greenPlayerTwoColName = col.gameObject.name;

            if (col.gameObject.name.Contains("green house (1)"))
            {
                SoundManager.safeHouseAudioSource.Play();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        greenPlayerTwoColName = "none";
    }
}
