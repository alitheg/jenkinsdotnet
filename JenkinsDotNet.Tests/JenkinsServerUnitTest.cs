using System;
using System.Threading.Tasks;
using JenkinsDotNet.Interfaces;
using JenkinsDotNet.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino;
using Rhino.Mocks;

namespace JenkinsDotNet.Tests
{

    // ReSharper disable ConvertToConstant.Local
    [TestClass]
    public class JenkinsServerUnitTest
    {
        readonly string _url = "http://www.example.com";
        readonly string _userName = "testUser";
        readonly string _apiKey = "someNewApiKey";
        readonly string _name = "Test Jenkins Server";
        readonly string _jobName = "Test Jenkins Job";
        readonly object[] _jobNameList = new object[1];
        private IJenkinsDataService _mockDataService;
        private JenkinsServer _targetServer;

        [TestInitialize]
        public void InitTests()
        {
            // Stub DataService and setupexpected return
            _mockDataService = MockRepository.GenerateStub<IJenkinsDataService>();
            _targetServer = new JenkinsServer(_mockDataService, _url, _userName, _apiKey, _name);
            _jobNameList[0] = _jobName;

        }

        [TestCleanup]
        public void CleanupTests()
        {
            _mockDataService.BackToRecord(BackToRecordOptions.All);
            _targetServer = null;
        }

        [TestMethod]
        public void JenkinsServer_ConstructedSuccessfully()
        {
            // Arrange

            // Act
            _targetServer = new JenkinsServer(_url, _userName, _apiKey, _name);

            // Assert
            Assert.IsInstanceOfType(_targetServer, typeof(JenkinsServer));
            Assert.AreEqual(_targetServer.Url, _url);
            Assert.AreEqual(_targetServer.UserName, _userName);
            Assert.AreEqual(_targetServer.ApiKey, _apiKey);
            Assert.AreEqual(_targetServer.Name, _name);
        }

        [TestMethod]
        public void GetNodeDetails_ServiceAvailable_NodeReturned()
        {
            // Arrange
            var expected = new Node();
            var expectedTask = new Task<Node>(() => expected);
            expectedTask.Start();

            // VS reports a compiler error here but tests run fine...
            _mockDataService.Expect(ds => ds.RequestAsync<Node>(Arg.Is(URL.Api), Arg.Is(_url), Arg.Is(_userName), Arg.Is(_apiKey), Arg<object[]>.Is.Anything))
                           .Return(expectedTask);
            //mockDataService.Expect(
            //    ds =>ds.RequestAsync<Node>(Arg.Is(URL.Api), Arg.Is(url), Arg.Is(userName), Arg.Is(apiKey),Arg<object[]>.Is.Anything));

            // Act
            var actual = _targetServer.GetNodeDetails();

            // Assert
            Assert.AreEqual(actual, expected);
            _mockDataService.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetNodeDetailsAsync_ServiceAvailable_NodeReturned()
        {
            // Arrange
            var expected = new Node();
            var expectedTask = new Task<Node>(() => expected);
            expectedTask.Start();

            // VS reports a compiler error here but tests run fine...
            _mockDataService.Expect(ds => ds.RequestAsync<Node>(Arg.Is(URL.Api), Arg.Is(_url), Arg.Is(_userName), Arg.Is(_apiKey), Arg<object[]>.Is.Anything))
                           .Return(expectedTask);

            // Act
            var actualTask = _targetServer.GetNodeDetailsAsync();
            actualTask.Wait();
            var actual = actualTask.Result;

            // Assert
            Assert.AreEqual(actual, expected);
            _mockDataService.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetJobDetails_ServiceAvailable_JobReturned()
        {
            // Arrange
            var expected = new Job { Name = _jobName };
            var expectedTask = new Task<Job>(() => expected);
            expectedTask.Start();

            // VS reports a compiler error here but tests run fine...
            _mockDataService.Expect(ds => ds.RequestAsync<Job>(Arg.Is(URL.Job), Arg.Is(_url), Arg.Is(_userName), Arg.Is(_apiKey), Arg<object[]>.List.ContainsAll(_jobNameList)))
                           .Return(expectedTask);

            // Act
            var actual = _targetServer.GetJobDetails(_jobName);

            // Assert
            Assert.AreEqual(actual, expected);
            _mockDataService.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetJobDetailsAsync_ServiceAvailable_JobReturned()
        {
            // Arrange
            var expected = new Job { Name = _jobName };
            var expectedTask = new Task<Job>(() => expected);
            expectedTask.Start();

            // VS reports a compiler error here but tests run fine...
            _mockDataService.Expect(ds => ds.RequestAsync<Job>(Arg.Is(URL.Job), Arg.Is(_url), Arg.Is(_userName), Arg.Is(_apiKey), Arg<object[]>.List.ContainsAll(_jobNameList)))
                           .Return(expectedTask);
            //mockDataService.Expect(
            //    ds =>ds.RequestAsync<Node>(Arg.Is(URL.Api), Arg.Is(url), Arg.Is(userName), Arg.Is(apiKey),Arg<object[]>.Is.Anything));

            _targetServer = new JenkinsServer(_mockDataService, _url, _userName, _apiKey, _name);

            // Act
            var actualTask = _targetServer.GetJobDetailsAsync(_jobName);
            actualTask.Wait();
            var actual = actualTask.Result;

            // Assert
            Assert.AreEqual(actual, expected);
            _mockDataService.VerifyAllExpectations();
        }
    }
    // ReSharper restore ConvertToConstant.Local
}
