using InMetrics.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace InMetrics.BusinessRules.UnitTests
{
    public class GreaterThanZeroDecimalAttributeUnitTests
    {
        private List<ValidationResult> ValidationResults;

        [SetUp]
        public void Setup()
        {
            ValidationResults = new List<ValidationResult>();
        }

        [TestCase("-100",false)]
        [TestCase("-9999999",false)]
        [TestCase("-1",false)]
        [TestCase("0",false)]
        [TestCase("1000",true)]
        public void Valid_When_Greater_Than_Zerp(decimal value, bool expected)
        {
            // Arrange
            var transaction = new Transaction { Amount = value };
            var validationContext = new ValidationContext(transaction, null, null);
            // Act
            var isValid = Validator.TryValidateObject(transaction, validationContext, ValidationResults, true);
            // Assert
            Assert.AreEqual(expected, isValid);
        }
    }
}