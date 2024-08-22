using System.Collections;
using UnityEngine;

public class WaveBuff : Bufs
{
    [SerializeField] private GameObject shockwavePrefab; // Prefab fali uderzeniowej
    [SerializeField] private float waveExpansionSpeed = 10f; // Pr�dko�� rozszerzania si� fali
    [SerializeField] private float waveDuration = 5f; // Czas trwania fali

    // Nadpisujemy metod� Collide z klasy Bufs
    protected override void Collide(ZombieStateAndHealth zombieHealth)
    {
        base.Collide(zombieHealth); // Wywo�anie bazowej logiki kolizji

        if (zombieHealth != null)
        {
            Debug.Log("Buff fali uderzeniowej zosta� zebrany.");

            // Generujemy fal� uderzeniow� wok� zombie
            StartCoroutine(GenerateShockwave(zombieHealth));
        }

        Destroy(gameObject); // Usuni�cie buffa po jego zebraniu
    }

    // Korutyna odpowiedzialna za utworzenie i rozszerzanie fali uderzeniowej
    private IEnumerator GenerateShockwave(ZombieStateAndHealth zombieHealth)
    {
        // Utworzenie prefabrykat fali uderzeniowej wok� zombie
        GameObject shockwave = Instantiate(shockwavePrefab, zombieHealth.transform.position, Quaternion.identity);

        float elapsedTime = 0f;

        // Dop�ki czas trwania fali nie minie, rozszerzamy fal�
        while (elapsedTime < waveDuration)
        {
            shockwave.transform.localScale += Vector3.one * waveExpansionSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Po zako�czeniu czasu trwania, zniszcz fal�
        Destroy(shockwave);
    }

    // Obs�uga kolizji z ziemi� (buff zniknie po dotkni�ciu ziemi)
    protected override void OnGroundCollision()
    {
        base.OnGroundCollision(); // Wywo�anie logiki kolizji z ziemi�
        Destroy(gameObject); // Usuni�cie obiektu po dotkni�ciu ziemi
    }
}
