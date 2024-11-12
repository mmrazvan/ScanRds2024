using ScanApp.Helpers;

namespace ScanAppTestProject.Helpers
{
	public class TimeHelpersTests
	{
		public TimeHelpersTests()
		{
		}

		[Fact]
		public void GetWorkingHoursRemainingToday_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			DateOnly date = DateOnly.FromDateTime(DateTime.Now);

			// Act
			var result = TimeHelpers.GetWorkingHoursRemainingToday(
				date);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public void CalculateSpeed_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			TimeSpan startScan = default(global::System.TimeSpan);
			TimeSpan endScan = default(global::System.TimeSpan);
			double production = 0;

			// Act
			var result = TimeHelpers.CalculateSpeed(
				startScan,
				endScan,
				production);

			// Assert
			Assert.True(false);
		}

		[Fact]
		public void GetRemainingHours_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			DateOnly date = default(global::System.DateOnly);

			// Act
			var result = TimeHelpers.GetRemainingHours(
				date);

			// Assert
			Assert.True(false);
		}
	}
}