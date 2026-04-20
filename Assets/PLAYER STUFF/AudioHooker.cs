using UnityEngine;

public class AudioHooker : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioSource DeathSoundSource;

    void Awake()
    {
        Enemy.MusicSource = MusicSource;
        Enemy.DeathSoundSource = DeathSoundSource;
    }
}
