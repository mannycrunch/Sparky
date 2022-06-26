using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class GradingCalculatorNUintTests
    {
        private GradingCalculator GradingCalc;
        private string grade;

        [SetUp]
        public void Setup()
        {
            GradingCalc = new GradingCalculator();
            grade = "";
        }

        [Test]
        public void GetGrade_InputScore95AndAttendance90_GetGradeA()
        {
            GradingCalc.Score = 95;
            GradingCalc.AttendancePercentage = 90;

            grade = GradingCalc.GetGrade();

            Assert.That(grade, Is.EqualTo("A"));
        }

        [Test]
        public void GetGrade_InputScore85AndAttendance90_GetGradeB()
        {
            GradingCalc.Score = 85;
            GradingCalc.AttendancePercentage = 90;

            grade = GradingCalc.GetGrade();

            Assert.That(grade, Is.EqualTo("B"));
        }

        [Test]
        public void GetGrade_InputScore65AndAttendance90_GetGradeC()
        {
            GradingCalc.Score = 65;
            GradingCalc.AttendancePercentage = 90;

            grade = GradingCalc.GetGrade();

            Assert.That(grade, Is.EqualTo("C"));
        }

        [Test]
        public void GetGrade_InputScore95AndAttendance65_GetGradeB()
        {
            GradingCalc.Score = 95;
            GradingCalc.AttendancePercentage = 65;

            grade = GradingCalc.GetGrade();

            Assert.That(grade, Is.EqualTo("B"));
        }

        [Test]
        [TestCase(95, 55)]
        [TestCase(65, 55)]
        [TestCase(50, 90)]
        public void GetGrade_FailureScenarios_GetGradeF(int score, int attPercent)
        {
            GradingCalc.Score = score;
            GradingCalc.AttendancePercentage = attPercent;
            grade = GradingCalc.GetGrade();
            Assert.That(grade, Is.EqualTo("F"));
        }

        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 65, ExpectedResult = "B")]
        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string GetGrade_AllLogicalScenarios_GradeOutput(int score, int attPercent)
        {
            GradingCalc.Score = score;
            GradingCalc.AttendancePercentage = attPercent;
            return GradingCalc.GetGrade();
        }
    }
}
