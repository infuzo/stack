using System;
using UnityEngine;

namespace Stack.Models
{
    [Serializable]
    public class StartPointsModel
    {
        [SerializeField]
        public Transform RightTopPoint;
        [SerializeField]
        public Transform RightBottomPoint;
        [SerializeField]
        public Transform LeftTopPoint;
        [SerializeField]
        public Transform LeftBottomPoint;

        public Transform GetRandomPoint()
        {
            var number = UnityEngine.Random.Range(0, 4);
            switch(number)
            {
                case 0: return RightTopPoint;
                case 1: return RightBottomPoint;
                case 2: return LeftTopPoint;
                case 3: return LeftBottomPoint;
            }
            Debug.LogError($"Incorrect random value (\"{number}\") to select start point.");
            return RightTopPoint;
        }
    }
}


