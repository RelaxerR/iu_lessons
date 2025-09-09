using UnityEngine;

namespace Internal.Scripts.Lesson2
{
  public abstract class Shape
  {
    protected Vector3 CenterPosition { get; set; }
    
    public abstract void Move(Vector3 position);
    public abstract void Scale(int multiplier);
    public abstract void Print();
  }
  
  public class Circle : Shape
  {
    private float Radius { get; set; }
    
    public Circle(float radius)
    {
      Radius = radius;
    }
    
    public override void Move(Vector3 position)
    {
      CenterPosition = position;
    }
    public override void Scale(int multiplier)
    {
      Radius *= multiplier;
    }
    public override void Print()
    {
      Debug.Log($"Circle: CenterPosition = {CenterPosition}, Radius = {Radius}");
    }
  }
  
  public class Triangle : Shape
  {
    private float SideLength { get; set; }
    
    public Triangle(float sideLength)
    {
      SideLength = sideLength;
    }
    
    public override void Move(Vector3 position)
    {
      CenterPosition = position;
    }
    public override void Scale(int multiplier)
    {
      SideLength *= multiplier;
    }
    public override void Print()
    {
      Debug.Log($"Triangle: CenterPosition = {CenterPosition}, SideLength = {SideLength}");
    }
  }
  
  public class Rectangle : Shape
  {
    private float Width { get; set; }
    private float Height { get; set; }
    
    public Rectangle(float width, float height)
    {
      Width = width;
      Height = height;
    }
    
    public override void Move(Vector3 position)
    {
      CenterPosition = position;
    }
    public override void Scale(int multiplier)
    {
      Width *= multiplier;
      Height *= multiplier;
    }
    public override void Print()
    {
      Debug.Log($"Rectangle: CenterPosition = {CenterPosition}, Width = {Width}, Height = {Height}");
    }
  }
}
