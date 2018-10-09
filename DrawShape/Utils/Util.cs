using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

using Point = DrawShape.Classes.Point;
using MessageBox = System.Windows.MessageBox;

namespace DrawShape.Utils
{
	/// <summary>
	/// Utility class.
	/// </summary>
	public static class Util
	{
		/// <summary>
		/// Function to check if point is in hexagon.
		/// </summary>
		/// <param name="point">Point to check.</param>
		/// <param name="hexagon">Hexagon in which point might be.</param>
		/// <returns>True if point is located in given hexagon.</returns>
		public static bool PointIsInHexagon(Point point, Polygon hexagon)
		{
            // Create a point for line segment from p to infinite 
			var extreme = new System.Windows.Point(10000, point.Y);

			// Count intersections of the above line with sides of polygon 
			int count = 0, i = 0;
			do
			{
				var next = (i + 1) % 6;

				// Check if the line segment from 'p' to 'extreme' intersects 
				// with the line segment from 'polygon[i]' to 'polygon[next]' 
				if (AreSidesIntersected(hexagon.Points[i], hexagon.Points[next], new System.Windows.Point(point.X, point.Y),  extreme))
				{
					// If the point 'p' is colinear with line segment 'i-next', 
					// then check if it lies on segment. If it lies, return true, 
					// otherwise false 
					if (Orientation(hexagon.Points[i], new System.Windows.Point(point.X, point.Y), hexagon.Points[next]) == 0)
						return OnSegment(hexagon.Points[i], new System.Windows.Point(point.X, point.Y), hexagon.Points[next]);

					count++;
				}
				i = next;
			} while (i != 0);

			// Return true if count is odd, false otherwise 
			return count % 2 == 1;
		}

		public static bool OnSegment(System.Windows.Point p, System.Windows.Point q, System.Windows.Point r)
		{
			return q.X <= Math.Max(p.X, r.X)
			       && q.X >= Math.Min(p.X, r.X)
			       && q.Y <= Math.Max(p.Y, r.Y)
			       && q.Y >= Math.Min(p.Y, r.Y);
		}

        // To find orientation of ordered triplet (p, q, r). 
        // The function returns following values 
        // 0 --> p, q and r are colinear 
        // 1 --> Clockwise 
        // 2 --> Counterclockwise 
		public static int Orientation(System.Windows.Point p, System.Windows.Point q, System.Windows.Point r)
		{
			var val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);
			if (val.Equals(0))
			{
				return 0; // colinear 
			}

			return (val > 0) ? 1 : 2; // clock or counterclock wise 
		}

		public static bool AreSidesIntersected(
			System.Windows.Point firstSidePointOne, System.Windows.Point firstSidePointTwo,
			System.Windows.Point secondSidePointOne, System.Windows.Point secondSidePointTwo
		)
		{
			var o1 = Orientation(firstSidePointOne, firstSidePointTwo, secondSidePointOne);
			var o2 = Orientation(firstSidePointOne, firstSidePointTwo, secondSidePointTwo);
			var o3 = Orientation(secondSidePointOne, secondSidePointTwo, firstSidePointOne);
			var o4 = Orientation(secondSidePointOne, secondSidePointTwo, firstSidePointTwo);

			// General case 
			if (o1 != o2 && o3 != o4)
				return true;

			// Special Cases 
			// p1, q1 and p2 are colinear and p2 lies on segment p1q1 
			if (o1 == 0 && OnSegment(firstSidePointOne, secondSidePointOne, firstSidePointTwo)) return true;

			// p1, q1 and p2 are colinear and q2 lies on segment p1q1 
			if (o2 == 0 && OnSegment(firstSidePointOne, secondSidePointTwo, firstSidePointTwo)) return true;

			// p2, q2 and p1 are colinear and p1 lies on segment p2q2 
			if (o3 == 0 && OnSegment(secondSidePointOne, firstSidePointOne, secondSidePointTwo)) return true;

			// p2, q2 and q1 are colinear and q1 lies on segment p2q2 
			if (o4 == 0 && OnSegment(secondSidePointOne, firstSidePointTwo, secondSidePointTwo)) return true;

			return false;
		}
		
		/// <summary>
		/// Returns an index of a hexagon with specified name.
		/// </summary>
		/// <param name="name">Hexagon's name.</param>
		/// <param name="elements">A list of available hexagons.</param>
		/// <returns>Index of hexagon.</returns>
		/// <exception cref="InvalidDataException">Throws if hexagon does not exist.</exception>
		public static int GetHexagonIdByName(string name, UIElementCollection elements)
		{
			var hexagons = elements.OfType<Polygon>();
			var enumerable = hexagons as Polygon[] ?? hexagons.ToArray();
			for (var i = 0; i < enumerable.Length; i++)
			{
				if (enumerable[i].Name == name)
				{
					return i;
				}
			}

			throw new InvalidDataException("hexagon does not exist");
		}
		
