using System;
using UnityEngine;

namespace Internal.Scripts.Lesson2
{
  public class Fibonacci : MonoBehaviour
  {
    #region Lesson2

    private static int PrintFibonacciCycle(int n)
    {
      int a = 0, b = 1;
      for (var i = 0; i < n; i++)
      {
        var c = a + b;
        a = b;
        b = c;
      }
      return a;
    }
    private static void PrintFibonacciRecursive(int a, int b, int n)
    {
      if (n == 0) return;
      Debug.Log(a.ToString());
      PrintFibonacciRecursive(b, a + b, n - 1);
    }

    #endregion

    private void Start()
    {
      PrintFibonacciCycle(5);
      PrintFibonacciRecursive(0, 1, 5);
    }
  }
}
