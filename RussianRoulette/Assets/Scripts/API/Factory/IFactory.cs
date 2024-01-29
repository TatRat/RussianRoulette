namespace TatRat.API
{
    public interface IFactory<out TOutput>
    {
        TOutput Create();
    }

    public interface IFactory<out TOutput, in TInput1>
    {
        TOutput Create(TInput1 param1);
    }

    public interface IFactory<out TOutput, in TInput1, in TInput2>
    {
        TOutput Create(TInput1 param1, TInput2 param2);
    }

    public interface IFactory<out TOutput, in TInput1, in TInput2, in TInput3>
    {
        TOutput Create(TInput1 param1, TInput2 param2, TInput3 param3);
    }
}