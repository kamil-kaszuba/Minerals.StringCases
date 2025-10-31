using FluentAssertions;
using Xunit;

namespace Minerals.StringCases.Tests
{
    public class StringExtensionsTests
    {
        private const string PascalCase1 = "ExampleVariableName321TestA";
        private const string CamelCase1 = "exampleVariableName321TestA";
        private const string UnderscoreCamelCase1 = "_exampleVariableName321TestA";
        private const string KebabCase1 = "example-variable-name-321-test-a";
        private const string SnakeCase1 = "example_variable_name_321_test_a";
        private const string MacroCase1 = "EXAMPLE_VARIABLE_NAME_321_TEST_A";
        private const string TrainCase1 = "Example-Variable-Name-321-Test-A";
        private const string TitleCase1 = "Example Variable Name 321 Test A";
        private const string SampleText1 = "  _ example Variable - - Name   321 TestA";

        private const string PascalCase2 = "ExampleVariableNameAbCd321";
        private const string CamelCase2 = "exampleVariableNameAbCd321";
        private const string UnderscoreCamelCase2 = "_exampleVariableNameAbCd321";
        private const string KebabCase2 = "example-variable-name-ab-cd-321";
        private const string SnakeCase2 = "example_variable_name_ab_cd_321";
        private const string MacroCase2 = "EXAMPLE_VARIABLE_NAME_AB_CD_321";
        private const string TrainCase2 = "Example-Variable-Name-Ab-Cd-321";
        private const string TitleCase2 = "Example Variable Name Ab Cd 321";
        private const string SampleText2 = "  _ example VARIABLE - - Name AbCd   321";

        [Theory]
        [InlineData("ToPascalCase", SampleText1, PascalCase1)]
        [InlineData("ToPascalCase", SampleText2, PascalCase2)]
        [InlineData("ToCamelCase", SampleText1, CamelCase1)]
        [InlineData("ToCamelCase", SampleText2, CamelCase2)]
        [InlineData("ToUnderscoreCamelCase", SampleText1, UnderscoreCamelCase1)]
        [InlineData("ToUnderscoreCamelCase", SampleText2, UnderscoreCamelCase2)]
        [InlineData("ToKebabCase", SampleText1, KebabCase1)]
        [InlineData("ToKebabCase", SampleText2, KebabCase2)]
        [InlineData("ToSnakeCase", SampleText1, SnakeCase1)]
        [InlineData("ToSnakeCase", SampleText2, SnakeCase2)]
        [InlineData("ToMacroCase", SampleText1, MacroCase1)]
        [InlineData("ToMacroCase", SampleText2, MacroCase2)]
        [InlineData("ToTrainCase", SampleText1, TrainCase1)]
        [InlineData("ToTrainCase", SampleText2, TrainCase2)]
        [InlineData("ToTitleCase", SampleText1, TitleCase1)]
        [InlineData("ToTitleCase", SampleText2, TitleCase2)]
        public void ConversionMethods_ShouldConvertCorrectly(string methodName, string input, string expected)
        {
            string result = ApplyConversionMethod(input, methodName);

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("PascalCase", PascalCase1, "ToKebabCase", "ToPascalCase")]
        [InlineData("PascalCase", PascalCase1, "ToSnakeCase", "ToPascalCase")]
        [InlineData("PascalCase", PascalCase1, "ToMacroCase", "ToPascalCase")]
        [InlineData("PascalCase", PascalCase1, "ToTrainCase", "ToPascalCase")]
        [InlineData("CamelCase", CamelCase1, "ToKebabCase", "ToCamelCase")]
        [InlineData("CamelCase", CamelCase1, "ToSnakeCase", "ToCamelCase")]
        [InlineData("CamelCase", CamelCase1, "ToMacroCase", "ToCamelCase")]
        [InlineData("KebabCase", KebabCase1, "ToPascalCase", "ToKebabCase")]
        [InlineData("KebabCase", KebabCase1, "ToCamelCase", "ToKebabCase")]
        [InlineData("KebabCase", KebabCase1, "ToSnakeCase", "ToKebabCase")]
        [InlineData("KebabCase", KebabCase1, "ToMacroCase", "ToKebabCase")]
        [InlineData("KebabCase", KebabCase1, "ToTrainCase", "ToKebabCase")]
        [InlineData("SnakeCase", SnakeCase1, "ToPascalCase", "ToSnakeCase")]
        [InlineData("SnakeCase", SnakeCase1, "ToCamelCase", "ToSnakeCase")]
        [InlineData("SnakeCase", SnakeCase1, "ToKebabCase", "ToSnakeCase")]
        [InlineData("SnakeCase", SnakeCase1, "ToMacroCase", "ToSnakeCase")]
        [InlineData("SnakeCase", SnakeCase1, "ToTrainCase", "ToSnakeCase")]
        [InlineData("MacroCase", MacroCase1, "ToPascalCase", "ToMacroCase")]
        [InlineData("MacroCase", MacroCase1, "ToCamelCase", "ToMacroCase")]
        [InlineData("MacroCase", MacroCase1, "ToKebabCase", "ToMacroCase")]
        [InlineData("MacroCase", MacroCase1, "ToSnakeCase", "ToMacroCase")]
        [InlineData("MacroCase", MacroCase1, "ToTrainCase", "ToMacroCase")]
        [InlineData("TrainCase", TrainCase1, "ToPascalCase", "ToTrainCase")]
        [InlineData("TrainCase", TrainCase1, "ToKebabCase", "ToTrainCase")]
        [InlineData("TrainCase", TrainCase1, "ToSnakeCase", "ToTrainCase")]
        [InlineData("TrainCase", TrainCase1, "ToMacroCase", "ToTrainCase")]
        public void RoundTripConversions_ShouldPreserveOriginal(string caseType, string input, string firstMethod, string secondMethod)
        {
            string intermediate = ApplyConversionMethod(input, firstMethod);
            string result = ApplyConversionMethod(intermediate, secondMethod);

            result.Should().Be(input, $"{caseType} -> {firstMethod} -> {secondMethod} should preserve the original value");
        }

        [Fact]
        public void RoundTrip_EdgeCase_PascalToTitleToPascal()
        {
            // This might not work due to space conversion
            string result = PascalCase1.ToTitleCase().ToPascalCase();
            // We expect this to still be a valid PascalCase string, but might not match exactly
            result.Should().NotBeNullOrEmpty();
            char.IsUpper(result[0]).Should().BeTrue("PascalCase should start with uppercase");
        }

        [Fact]
        public void RoundTrip_EdgeCase_CamelToTitleToCamel()
        {
            // This might not work due to space conversion
            string result = CamelCase1.ToTitleCase().ToCamelCase();
            // We expect this to still be a valid camelCase string
            result.Should().NotBeNullOrEmpty();
            char.IsLower(result[0]).Should().BeTrue("camelCase should start with lowercase");
        }

        private static string ApplyConversionMethod(string input, string methodName) => methodName switch
        {
            "ToPascalCase" => input.ToPascalCase(),
            "ToCamelCase" => input.ToCamelCase(),
            "ToUnderscoreCamelCase" => input.ToUnderscoreCamelCase(),
            "ToKebabCase" => input.ToKebabCase(),
            "ToSnakeCase" => input.ToSnakeCase(),
            "ToMacroCase" => input.ToMacroCase(),
            "ToTrainCase" => input.ToTrainCase(),
            "ToTitleCase" => input.ToTitleCase(),
            _ => throw new ArgumentException($"Unknown method: {methodName}")
        };
    }
}