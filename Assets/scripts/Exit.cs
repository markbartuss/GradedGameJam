using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            Cursor.visible = true;
            Time.timeScale = 1;
        }
    }
}
