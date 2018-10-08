using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using Point = DrawShape.Classes.Point;

namespace DrawShape.Utils
{
	/// <summary>
	/// Utility class.
	/// </summary>
	public static class Util
	{
		/// <summary>
		/// Function to get hexagon id by clicking on it
		/// </summary>
		/// <param name="clickPosition">Point where mouse was clicked.</param>
		/// <param name="elements">Collection of elements.</param>
		/// <returns>Id of hexagon, if the point that was clocked was in it.</returns>
		/// <exception cref="InvalidDataException">Throws if hexagon does not exist.</exception>
		public static int GetHexagonIdByClickPos(Point clickPosition, UIElementCollection elements)
		{
			var hexagons = elements.OfType<Polygon>();
			var enumerable = hexagons as Polygon[] ?? hexagons.ToArray();
			for (var i = enumerable.Length - 1; i >= 0; i--)
			{
				if (PointIsInHexagon(clickPosition, enumerable[i]))
				{
					return i;
				}
			}

			throw new InvalidDataException("hexagon does not exist");
		}

		/// <summary>
		/// Function to check if point is in hexagon.
		/// </summary>
		/// <param name="point">Point to check.</param>
		/// <param name="hexagon">Hexagon in which point might be.</param>
		/// <returns>True if point is located in given hexagon.</returns>
		public static bool PointIsInHexagon(Point point, Polygon hexagon)
		{
			// TODO: check if mouse is clicked on hexagon

			return false;
		}

		public static bool AreSidesIntersected(
			System.Windows.Point firstSidePointOne, System.Windows.Point firstSidePointTwo,
			System.Windows.Point secondSidePointOne, System.Windows.Point secondSidePointTwo
		)
		{
			var solution = SolveCramer(new[]
			{
				new[]
				{
					firstSidePointTwo.X - firstSidePointOne.X,
					-secondSidePointTwo.X + secondSidePointOne.X,
					secondSidePointOne.X - firstSidePointOne.X
				},
				new[]
				{
					firstSidePointTwo.Y - firstSidePointOne.Y,
					-secondSidePointTwo.Y + secondSidePointOne.Y,
					secondSidePointOne.Y - firstSidePointOne.Y
				}
			});
			return !solution.Any(var => var < 0 && var > 1);
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

			if (matrix[0].Length != order)
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
		/// Function to move hexagon using arrow keys.
		/// </summary>
		/// <param name="hexagon">Hexagon to be moved.</param>
		/// <param name="location">Where to move the hexagon.</param>
		public static Polygon MoveHexagonWithArrows(Polygon hexagon, System.Windows.Point location)
		{
			/*
			var points = hexagon.Points;
			hexagon.Points.Clear();
			foreach (var point in points)
			{
				hexagon.Points.Add(new Point(point.X - location.X, point.Y - location.Y));
			}
	*/		
			Canvas.SetTop(hexagon, Canvas.GetTop(hexagon)-location.Y);
			Canvas.SetLeft(hexagon, Canvas.GetLeft(hexagon)+location.X);
			

			return hexagon;
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
