using System.IO;
using System.Windows.Controls;
using System.Collections.Generic;

namespace CargoDelivery.BL
{
	public class Validator
	{
		private readonly List<TextBox> _inputs;
		
		public Validator(List<TextBox> inputs)
		{
			_inputs = inputs;
		}

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
