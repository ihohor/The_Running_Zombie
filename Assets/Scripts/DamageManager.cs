using UnityEngine;
using System.Collections.Generic;

public class DamageManager : MonoBehaviour
{
    [System.Serializable]
    public class BombData
    {
        public GameObject bombPrefab; // Prefab bomby
        public int damage; // Obra�enia zadawane przez bomb�
    }

    // Lista bomb zarz�dzana przez DamageManager
    public List<BombData> bombs = new List<BombData>();

    // Funkcja obliczaj�ca obra�enia zadane przez bomb�
    public void ApplyDamage(GameObject bomb, GameObject target)
    {
        BombData bombData = bombs.Find(b => b.bombPrefab == bomb);
        if (bombData != null)
        {
            // Znalezienie skryptu zdrowia na obiekcie docelowym (np. ZombieHealth)
            ZombieHealth targetHealth = target.GetComponent<ZombieHealth>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(bombData.damage);
            }
        }
    }
}