		/// <summary>
		/// Function to get colour with ColorDialog.
		/// </summary>
		/// <param name="brush">Variable to store picked colour.</param>
		/// <returns>True if colour was picked.</returns>
		public static bool GetColorFromColorDilog(out Brush brush)
		{
			var colorDialog = new ColorDialog {AllowFullOpen = true};
			var dialogResult = colorDialog.ShowDialog();
			brush = new SolidColorBrush();
			if (dialogResult != DialogResult.OK)
			{
				return false;
			}

			brush = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
			return true;
		}

		/// <summary>
		/// Function to display message box.
		/// </summary>
		/// <param name="msg">Message to be displayed.</param>
		public static void MessageBoxFatal(string msg)
		{
			MessageBox.Show(msg, "Fatal", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		/// <summary>
		/// Function to calculate determinant of matrix.
		/// </summary>
		/// <param name="matrix">Matrix.</param>
		/// <returns>Value of determinant.</returns>
		/// <exception cref="InvalidDataException">Throws if matrix is empty, or it's dimensions aren't equal.</exception>
		public static double Determinant(double[][] matrix)
		{
			var order = matrix.Length;
			if (order < 1)
			{
				throw new InvalidDataException("matrix is empty");
			}

			if (matrix[0].Length - 1 != order)
			{
				throw new InvalidDataException("matrix is not square");
			}

			const double eps = 1e-9;
			var det = 1.0;
			for (var i = 0; i < order; i++)
			{
				var k = i;
				for (var j = i + 1; j < order; j++)
				{
					if (Math.Abs(matrix[j][i]) > Math.Abs(matrix[k][i]))
					{
						k = j;
					}
				}

				if (Math.Abs(matrix[k][i]) < eps)
				{
					det = 0;
					break;
				}

				var temp = matrix[i];
				matrix[i] = matrix[k];
				matrix[k] = temp;
				if (i != k)
				{
					det = -det;
				}

				det *= matrix[i][i];
				for (var j = i + 1; j < order; j++)
				{
					matrix[i][j] /= matrix[i][i];
				}

				for (var j = 0; j < order; j++)
				{
					if (j != i && Math.Abs(matrix[j][i]) > eps)
					{
						for (var l = i + 1; l < order; l++)
						{
							matrix[j][l] -= matrix[i][l] * matrix[j][i];
						}
					}
				}
			}

			return det;
		}

		/// <summary>
		/// Finction to solve linear equations using Cramer's method.
		/// </summary>
		/// <param name="extendedMatrix">Extended matrix of the equations.</param>
		/// <returns>Array of results.</returns>
		public static double[] SolveCramer(double[][] extendedMatrix)
		{
			var size = extendedMatrix.Length;
			if (size < 1)
			{
				throw new InvalidDataException("matrix is empty");
			}

			if (size + 1 != extendedMatrix[0].Length)
			{
				throw new InvalidDataException("can't solve matrix");
			}
			
			var solution = new double[extendedMatrix.Length];
			var mainDet = Determinant(extendedMatrix);
			if (mainDet.Equals(0))
			{
				throw new InvalidDataException("matrix is singular");
			}

			for (var j = 0; j < size; j++)
			{
				var col = new double[size];
				for (var i = 0; i < size; i++)
				{
					col[i] = extendedMatrix[i][j];
					extendedMatrix[i][j] = extendedMatrix[i][size];
				}

				var currentDet = Determinant(extendedMatrix);
				for (var i = 0; i < size; i++)
				{
					extendedMatrix[i][j] = col[i];
				}

				solution[j] = currentDet / mainDet;
			}

			return solution;
		}
		
		/// <summary>
		/// Function to get line from given coordinates.
		/// </summary>
		/// <param name="start">Starting point.</param>
		/// <param name="end">Ending point.</param>
		/// <param name="brush">Colour of the line.</param>
		/// <returns></returns>
		public static Line GetLine(Point start, Point end, Brush brush)
		{
			var line = new Line { X1 = start.X, Y1 = start.Y, X2 = end.X, Y2 = end.Y, StrokeThickness = 1, Stroke = brush, SnapsToDevicePixels = true};
			line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
			return line;
		}
	}
}
