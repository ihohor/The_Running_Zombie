using UnityEngine;
using System.Collections.Generic;

public class DamageManager : MonoBehaviour
{
    [System.Serializable]
    public class BombData
    {
        public GameObject bombPrefab; // Prefab bomby
        public int damage; // Obra¿enia zadawane przez bombê
    }

    public List<BombData> bombs = new List<BombData>(); // Lista bomb zarz¹dzana przez DamageManager

    public void ApplyDamage(GameObject bomb, ZombieStateAndHealth target)
    {
        BombData bombData = bombs.Find(b => b.bombPrefab == bomb);
        if (bombData != null)
        {
            target.TakeDamage(bombData.damage);
        }
        else
        {
            Debug.LogWarning("Brak danych dla tej bomby w DamageManager.");
        }
    }
}