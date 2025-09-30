namespace DefaultNamespace
{
    public interface IEnnemy
    {
        void Walknig(float speed);
        void Stop(int  timeToStop);
        void Attacking();
        void ReturnFromStart();
    }
}