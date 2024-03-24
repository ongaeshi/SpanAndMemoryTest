// https://annulusgames.com/blog/span-and-memory/

static void SpanTest()
{
    var array = new int[8];

    // 配列の一部をSpan<int>として取得
    var span = array.AsSpan(2, 4);

    // Spanを介してデータを書き込む
    for (int i = 0; i < span.Length; i++)
    {
        span[i] = 1;
    }

    // 元の配列が書き換えられている
    foreach (var x in array)
    {
        // 0, 0, 1, 1, 1, 1, 0, 0
        Console.WriteLine(x);
    }
}

static void ReadOnlySpanTest()
{
    var array = new int[8];

    // 配列の一部をSpan<int>として取得
    ReadOnlySpan<int> span = array.AsSpan(2, 4);

    // ReadOnlySpan には書き込みは用意されていない
    //// Spanを介してデータを書き込む
    //for (int i = 0; i < span.Length; i++)
    //{
    //    span[i] = 1;
    //}

    // unsafe を使えば ReadOnlySpan も書き換えることができる(やってはいけない)
    //unsafe
    //{
    //    fixed (int* ptr = span)
    //    {
    //        *ptr = 3;
    //    }
    //}

    // 元の配列の最終結果を出力
    foreach (var x in array)
    {
        // 0, 0, 1, 1, 1, 1, 0, 0
        Console.WriteLine(x);
    }
}

static void StringSpanTest()
{
    // 文字列の一部'ABCDE'をReadOnlySpan<char>として取得
    // コピーを作成しないためアロケーションもなく高速
    {
        ReadOnlySpan<char> span = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".AsSpan(3, 5);
        Console.WriteLine(span.ToString());
    }

    {
        // 文字列の場合はデフォルトで ReadOnlySpan が返ってくる模様。
        var span = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".AsSpan(3, 5);
        // span[1] = 'Q';
    }

}

static Span<int> GetSpan(int[] array)
{
    return array.AsSpan(2, 4);
}

static void SetSpan(Span<int> span, int value)
{
    for (int i = 0; i < span.Length; i++)
    {
        span[i] = value;
    }
}

static void ReturnSpanTest()
{
    var array = new int[8];

    var span = GetSpan(array);
    SetSpan(span, 2);

    // 元の配列が書き換えられている
    foreach (var x in array)
    {
        // 0, 0, 1, 1, 1, 1, 0, 0
        Console.WriteLine(x);
    }
}

static void StackAllocTest()
{
    // ポインタではなくSpan<T>で受け取る
    // → こうすると unsafe 外でも stackalloc が使える。
    Span<int> buffer = stackalloc int[10];

    try
    {
        foreach (var x in buffer)
        {
            Console.WriteLine(x);
        }

        // 境界外にアクセスした場合はちゃんと例外がスローされる
        //buffer[-1] = 1;
    }
    catch (IndexOutOfRangeException ex)
    {
        Console.WriteLine(ex);
    }
}

static Span<int> ReturnSpan()
{
    //Span<int> stackSpan = stackalloc int[10];
    //return stackSpan;

    Span<int> arraySpan = new int[10].AsSpan();
    return arraySpan;
}

static void ReturnNewSpanTest()
{
    var span = ReturnSpan();
    Console.WriteLine(span.Length);
}

static void RefStructTest()
{
    var s = new FooStruct { span = stackalloc int[3] };
    s.span[0] = 1;
    s.span[1] = 2;
    s.span[2] = 3;

    foreach (var x in s.span)
    {
        Console.WriteLine(x);
    }

    RefStructArgTest(s);
}

static FooStruct RefStructArgTest(FooStruct s)
{
    foreach (var x in s.span)
    {
        Console.WriteLine(x);
    }
    // 引数で受け取った Span を戻り値で返すのは問題ない(スコープは同じなので)
    return s;
}

static void TaskTest()
{
    //var s = new FooStruct { span = stackalloc int[3] };
    Span<int> span = stackalloc int[3];
    var s = new FooStruct { span = span };

    Task.Run(() =>
    {
        //foreach (var x in s.span)
        //{
        //    Console.WriteLine(x);
        //}
    });
}

// main
Console.WriteLine($"{nameof(SpanTest)}");
SpanTest();
Console.WriteLine($"{nameof(ReadOnlySpanTest)}");
ReadOnlySpanTest();
Console.WriteLine($"{nameof(StringSpanTest)}");
StringSpanTest();
Console.WriteLine($"{nameof(ReturnSpanTest)}");
ReturnSpanTest();
Console.WriteLine($"{nameof(StackAllocTest)}");
StackAllocTest();
Console.WriteLine($"{nameof(ReturnNewSpanTest)}");
ReturnNewSpanTest();
Console.WriteLine($"{nameof(RefStructTest)}");
RefStructTest();

// ref構造体はそれ自体がスタックに置かれることを保証されるため、内部にもref構造体を持てる
public ref struct FooStruct
{
    public Span<int> span;
}


