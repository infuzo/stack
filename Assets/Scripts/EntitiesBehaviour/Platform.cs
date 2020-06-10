using UnityEngine;
using Zenject;

namespace Stack.EntitiesBehaviour
{
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        public Transform LeftTopPoint;
        [SerializeField]
        public Transform LeftBottomPoint;
        [SerializeField]
        public Transform RightTopPoint;
        [SerializeField]
        public Transform RightBottomPoint;

        [Space]
        [SerializeField]
        protected BoxCollider boxCollider;

        protected bool movementStopped;

        protected Vector3 direction;
        protected Vector3 startPosition;
        protected float speed;
        protected float maxDistance;

        private void Update()
        {
            if(movementStopped) { return; }

            transform.position += direction * speed * Time.deltaTime;
            if(Vector3.Distance(transform.position, startPosition) >= maxDistance)
            {
                transform.position = startPosition;
            }
        }

        public virtual void StartMovement(Vector3 direction,
            Vector3 startPosition,
            float speed,
            float maxDistance)
        {
            this.direction = direction;
            this.startPosition = startPosition;
            this.speed = speed;
            this.maxDistance = maxDistance;

            boxCollider.enabled = false;
            movementStopped = false;
        }

        public virtual void StopMovement()
        {
            movementStopped = true;
        }
    }
}

