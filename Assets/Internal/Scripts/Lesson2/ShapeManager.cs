using System;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Scripts.Lesson2
{
  public class ShapeManager : MonoBehaviour
  {
    private void Start()
    {
      var shapes = new List<Shape>();
      shapes.Add(new Circle(5));
      shapes.Add(new Rectangle(5, 6));
      shapes.Add(new Triangle(5));
      
      foreach(var shape in shapes)
      {
        shape.Move(new Vector3(1, 2, 3));
        shape.Scale(2);
        shape.Print();
      }
    }
  }
}
