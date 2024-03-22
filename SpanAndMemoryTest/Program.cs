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

    //// Spanを介してデータを書き込む
    //for (int i = 0; i < span.Length; i++)
    //{
    //    span[i] = 1;
    //}

    // 元の配列が書き換えられている
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

// main
Console.WriteLine($"{nameof(SpanTest)}");
SpanTest();
Console.WriteLine($"{nameof(ReadOnlySpanTest)}");
ReadOnlySpanTest();
Console.WriteLine($"{nameof(StringSpanTest)}");
StringSpanTest();
Console.WriteLine($"{nameof(ReturnSpanTest)}");
ReturnSpanTest();


