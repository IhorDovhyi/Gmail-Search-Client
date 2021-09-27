using Gmail_Search_Client.Controllers;
using Gmail_Search_Client.Models;
using Gmail_Search_Client.Models.GoogleModels;
using Google.Apis.Gmail.v1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace GmailSearchClientTests
{
    [TestClass]
    public class GmailTest
    {
        [TestMethod]
        public void GoogleMessageTest_SetMessageBodyMethod_CorrectStringExpected()
        {
            // arrange
            var testMessage = new GoogleMessage();
            var testString = "PHA+VGVzdDwvcD4=";
            var expected = "<p>Test</p>";
            // act
            testMessage.SetMessageBody(testString);
            // assert
            Assert.AreEqual(expected: expected, actual: testMessage.MessageBody);
        }

        [TestMethod]
        public void GoogleMessageTest_SetLabelsMethod_CorrectListLabels()
        {
            // arrange
            var testMessage = new GoogleMessage();
            var testList = new List<string>() { "CATEGORY_PROMOTIONS", "UNREAD", "INBOX" };
            var expected = new List<string>() { "CATEGORY PROMOTIONS", "UNREAD", "INBOX" };
            // act
            testMessage.SetLabels(testList);
            // assert
            CollectionAssert.AreEqual(expected: expected, actual: testMessage.Labels);
            Assert.AreEqual(expected: expected.Count, actual: testMessage.Labels.Count);
        }

        [TestMethod]
        public void GoogleMessageTest_GetLabelsWithMessageBodyMethod_CorrectStringExpected()
        {
            // arrange
            var testMessage = new GoogleMessage();
            var testBody = "<p>Test</p>";
            testMessage.MessageBody = testBody;
            testMessage.Labels = new List<string>() { "CATEGORY PROMOTIONS", "UNREAD", "INBOX" };
            var expected = "CATEGORY PROMOTIONS UNREAD INBOX <p>Test</p>";
            // act
            var actual = testMessage.GetLabelsWithMessageBody();
            // assert
            Assert.AreEqual(expected: expected, actual: actual);
        }

        [TestMethod]
        public void GoogleMessageTest_SetDateMethod_CorrectDateExpected()
        {
            // arrange
            var testMessage = new GoogleMessage();
            var testDate = new DateTime(2021, 9, 9, 9, 41, 59);
            var expected = new DateTime(2021, 9, 9, 12, 41, 59); ;
            // act
            testMessage.SetDate(testDate);
            // assert
            Assert.AreEqual(expected: expected, actual: testMessage.DateReceived);
        }

        [TestMethod]
        public void GoogleMessagePage_SetPageType_CorrectPageTypeExpected()
        {
            // arrange
            var mock = new Mock<GmailService>();
            var testMessagePage = new GoogleMessagePage(mock.Object);
            var testPageType = PossiblePageTypes.Starred;
            // act
            testMessagePage.PageType = testPageType;
            // assert
            Assert.AreEqual(expected: testPageType, actual: testMessagePage.PageType);
        }

        [TestMethod]
        public void GoogleMessagePage_SetPageType_IncorrectPageTypeExpected()
        {
            // arrange
            var mock = new Mock<GmailService>();
            var testMessagePage = new GoogleMessagePage(mock.Object);
            var testPageType = "test";
            var expected = PossiblePageTypes.None;
            // act
            testMessagePage.PageType = testPageType;
            // assert
            Assert.AreEqual(expected: expected, actual: testMessagePage.PageType);
        }

        [TestMethod]
        public void GoogleMessagePage_SetMessagesInPage_CorrectMessagesInPageExpected()
        {
            // arrange
            var mock = new Mock<GmailService>();
            var testMessagePage = new GoogleMessagePage(mock.Object);
            var testMessagesInPage = 11;
            // act
            testMessagePage.MessagesInPage = testMessagesInPage;
            // assert
            Assert.AreEqual(expected: testMessagesInPage, actual: testMessagePage.MessagesInPage);
        }

        [TestMethod]
        public void GoogleMessagePage_SetMessagesInPage_IncorrectMessagesInPageExpected()
        {
            // arrange
            var mock = new Mock<GmailService>();
            var testMessagePage = new GoogleMessagePage(mock.Object);
            var testMessagesInPage = 9;
            var expected = 10;
            // act
            testMessagePage.MessagesInPage = testMessagesInPage;
            // assert
            Assert.AreEqual(expected: expected, actual: testMessagePage.MessagesInPage);
        }

        [TestMethod]
        public void GoogleMessagePage_SetPageNumber_CorrectPageNumberExpected()
        {
            // arrange
            var mock = new Mock<GmailService>();
            var testMessagePage = new GoogleMessagePage(mock.Object);
            var testPageNumber = 12;
            // act
            testMessagePage.PageNumber = testPageNumber;
            // assert
            Assert.AreEqual(expected: testPageNumber, actual: testMessagePage.PageNumber);
        }

        [TestMethod]
        public void GoogleMessagePage_SetPageNumber_IncorrectPageNumberExpected()
        {
            // arrange
            var mock = new Mock<GmailService>();
            var testMessagePage = new GoogleMessagePage(mock.Object);
            var testPageNumber = 0;
            var expected = 10;
            // act
            testMessagePage.PageNumber = testPageNumber;
            // assert
            Assert.AreEqual(expected: expected, actual: testMessagePage.PageNumber);
        }

        [TestMethod]
        public void IUseCaseAdapter_GetAllMessages_CorrectGoogleMessagePageExpected()
        {
            // arrange
            var mockUseCase = new Mock<IUseCaseAdapter>();
            var mockService = new Mock<GmailService>();
            mockUseCase.Setup(m => m.GetAllMessages()).Returns(new GoogleMessagePage(mockService.Object));
            var expectedGoogleMessagePage = new GoogleMessagePage(mockService.Object);
            // act
            var actual = mockUseCase.Object.GetAllMessages();
            // assert
            Assert.AreEqual(expected: expectedGoogleMessagePage.GetType(), actual: actual.GetType());
            Assert.AreEqual(expected: expectedGoogleMessagePage.PageType, actual: actual.PageType);
            Assert.AreEqual(expected: expectedGoogleMessagePage.MessagesInPage, actual: actual.MessagesInPage);
            Assert.AreEqual(expected: expectedGoogleMessagePage.PageNumber, actual: actual.PageNumber);
        }

        [TestMethod]
        public void IUseCaseAdapter_PageByNumber_CorrectGoogleMessagePageExpected()
        {
            // arrange
            var pageNumber = 1;
            var mockUseCase = new Mock<IUseCaseAdapter>();
            var mockService = new Mock<GmailService>();
            mockUseCase.Setup(m => m.GetPageByNumber(pageNumber)).Returns(new GoogleMessagePage(mockService.Object));
            var expectedGoogleMessagePage = new GoogleMessagePage(mockService.Object);
            expectedGoogleMessagePage.PageNumber = pageNumber;
            // act
            var actual = mockUseCase.Object.GetPageByNumber(pageNumber);
            // assert
            Assert.AreEqual(expected: expectedGoogleMessagePage.GetType(), actual: actual.GetType());
            Assert.AreEqual(expected: expectedGoogleMessagePage.PageType, actual: actual.PageType);
            Assert.AreEqual(expected: expectedGoogleMessagePage.MessagesInPage, actual: actual.MessagesInPage);
            Assert.AreEqual(expected: expectedGoogleMessagePage.PageNumber, actual: actual.PageNumber);
        }
    }
}
