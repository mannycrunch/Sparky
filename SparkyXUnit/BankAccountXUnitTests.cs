namespace Sparky
{
    public class BankAccountXUnitTests
    {
        private BankAccount bankAccount;

        public BankAccountXUnitTests()
        {

        }

        [Fact]
        public void BankDeposit_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message(""));

            BankAccount bankAccount = new(logMock.Object);
            var result = bankAccount.Deposit(100);
            Assert.True(result);
            Assert.Equal(100, bankAccount.GetBalance());
        }

        [Theory]
        [InlineData(200, 100)]
        [InlineData(200, 150)]
        public void BankWithdrawal100With200Balance_ReturnTrue(int balance, int withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.True(result);
        }

        [Theory]
        [InlineData(200, 300)]
        public void BankWithdraw_Withdraw300With200Balance_ReturnFalse(int balance, int withdraw)
        {
            var logMock = new Mock<ILogBook>();

            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);
            //logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x => x < 0))).Returns(false);
            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.False(result);
        }

        [Fact]
        public void BankLogDummy_LogMockString_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput = "hello";

            logMock.Setup(x => x.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());

            Assert.Equal(desiredOutput, logMock.Object.MessageWithReturnStr("HELLo"));
        }

        [Fact]
        public void BankLogDummy_LogMockOutputStr_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput = "hello";

            logMock.Setup(x => x.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);
            string result = "";
            Assert.True(logMock.Object.LogWithOutputResult("Ben", out result));
            Assert.Equal(desiredOutput, result);
        }

        [Fact]
        public void BankLogDummy_LogRefChecker_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            Customer customer = new();
            Customer customerNotUsed = new();

            logMock.Setup(x => x.LogWithRefObj(ref customer)).Returns(true);

            Assert.False(logMock.Object.LogWithRefObj(ref customerNotUsed));
            Assert.True(logMock.Object.LogWithRefObj(ref customer));
        }

        [Fact]
        public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
        {
            var logMock = new Mock<ILogBook>();
            logMock.SetupAllProperties();
            //logMock.Setup(x => x.LogSeverity).Returns(10);
            logMock.Setup(x => x.LogType).Returns("warning");
            
            logMock.Object.LogSeverity = 100;
            Assert.Equal(100, logMock.Object.LogSeverity);
            Assert.Equal("warning", logMock.Object.LogType);

            //callbacks
            string logTemp = "Hello, ";
            logMock.Setup(x => x.LogToDb(It.IsAny<string>())).Returns(true)
                .Callback((string str) => logTemp += str);
            logMock.Object.LogToDb("Ben");
            Assert.Equal("Hello, Ben", logTemp);

            //callbacks
            int counter = 5;
            logMock.Setup(x => x.LogToDb(It.IsAny<string>()))
                .Callback(() => counter++)
                .Returns(true)
                .Callback(() => counter++);
            logMock.Object.LogToDb("Ben");
            logMock.Object.LogToDb("Ben");
            Assert.Equal(9, counter);
        }

        [Fact]
        public void BankLogDummy_VerifyExample()
        {
            var logMock = new Mock<ILogBook>();
            BankAccount bankAccount = new (logMock.Object);
            bankAccount.Deposit(100);
            Assert.Equal(100, bankAccount.GetBalance());

            //verification
            logMock.Verify(x => x.Message(It.IsAny<string>()), Times.Exactly(2));
            logMock.Verify(x => x.Message("Test"), Times.AtLeastOnce);
            logMock.VerifySet(x => x.LogSeverity = 101, Times.Once);
            logMock.VerifyGet(x => x.LogSeverity, Times.Once);
        }
    }
}
