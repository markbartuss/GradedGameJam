using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // Gets triggered when player enters the water/barrier
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // restarts the scene
        }
    }
}
