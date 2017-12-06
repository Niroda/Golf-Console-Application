using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfDemo {
	public class Course {
		#region Statics and Constants
		/// <summary>
		/// How much of a tolerance the cup should have.
		/// How close to the cup do you need to get in order to win?
		/// </summary>
		public const double Tolerance = 0.1;
		#endregion
		#region Fields and Properties
		/// <summary>
		/// Max number of swings allowed on this course
		/// </summary>
		public int MaxSwings { get; set; }

		/// <summary>
		/// The Course's rough. Combines with the course length 
		/// to determine how far from the cup you should be allowed 
		/// to go without failing.
		/// </summary>
		public double CourseRough { get; set; }

		/// <summary>
		/// Backing Field - How many swings have been taken on this course
		/// </summary>
		private int swingCounter;

		/// <summary>
		/// Property - How many swings have been taken on this course
		/// </summary>
		public int SwingCounter
		{
			get { return swingCounter; }
			set
			{
				if(value >= this.MaxSwings) {
					HasEnded = true;
					HasLost = true;
				}
				swingCounter = value;
			}
		}
		
		/// <summary>
		/// A list of all the swings that have been taken on this course.
		/// </summary>
		public List<Swing> Swings { get; set; }

		/// <summary>
		/// Has the course reached its end, in one way or another?
		/// </summary>
		public bool HasEnded { get; set; }
		/// <summary>
		/// Has the player lost the game?
		/// </summary>
		public bool HasLost { get; set; }
		/// <summary>
		/// Has the player won the game?
		/// </summary>
		public bool HasWon { get; set; }
		/// <summary>
		/// Has the player lost the game by going out of bounds?
		/// </summary>
		public bool IsOutOfBounds { get; set; }

		/// <summary>
		/// Calculates how far has the ball travelled so far during this course.
		/// </summary>
		public double DistanceTravelled {
			get
			{
				double totalDistance = 0;
				foreach(Swing swing in Swings) {
					totalDistance += swing.Distance;
				}
				return totalDistance;
			}
		}

		/// <summary>
		/// Calculates how far from the cup the ball is at this time.
		/// </summary>
		public double DistanceFromHole
		{
			get
			{
				double distanceFromHole = CourseLength;
				foreach(Swing swing in Swings) {
					distanceFromHole = Math.Abs(distanceFromHole - swing.Distance);
				}
				return distanceFromHole;
			}
		}

		/// <summary>
		/// How long the course is, from start to finish - how far away from the starting position the cup is.
		/// </summary>
		public double CourseLength { get; set; }
		#endregion
		#region Constructors
		/// <summary>
		/// Constructor for the Course class - Initializes objects
		/// </summary>
		private Course() {
			this.Swings = new List<Swing>();
		}

		/// <summary>
		/// Constructor for the Course class, sets a max swing limit
		/// </summary>
		/// <param name="maxSwings">Max number of swings allowed on this course</param>
		public Course(int maxSwings) : this() {
			this.MaxSwings = maxSwings;
		}

		/// <summary>
		/// Main public constructor for Course. Allows setting of the values relevant to the game.
		/// </summary>
		/// <param name="courseLength">The length of the Course (how far from the starting point the cup should be)</param>
		/// <param name="maxSwings">Max number of swings allowed on this course</param>
		/// <param name="courseRough">How big the rough should be (how far away from the cup you are allowed to be)</param>
		public Course(
				double courseLength,
				int maxSwings = 8,
				double courseRough = 1000
			)
			: this(maxSwings: maxSwings) {
			this.CourseLength = courseLength;
			this.CourseRough = courseRough;
		}
		#endregion
		#region Methods

		/// <summary>
		/// Take a swing on the course, with the specified angle and velocity.
		///		Exceptions:
		///			AngleInvalidException
		/// </summary>
		/// <param name="angle">angle of the launch</param>
		/// <param name="velocity">speed of the ball</param>
		public void Swing(double angle, double velocity) {
			// Create the swing object, with the angle and velocity provided
			Swing swing = new Swing(angle, velocity, this);

			// Add the swing to the list
			Swings.Add(swing);

			// Increment the Swing Counter
			this.SwingCounter++;

			// If the distance to the hole is less than the tolerance,
			// the player has won, so end the course.
			if(this.DistanceFromHole // distance
				<= Course.Tolerance /* tolerance */) {
				this.HasEnded = true;
				this.HasWon = true;

			// If the distance is greater than the total distance plus the rough,
			// the player is out of bounds, so end the course in failure
			} else if(this.DistanceFromHole > (CourseLength + CourseRough)) {
				this.IsOutOfBounds = true;
				this.HasEnded = true;
				this.HasLost = true;
			}
		}

		#endregion
	}
}
