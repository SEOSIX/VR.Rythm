using UnityEngine;

namespace DefaultNamespace
{
    public interface IEnnemy
    {
        void Walking(float speed);
        void Stop(float  timeToStop);
        void Attacking();
        void ReturnFromStart(GameObject warpTarget);
    }
}