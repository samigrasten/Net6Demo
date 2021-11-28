public class BreakpointDemo
{
    private static int _factor = 1;

    public static int Start() {

        var list = Enumerable.Range(1, 1000).Select(i => i).ToList();        
        var result = DoSomething(list);

        return result;
    }

    private static int DoSomething(List<int> list)
    {
        var sum = 0;
        foreach (var item in list)
        {
            if (item == 76) InvokeSomeMethod(item);

            sum += item / _factor;
        }

        return sum;
    }

    private static void InvokeSomeMethod(int item)
    {
        // do something with item
        _factor = 0;
    }
}