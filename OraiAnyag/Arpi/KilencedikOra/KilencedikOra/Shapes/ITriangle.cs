namespace KilencedikOra.Shapes;

/// <summary>
/// The interface of the triangle.
/// </summary>
public interface ITriangle
{
    /// <summary>
    /// True if this triangle is isosceles.
    /// </summary>
    bool IsIsosceles { get; }

    /// <summary>
    /// True if this triangle is equilateral.
    /// </summary>
    bool IsEquilateral { get; }

    /// <summary>
    /// Calculates the sum of all sides of the triangle.
    /// </summary>
    /// <returns>The sum of all sides of the triangle.</returns>
    int GetPerimeter();

    /// <summary>
    /// Calculates the area of the triangle.
    /// </summary>
    /// <returns>The area of the triangle.</returns>
    double GetArea();

    /// <summary>
    /// Determines the kind or classification of the triangle by its sides.
    /// </summary>
    /// <returns>The kind or classification of the triangle.</returns>
    Kind GetKind();
}