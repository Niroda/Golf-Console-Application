using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfDemo {
	public class Swing {
		/// <summary>
		/// Gravitational Acceleration Constant
		/// </summary>
		public const double GRAVITY = 9.8;
		
		/// <summary>
		/// The course that the swing is being taken on.
		/// </summary>
		public Course Course { get; set; }

		/// <summary>
		/// The angle of the golf ball leaving the ground, in degrees
		/// </summary>
		public double Angle { get; set; }

		/// <summary>
		/// The angle of the golf ball leaving the ground, in radians
		/// </summary>
		public double AngleInRadians {
			get {
				return (Math.PI / 180) * this.Angle;
			}
		}

		/// <summary>
		/// The velocity of the golf ball leaving the ground, in meters per second
		/// </summary>
		public double Velocity { get; set; }

		/// <summary>
		/// The distance travelled by the golf ball as a result of this swing
		/// </summary>
		public double Distance {
			get
			{
				return Math.Pow(this.Velocity, 2) / GRAVITY * 
					Math.Sin(2 * this.AngleInRadians);
			}
		}

		/// <summary>
		/// Constructor for the Swing class.
		/// 
		///		Exceptions:
		///			AngleInvalidException
		/// </summary>
		/// <param name="angle">angle of the launch</param>
		/// <param name="velocity">speed of the ball</param>
		/// <param name="course">which course is used</param>
		public Swing(double angle, double velocity, Course course) {
			// Ensure that the angle is in the right span.
			if(angle >= 90 || angle <= 0) {
				throw new AngleInvalidExeption();
			}
			// Ensure that the velocity is positive.
			if(velocity <= 0) {
				this.Velocity = 0;
			} else {
				this.Velocity = velocity;
			}

			// Assign the values to the member variables
			this.Angle = angle;
			this.Course = course;

		}
	}
}
