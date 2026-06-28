using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class SensorSystemmm : MonoBehaviour
    {
        [field: SerializeField] public float SensorRadius { get; private set; }
        [field: SerializeField] public float SensorAngle { get; private set; }

        [SerializeField] private LayerMask whatIsTarget;
        [SerializeField] private LayerMask whatIsObstacle;

        private void FixedUpdate() //suele ser mas lento y computacionalmente menos pesado.
        {
            SearchTarget();
        }


        public GameObject SearchTarget()
        {
            Collider[] results = Physics.OverlapSphere(transform.position, SensorRadius, whatIsTarget);

            if (results.Length > 0)
            {
                Vector3 directionToTarget = results[0].transform.position - transform.position;

                if (Vector3.Angle(transform.forward, directionToTarget) <= SensorAngle / 2)
                {
                    if (!Physics.Raycast(transform.position + Vector3.up * 0.3f, directionToTarget,
                            directionToTarget.magnitude, whatIsObstacle))
                    {
                        //HAS DETECTADO A ALGUIEN
                        return results[0].gameObject;
                    }
                }
            }
            return null; //si no ha detectado nada retorna un null
        }

        public Vector3 DirFromAngle(float angle, bool relativeToFront)
        {
            if (relativeToFront)
            {
                angle += transform.eulerAngles.y;
            }
            return new Vector3
                (Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle) * Mathf.Deg2Rad); //en unity es al reves
        }


    }
}