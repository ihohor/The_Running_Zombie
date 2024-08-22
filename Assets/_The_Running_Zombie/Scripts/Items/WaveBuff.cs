using System.Collections;
using UnityEngine;

public class WaveBuff : Bufs
{
    [SerializeField] private GameObject shockwavePrefab; // Prefab fali uderzeniowej
    [SerializeField] private float waveExpansionSpeed = 10f; // Prêdkoœæ rozszerzania siê fali
    [SerializeField] private float waveDuration = 5f; // Czas trwania fali

    // Nadpisujemy metodê Collide z klasy Bufs
    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth); // Wywo³anie bazowej logiki kolizji

        if (zombieHealth != null)
        {
            Debug.Log("Buff fali uderzeniowej zosta³ zebrany.");

            // Generujemy falê uderzeniow¹ wokó³ zombie
            StartCoroutine(GenerateShockwave(zombieHealth));
        }

        Destroy(gameObject); // Usuniêcie buffa po jego zebraniu
    }

    // Korutyna odpowiedzialna za utworzenie i rozszerzanie fali uderzeniowej
    private IEnumerator GenerateShockwave(ZombieStateAndHealth zombieHealth)
    {
        // Utworzenie prefabrykat fali uderzeniowej wokó³ zombie
        GameObject shockwave = Instantiate(shockwavePrefab, zombieHealth.transform.position, Quaternion.identity);

        float elapsedTime = 0f;

        // Dopóki czas trwania fali nie minie, rozszerzamy falê
        while (elapsedTime < waveDuration)
        {
            shockwave.transform.localScale += Vector3.one * waveExpansionSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Po zakoñczeniu czasu trwania, zniszcz falê
        Destroy(shockwave);
    }

    // Obs³uga kolizji z ziemi¹ (buff zniknie po dotkniêciu ziemi)
    protected override void OnGroundCollision()
    {
        base.OnGroundCollision(); // Wywo³anie logiki kolizji z ziemi¹
        Destroy(gameObject); // Usuniêcie obiektu po dotkniêciu ziemi
    }
}
