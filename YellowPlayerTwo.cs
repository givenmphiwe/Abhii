using System.Collections;
using UnityEngine;

public class YellowPlayerTwo : MonoBehaviour
{
    public static string yellowPlayerTwoColName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            yellowPlayerTwoColName = col.gameObject.name;

            if (col.gameObject.name.Contains("yellow house (1)"))
            {
                SoundManager.safeHouseAudioSource.Play();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        yellowPlayerTwoColName = "none";
    }
}
