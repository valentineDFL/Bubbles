using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Bubble
{
    internal class SpawnPositionCalculator : MonoBehaviour
    {
        private const int PositionsCount = 360;

        [SerializeField][Range(1, 15)] private float _radius;
        [SerializeField] private Transform _targetPos;

        [SerializeField][Range(1, 60)] private int _spheresCount;
        [SerializeField] private Transform _prefabForSpawnTransform;

        [SerializeField] private int _spawnYPos;

        private Vector3[] _positionsForSpawn;

        public Vector3[] PositionsForSpawn => _positionsForSpawn;
        public int SpheresCount => _spheresCount;

        private void OnDisable()
        {
            _prefabForSpawnTransform.transform.localScale = Vector3.one;
        }

        private void Awake()
        {
            _positionsForSpawn = CalculateSpawnPosition();
        }

        private Vector3[] CalculateSpawnPosition()
        {
            Vector3[] positionsForSpawn = new Vector3[_spheresCount];

            float angleBetweenSpheres = PositionsCount / _spheresCount * Mathf.Deg2Rad;

            for(int i = 0; i < _spheresCount; i++)
            {
                float angle = angleBetweenSpheres * i;

                Vector2 spheresPos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _radius;
                positionsForSpawn[i] = new Vector3(_targetPos.position.x + spheresPos.x, _spawnYPos, _targetPos.position.z + spheresPos.y);
            }

            return positionsForSpawn;
        }

        public void ChangeSize()
        {
            float sphereHorizontalPerimeter = 2 * Mathf.PI * _radius;
            float summPrefabsSizes = _prefabForSpawnTransform.transform.localScale.x * _spheresCount;
            float newSize = sphereHorizontalPerimeter / summPrefabsSizes;

            _prefabForSpawnTransform.transform.localScale = new Vector3(newSize, newSize, newSize);
        }
    }
}
