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

// main
Console.WriteLine($"{nameof(SpanTest)}");
SpanTest();
Console.WriteLine($"{nameof(ReadOnlySpanTest)}");
ReadOnlySpanTest();


