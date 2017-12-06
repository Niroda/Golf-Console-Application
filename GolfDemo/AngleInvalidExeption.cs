using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfDemo {
	public class AngleInvalidExeption : Exception {
		public AngleInvalidExeption()
			: base("The angle is invalid - try again") {
		}
	}
}
