using System;
using System.Collections;

class SimpleClass
{
	public Int32 m_key;
	public Int32 m_value;
	public SimpleClass(Int32 key, Int32 val)
	{
		m_key = key;
		m_value = val;
	}
}

class SimpleComparer : IComparer
{
        public Int32 Compare (SimpleClass x, SimpleClass y)
        {
        	if (x == null)
			return y == null ? 0 : 1;

		if (y == null)
			return -1;

		if (x.m_key == y.m_key)
			return 0;

		return x.m_key > y.m_key ? 1 : -1;
        }

	public Int32 Compare(object x, object y)
	{
		return Compare(x as SimpleClass, y as SimpleClass);
	} 
}

class ArraySortTest
{
	public static void ShowArray(SimpleClass[] array)
	{
		for (int i = 0; i < array.Length; ++i)
		{
			if (array[i] != null)
				Console.WriteLine("array[{0}] -> m_key: {1} : m_value: {2}", i, array[i].m_key, array[i].m_value);
			else
				Console.WriteLine("array[{0}] is null!", i);
		}
	}
	public static void Main()
	{
		Random rnd = new Random();
		SimpleComparer simpleComparer = new SimpleComparer();
		SimpleClass[] array = new SimpleClass[10];
		for (int i = 0; i < array.Length / 2; ++i)
		{
			array[i] = new SimpleClass(0, rnd.Next(0, 100));
		}

		Console.WriteLine("Before full array sort:");
		ShowArray(array);
		
		SimpleClass[] array2 = new SimpleClass[10];
		array.CopyTo(array2, 0);
		Array.Sort(array2, simpleComparer);
		Console.WriteLine("After full array sort - remember, quick-sort is unstable:");
		ShowArray(array2);

		Array.Sort(array, 0, array.Length / 2, simpleComparer);
		Console.WriteLine("After short-range array sort - the implementation is based on insertion sort, so it should be stable:");
		ShowArray(array);
	}
}
