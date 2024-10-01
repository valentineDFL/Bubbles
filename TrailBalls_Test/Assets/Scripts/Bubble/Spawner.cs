using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Bubble
{
    internal class Spawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _pool;
        [SerializeField] private SpawnPositionCalculator _spawnPositionCalculator;

        [SerializeField] private float _spawnDelay;
        private float _time;

        private int _index;

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time >= _spawnDelay)
            {
                if (_index > _spawnPositionCalculator.PositionsForSpawn.Length - 1)
                    _index = 0;

                _time = 0;

                _pool.ReleasePool(_spawnPositionCalculator.PositionsForSpawn[_index]);

                _index += 1;
            }
        }
    }
}
