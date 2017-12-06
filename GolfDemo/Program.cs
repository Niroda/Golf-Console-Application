using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfDemo {
	public class Program {
		static void Main(string[] args) {
			// Generate a random length for the course, between 200 and 800 meters
			Random rng = new Random();
			double length = rng.Next(200, 800);
			int maxSwings = (int)(length / 100) + 1;

			// Create the course, with the length we generated above
			Course course = new Course(courseLength: length, maxSwings: maxSwings);

			// Main Game Loop - will be maintained as long as the game is running.
			while(!course.HasEnded) {
				// Output the current state of the game to the console
				Console.WriteLine($"Course Length {course.CourseLength} meters");
				Console.WriteLine($"Course Rough {course.CourseRough} meters (Max bounds: {course.CourseRough + course.CourseLength} meters)");
                Console.WriteLine($"Distance to Hole: {course.DistanceFromHole.ToString("0.00")} meters");
				Console.WriteLine($"You have taken {course.SwingCounter} swings at this hole. (Max is {course.MaxSwings})");
                Console.WriteLine("----------------");

				// Print every swing taken so far
				foreach(var item in course.Swings) {
					Console.WriteLine($"\tAngle:{item.Angle}\tVelocity:{item.Velocity}\tDistance:{item.Distance.ToString("N2")} meters");
				}
				Console.WriteLine("----------------");
				// Get the angle of the next swing from the user
				Console.Write("Angle: ");
				double inValue1 = double.Parse(Console.ReadLine());
				// Get the velocity of the next swing from the user
				Console.Write("Velocity (only positive values): ");
				double inValue2 = double.Parse(Console.ReadLine());

				// Try to take the swing - if the angle is invalid, it will fail
				try {
					course.Swing(angle: inValue1, velocity: inValue2);
				} catch(AngleInvalidExeption e) {
					Console.WriteLine(e.Message);
					continue;
				}
				// Output the distance of the swing.
				Console.WriteLine($"Distance: {course.Swings.Last().Distance.ToString("N2")} meters");
				Console.WriteLine("----------------");
				// Wait for the player to press a button.
				Console.ReadKey();
				Console.Clear();
			}
			// Print the result of the course - if you failed, and if so how.
			if(course.HasLost) {
				Console.WriteLine("You failed to reach the cup.");
				Console.WriteLine($"Distance to Hole: {course.DistanceFromHole.ToString("0.00")} meters");
				if(course.IsOutOfBounds) {
					Console.WriteLine("Because the golf ball is out of bounds.");
				}
				if(course.SwingCounter >= course.MaxSwings) {
					Console.WriteLine("Because you took too many swings.");
				}
			} else if(course.HasWon) {
				Console.WriteLine("You reached the cup.");
				if(course.SwingCounter == 1) {
					Console.WriteLine("Hole in one!");
				}
			}
			Console.ReadKey();
		}
	}
}
