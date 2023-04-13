namespace KilencedikOra.Shapes;

internal class Triangle : ITriangle
{
    private readonly int m_Side1;
    private readonly int m_Side2;
    private readonly int m_Side3;

    public bool IsIsosceles => m_Side1 == m_Side2 || m_Side2 == m_Side3 || m_Side1 == m_Side3;

    public bool IsEquilateral => m_Side1 == m_Side2 && m_Side2 == m_Side3;

    public Triangle(int side1, int side2, int side3)
    {
        if (side1 < side2 || side1 < side3)
        {
            throw new ArgumentException("First side is not the longest.");
        }

        if (side1 >= side2 + side3)
        {
            throw new ArgumentException("Does not satisfy triangle inequality.");
        }

        m_Side1 = side1;
        m_Side2 = side2;
        m_Side3 = side3;
    }

    public int GetPerimeter()
    {
        return m_Side1 + m_Side2 + m_Side2;
    }

    public double GetArea()
    {
        // Here we use Heron's formula to compute the area.
        var halfPerimeter = GetPerimeter() / 2.0;
        var radicand = halfPerimeter
                       * (halfPerimeter - m_Side1)
                       * (halfPerimeter - m_Side2)
                       * (halfPerimeter - m_Side3);
        return Math.Sqrt(radicand);
    }

    public Kind GetKind()
    {
        if (IsIsosceles)
        {
            return Kind.Isosceles;
        }
        else if (IsEquilateral)
        {
            return Kind.Equilateral;
        }
        else
        {
            return Kind.Scalene;
        }
    }
}