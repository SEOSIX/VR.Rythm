namespace DefaultNamespace
{
    public interface IEnnemy
    {
        void Walknig(float speed);
        void Stop(float  timeToStop);
        void Attacking();
        void ReturnFromStart();
    }
}