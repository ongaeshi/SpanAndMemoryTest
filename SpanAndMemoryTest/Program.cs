// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

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