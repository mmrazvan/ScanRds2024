using Bogus;

using DataAccess.Models;

using FluentAssertions;

using Microsoft.IdentityModel.Tokens;

using ScanApp.Helpers;

namespace ScanAppTestProject.Helpers
{
	public class ShiftHelpersTests
	{
		[Fact]
		public void GetShifts_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			DateOnly date = DateOnly.FromDateTime(DateTime.Now);

			List<Opis> opis = new Faker<Opis>("ro")
				.RuleFor(o => o.Data, f => DateTime.Now)
				.RuleFor(o => o.Cantitate, f => f.Random.Int())
				.RuleFor(o => o.Judet, f => f.Address.County())
				.RuleFor(o => o.Oras, f => f.Address.County())
				.RuleFor(o => o.Cantitate, f => f.Random.Int(100, 1000))
				.RuleFor(o => o.Data, f => f.Date.Recent(1))
				.Generate(30);

			// Act
			var result = ShiftHelpers.GetShifts(date, opis);

			// Assert
			result.IsNullOrEmpty().Should().BeFalse();
			result.Count.Should().BeInRange(1, 3);
		}
	}
}