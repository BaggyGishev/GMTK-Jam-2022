using System.Collections.Generic;
using UnityEngine;

namespace Gisha.GMTK2022.Core
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnpoints;

        private GameData GameData => ResourceGetter.GameData;

        public void Generate(int type, int count)
        {
            List<int> readyPoints = new List<int> {0, 1, 2, 3, 4, 5};
            for (int i = 0; i < count; i++)
            {
                int point = readyPoints[Random.Range(0, readyPoints.Count)];
                readyPoints.Remove(point);

                Instantiate(GameData.EnemyPrefabs[type], spawnpoints[point].position, Quaternion.identity);
            }
        }
    }
}