using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Bubble
{
    internal class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _sphereForSpawn;
        private int _poolStartUpCapacity;

        private List<GameObject> _pool;

        private SpawnPositionCalculator _spawnPositionCalculator;
        private BubbleColor _bubbleColor;

        private int _index;

        [SerializeField] private float timescale;

        private void Awake()
        {
            _spawnPositionCalculator = GetComponent<SpawnPositionCalculator>();
            _bubbleColor = GetComponent<BubbleColor>();

            _poolStartUpCapacity = _spawnPositionCalculator.SpheresCount;

            _pool = new List<GameObject>(_poolStartUpCapacity);

            _spawnPositionCalculator.ChangeSize();
            for(int i = 0; i < _poolStartUpCapacity; i++)
            {
                GameObject sphere = Instantiate(_sphereForSpawn, this.transform.position, Quaternion.identity);
                _bubbleColor.InitColor(sphere);
               
                _pool.Add(sphere);

                _index = i + 1;
                _pool[i].name = _pool[i].name + $" {_index}";

                _bubbleColor.BubblesTrailRenderer[sphere].enabled = false;
                _pool[i].SetActive(false);
            }
        }

        public void ReleasePool(Vector3 spawnPos)
        {
            if(_pool.Count == 0)
                RisePool();

            _pool[0].transform.position = spawnPos;
            _bubbleColor.BubblesTrailRenderer[_pool[0]].enabled = true;
            _bubbleColor.BubblesTrailRenderer[_pool[0]].Clear();

            _pool[0].SetActive(true);
            _bubbleColor.ChangeColor(_pool[0]);

            _pool.RemoveAt(0);
        }

        public void ReturnToPool(GameObject bubble)
        {
            _pool.Add(bubble);

            bubble.SetActive(false);
            _bubbleColor.BubblesTrailRenderer[bubble].enabled = false;
            bubble.transform.position = this.transform.position;
        }

        private void RisePool()
        {
            GameObject sphere = Instantiate(_sphereForSpawn, this.transform.position, Quaternion.identity);
            sphere.name = sphere.name + $" {_index + 1}";

            sphere.SetActive(false);
            _bubbleColor.InitColor(sphere);
            _bubbleColor.BubblesTrailRenderer[sphere].enabled = false;

            _index += 1;

            _pool.Add(sphere);
        }
    }
}
