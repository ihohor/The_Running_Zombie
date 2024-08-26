using UnityEngine;

public class MedKit : Item
{
    [SerializeField] private int _healAmount = 50;

    // D�wi�k pojawienia si� apteczki
    [SerializeField] private AudioClip medKitSound;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();

        // Dodanie AudioSource, je�li brak
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Odtwarzanie d�wi�ku w p�tli, dop�ki apteczka istnieje
        PlayMedKitSound();
    }

    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth);
        zombieHealth.Heal(_healAmount);

        // Zatrzymaj d�wi�k po zderzeniu
        StopMedKitSound();

        // Zniszcz apteczk� po u�yciu
        Destroy(gameObject);
    }

    // Funkcja odtwarzaj�ca d�wi�k apteczki
    private void PlayMedKitSound()
    {
        if (medKitSound != null && _audioSource != null)
        {
            _audioSource.clip = medKitSound;
            _audioSource.loop = true;  // D�wi�k b�dzie odtwarzany w p�tli
            _audioSource.Play();
        }
    }

    // Funkcja zatrzymuj�ca d�wi�k apteczki
    private void StopMedKitSound()
    {
        if (_audioSource != null && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
