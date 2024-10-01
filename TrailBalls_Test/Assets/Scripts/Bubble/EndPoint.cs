using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Bubble
{
    internal class EndPoint : MonoBehaviour
    {
        [SerializeField] private ObjectPool _pool;

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _pool.ReturnToPool(other.gameObject);
        }
    }
}
