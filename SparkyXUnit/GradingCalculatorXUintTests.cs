namespace Sparky
{
    public class GradingCalculatorXUintTests
    {
        private GradingCalculator GradingCalc;
        private string grade;

        public GradingCalculatorXUintTests()
        {
            GradingCalc = new GradingCalculator();
            grade = "";
        }

        [Fact]
        public void GetGrade_InputScore95AndAttendance90_GetGradeA()
        {
            GradingCalc.Score = 95;
            GradingCalc.AttendancePercentage = 90;

            var result = GradingCalc.GetGrade();

            Assert.Equal("A", result);
        }

        [Fact]
        public void GetGrade_InputScore85AndAttendance90_GetGradeB()
        {
            GradingCalc.Score = 85;
            GradingCalc.AttendancePercentage = 90;

            grade = GradingCalc.GetGrade();

            Assert.Equal("B", grade);
        }

        [Fact]
        public void GetGrade_InputScore65AndAttendance90_GetGradeC()
        {
            GradingCalc.Score = 65;
            GradingCalc.AttendancePercentage = 90;

            grade = GradingCalc.GetGrade();

            Assert.Equal("C", grade);
        }

        [Fact]
        public void GetGrade_InputScore95AndAttendance65_GetGradeB()
        {
            GradingCalc.Score = 95;
            GradingCalc.AttendancePercentage = 65;

            grade = GradingCalc.GetGrade();

            Assert.Equal("B", grade);
        }

        [Theory]
        [InlineData(95, 55)]
        [InlineData(65, 55)]
        [InlineData(50, 90)]
        public void GetGrade_FailureScenarios_GetGradeF(int score, int attPercent)
        {
            GradingCalc.Score = score;
            GradingCalc.AttendancePercentage = attPercent;
            grade = GradingCalc.GetGrade();
            Assert.Equal("F", grade);
        }

        [Theory]
        [InlineData(95, 90, "A")]
        [InlineData(85, 90, "B")]
        [InlineData(65, 90, "C")]
        [InlineData(95, 65, "B")]
        [InlineData(95, 55, "F")]
        [InlineData(65, 55, "F")]
        [InlineData(50, 90, "F")]
        public void GetGrade_AllLogicalScenarios_GradeOutput(int score, int attPercent, string expectedResult)
        {
            GradingCalc.Score = score;
            GradingCalc.AttendancePercentage = attPercent;
            grade = GradingCalc.GetGrade();
            Assert.Equal(expectedResult, grade);
        }
    }
}
