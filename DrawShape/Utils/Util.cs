using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.Windows.Controls;
using DrawShape.Classes;
using MessageBox = System.Windows.MessageBox;
using Point = System.Windows.Point;

namespace DrawShape.Utils
{
	public static class Util
	{
		public static int GetHexagonIdByClickPos(Classes.Point clickPosition, UIElementCollection elements)
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

		public static bool PointIsInHexagon(Classes.Point point, Polygon hexagon)
		{
			// TODO: check if mouse is clicked on hexagon
			
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

		public static void MessageBoxFatal(string msg)
		{
			MessageBox.Show(msg, "Fatal", MessageBoxButton.OK, MessageBoxImage.Error);
		}

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

		public static double[] SolveCarmer(double[][] extendedMatrix)
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
		
		public static void MoveHexagonWithArrows(ref Hexagon hexagon, Point location)
		{
			foreach (var point in hexagon.Points)
			{
				point.X = location.X;
				point.Y = location.Y;
			}
		}
		
		public static Line GetLine(Classes.Point start, Classes.Point end, Brush brush)
		{
			var line = new Line { X1 = start.X, Y1 = start.Y, X2 = end.X, Y2 = end.Y, StrokeThickness = 1, Stroke = brush, SnapsToDevicePixels = true};
			line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
			return line;
		}
	}
}
