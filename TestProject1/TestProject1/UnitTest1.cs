using NUnit.Framework;
using RestSharp;
using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using AventStack.ExtentReports.Reporter.Config;

namespace APITesting
{
    [TestFixture]
    public class ApiTests : IDisposable
    {
        private RestClient _client;
        private ExtentReports _extent;
        private ExtentTest _test;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Initialize ExtentReports
            _extent = new ExtentReports();
            var sparkReporter = new ExtentSparkReporter("TestReport.html");
            sparkReporter.Config.DocumentTitle = "API Test Report";
            sparkReporter.Config.ReportName = "API Testing with NUnit and RestSharp";
            sparkReporter.Config.Theme = Theme.Standard;

            _extent.AttachReporter(sparkReporter);

        }

        [SetUp]
        public void Setup()
        {
            _client = new RestClient("https://jsonplaceholder.typicode.com");
        }

        [Test, Order(1)]
        public void Test_GetPostById()
        {
            _test = _extent.CreateTest("Test_GetPostById");

            var request = new RestRequest("/posts/1", Method.Get);
            var response = _client.Execute(request);

            _test.Info("GET /posts/1 Request sent");
            _test.Info("Response: " + response.Content);

            // Verify status code
            Assert.AreEqual(200, (int)response.StatusCode, "Status code should be 200");
            _test.Pass("Status code is 200");

            // Verify response body
            Assert.IsTrue(response.Content.Contains("\"userId\": 1"));
            _test.Pass("Response contains userId: 1");
        }

        [Test, Order(2)]
        public void Test_CreateNewPost()
        {
            _test = _extent.CreateTest("Test_CreateNewPost");

            var request = new RestRequest("/posts", Method.Post);
            var postData = new
            {
                userId = 1,
                title = "New Post Title",
                body = "This is the body content of the new post."
            };
            request.AddJsonBody(postData);

            var response = _client.Execute(request);

            _test.Info("POST Request sent to: " + request.Resource);
            _test.Info("Request Body: " + JsonConvert.SerializeObject(postData));
            _test.Info("Response: " + response.Content);

            Assert.AreEqual(201, (int)response.StatusCode, "Status code should be 201");
            _test.Pass("Status code is 201");

            Assert.IsTrue(response.Content.Contains("\"title\": \"New Post Title\""));
            _test.Pass("Response contains the new post title");
        }

        [Test, Order(3)]
        public void Test_UpdatePost()
        {
            _test = _extent.CreateTest("Test_UpdatePost");

            var request = new RestRequest("/posts/1", Method.Put);
            var putData = new
            {
                userId = 1,
                id = 1,
                title = "Updated Title",
                body = "Updated body content."
            };
            request.AddJsonBody(putData);
            var response = _client.Execute(request);

            _test.Info("PUT Request sent to: " + request.Resource);
            _test.Info("Request Body: " + JsonConvert.SerializeObject(putData));
            _test.Info("Response: " + response.Content);

            Assert.AreEqual(200, (int)response.StatusCode, "Status code should be 200");
            _test.Pass("Status code is 200");

            Assert.IsTrue(response.Content.Contains("\"title\": \"Updated Title\""));
            _test.Pass("Response contains the updated title");
        }

        [Test, Order(4)]
        public void Test_DeletePost()
        {
            _test = _extent.CreateTest("Test_DeletePost");

            var request = new RestRequest("/posts/1", Method.Delete);
            var response = _client.Execute(request);

            _test.Info("DELETE Request sent to: " + request.Resource);
            _test.Info("Response: " + response.Content);

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK ||
                          response.StatusCode == System.Net.HttpStatusCode.NoContent, "Status code should be 200 or 204");
            _test.Pass("Status code is 200 or 204");

            Assert.IsTrue(response.Content == null || response.Content.Trim() == "{}", "Expected response body to be empty or contain an empty JSON object.");
            _test.Pass("Response body is empty as expected");
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                _test.Pass("Test Passed");
            }
            else
            {
                _test.Fail("Test Failed: " + TestContext.CurrentContext.Result.Message);
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _extent.Flush();
        }
            
        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}


