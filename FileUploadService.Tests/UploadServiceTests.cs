using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FileUploadServer.Models;
using FileUploadService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FileUploadService.Tests
{
    [TestClass]
    public class UploadServiceTests
    {
        private IHttpClientFactory _httpClientFactory;
        private IConfiguration _config;

        [TestInitialize]
        public void Initialize()
        {
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfig = new Mock<IConfiguration>();

            var configuration = new Dictionary<string, string>
    {
        { "NotificationService:BaseUrl", "https://localhost:44357" },
        { "NotificationService:ApiKey", "ZW1haWxzZXJ2ZXJzZXJ2aWNl" }
    };
            mockConfig.Setup(x => x[It.IsAny<string>()]).Returns((string key) =>
            {
                if (configuration.ContainsKey(key))
                {
                    return configuration[key];
                }
                return null;
            });

            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(mockHttpClient.Object);

            _httpClientFactory = mockHttpClientFactory.Object;
            _config = mockConfig.Object;
        }

        [TestMethod]
        public async Task UploadFileServiceAsync_Success()
        {
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            await writer.WriteAsync("Sample file content");
            await writer.FlushAsync();
            memoryStream.Position = 0;

            var uploadRequest = new UploadRequest
            {
                FilesData = new List<IFormFile>
                {
                    new FormFile(memoryStream, 0, memoryStream.Length, "File", "sample.txt")
                },
                ToEmail = "surachachacha@example.com",
                ToEmailBody = "Test email body"
            };

            var uploadService = new UploadService(_httpClientFactory, _config);

            var response = await uploadService.UploadFileServiceAsync(uploadRequest);

            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Message);
        }

        [TestMethod]
        public async Task UploadFileServiceAsync_Failure()
        {
            var uploadRequest = new UploadRequest
            {
                FilesData = new List<IFormFile>
                {
                    new FormFile(Stream.Null, 0, 0, "File", "sample.txt")
                },
                ToEmail = "surachachacha@example.com",
                ToEmailBody = "Test email body"
            };

            var uploadService = new UploadService(_httpClientFactory, _config);

            var response = await uploadService.UploadFileServiceAsync(uploadRequest);

            Assert.IsFalse(response.Success);
            Assert.IsNotNull(response.Message);
            Assert.IsNull(response.Data);
        }
    }
}