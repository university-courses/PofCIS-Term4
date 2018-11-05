using System.IO;
using System.Windows.Controls;
using System.Collections.Generic;

namespace CargoDelivery.BL
{
	/// <summary>
	/// Represents input fields validator.
	/// </summary>
	public class Validator
	{
		/// <summary>
		/// A list of inputs to validate.
		/// </summary>
		private readonly List<TextBox> _inputs;
		
		/// <summary>
		/// Constructs a new validator object with given input fields.
		/// </summary>
		/// <param name="inputs">A list of inputs.</param>
		public Validator(List<TextBox> inputs)
		{
			_inputs = inputs;
		}

		/// <summary>
		/// Validates input fields given in constructor.
		/// </summary>
		/// <exception cref="InvalidDataException">Throws if some field(s) contains errors.</exception>
		public void Validate()
		{
			foreach (var input in _inputs)
			{
				var parent = input.Parent as GroupBox;
				var result = Validation.GetErrors(input);
				if (result.Count > 0)
				{
					if (parent != null)
					{
						throw new InvalidDataException("Field '" + parent.Header + "' contains errors!");	
					}

					throw new InvalidDataException("Some fields contains errors!");
				}

				if (input.Text.Length != 0)
				{
					continue;
				}

				if (parent != null)
				{
					throw new InvalidDataException("Field '" + parent.Header + "' can't be empty!");	
				}

				throw new InvalidDataException("Check all fields if it is filled up!");
			}
		}
	}
}
