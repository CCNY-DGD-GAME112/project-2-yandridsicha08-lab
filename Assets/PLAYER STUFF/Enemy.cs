using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static float GhostSpeed = 5f;
    public static int Score = 0;

    public static AudioSource MusicSource;
    public static AudioSource DeathSoundSource;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pause the music and play death sound
            if (MusicSource != null) MusicSource.Pause();
            if (DeathSoundSource != null) DeathSoundSource.Play();

            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
