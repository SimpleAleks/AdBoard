using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AdBoard.Contracts.Advert;
using AdBoard.Domain.Advert;
using AdBoard.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace AdBoard.Api.Tests;

public class AdvertTests : IClassFixture<AdBoardWebApplicationFactory>
{
    private readonly AdBoardWebApplicationFactory _webApplicationFactory;

    public AdvertTests(AdBoardWebApplicationFactory webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Fact]
    public async Task Advert_GetAll_Success()
    {
        //Arrange
        var httpClient = _webApplicationFactory.CreateClient();
        var advertId = DataSeedHelp.TestAdvertId;
        
        //Act
        var response = await httpClient.GetAsync($"Advert");
        
        //Assert
        Assert.NotNull(response);

        var result = await response.Content.ReadFromJsonAsync<ShortAdvertDto[]>();
        
        Assert.NotNull(result);
        
        Assert.Single(result);
        var resultElement = result.First();
        Assert.Equal("test_advert_name", resultElement.Name);
        Assert.Equal((decimal)55.4, resultElement.Cost);
        Assert.Equal(advertId, resultElement.Id);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Advert_GetById_Success()
    {
        //Arrange
        var httpClient = _webApplicationFactory.CreateClient();
        var advertId = DataSeedHelp.TestAdvertId;
        using var scope = _webApplicationFactory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AdBoardDbContext>();
        var advert = db.Find<Advert>(advertId)!;
        
        //Act
        var response = await httpClient.GetAsync($"Advert/{advertId}");
        
        //Assert
        Assert.NotNull(response);

        var result = await response.Content.ReadFromJsonAsync<AdvertDto>();
        
        Assert.NotNull(result);
        
        Assert.Equal(advert.Id, result.Id);
        Assert.Equal(advert.Name, result.Name);
        Assert.Equal(advert.Description, result.Description);
        Assert.Equal(advert.Cost, result.Cost);
        Assert.Equal(advert.Email , result.Email);
        Assert.Equal(advert.Location , result.Location);
        Assert.Equal(advert.Phone , result.Phone);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Advert_Create_Success()
    {
        //Arrange
        var httpClient = _webApplicationFactory.CreateClient();

        CreateAdvertDto model = new()
        {
            Name = "test_advert_name",
            CategoryId = DataSeedHelp.TestCategoryId,
            Cost = (decimal)234.4,
            Description = "test_advert_description",
            Email = "test_advert_email",
            Location = "test_advert_location",
            Phone = "test_advert_phone",
            UserId = DataSeedHelp.TestUserId
        };
        
        var file1Content = new ByteArrayContent(File.ReadAllBytes("./../../../Files/advert_create_test.txt"));
        
        //Act
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(model.Name), nameof(model.Name).ToLower());
        content.Add(new StringContent(model.CategoryId.ToString()), nameof(model.CategoryId).ToLower());
        content.Add(new StringContent(model.Cost.ToString()), nameof(model.Cost).ToLower());
        content.Add(new StringContent(model.Description), nameof(model.Description).ToLower());
        content.Add(new StringContent(model.Email), nameof(model.Email).ToLower());
        content.Add(new StringContent(model.Location), nameof(model.Location).ToLower());
        content.Add(new StringContent(model.Phone), nameof(model.Phone).ToLower());
        content.Add(new StringContent(model.UserId.ToString()), nameof(model.UserId).ToLower());
        content.Add(file1Content, nameof(model.Images), "file1");
        var response = await httpClient.PostAsync(requestUri: "Advert", content: content);
        
        
        
        //Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ShortAdvertDto>();
        Assert.NotNull(result);
        var advertId = result.Id;
        
        using var scope = _webApplicationFactory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AdBoardDbContext>();
        var advert = await db.Set<Advert>().Where(x => x.Id == advertId).Include(x => x.Images).FirstOrDefaultAsync();
        
        Assert.NotNull(advert);
        Assert.Equal(advert.Name, model.Name);
        Assert.Equal(advert.Description, model.Description);
        Assert.Equal(advert.Cost, model.Cost);
        Assert.Equal(advert.Email , model.Email);
        Assert.Equal(advert.Location , model.Location);
        Assert.Equal(advert.Phone , model.Phone);
        Assert.Equal(await file1Content.ReadAsByteArrayAsync(), advert.Images.First().Content);
    }
    
    [Fact]
    public async Task Advert_Update_Success()
    {
        //Arrange
        var taskHttpClient = Task.Run(_webApplicationFactory.CreateClient);

        CreateAdvertDto model = new()
        {
            Name = "test_advert_name2",
            CategoryId = DataSeedHelp.TestCategoryId,
            Cost = (decimal)234.5,
            Description = "test_advert_description2",
            Email = "test_advert_email2",
            Location = "test_advert_location2",
            Phone = "test_advert_phone2",
            UserId = DataSeedHelp.TestUserId
        };
        
        //Act
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(model.Name), nameof(model.Name).ToLower());
        content.Add(new StringContent(model.CategoryId.ToString()), nameof(model.CategoryId).ToLower());
        content.Add(new StringContent(model.Cost.ToString()), nameof(model.Cost).ToLower());
        content.Add(new StringContent(model.Description), nameof(model.Description).ToLower());
        content.Add(new StringContent(model.Email), nameof(model.Email).ToLower());
        content.Add(new StringContent(model.Location), nameof(model.Location).ToLower());
        content.Add(new StringContent(model.Phone), nameof(model.Phone).ToLower());
        content.Add(new StringContent(model.UserId.ToString()), nameof(model.UserId).ToLower());
        var httpClient = await taskHttpClient;
        var response = await httpClient.PutAsync(requestUri: $"Advert/{DataSeedHelp.TestAdvertId}", content: content );
        
        
        
        //Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ShortAdvertDto>();
        Assert.NotNull(result);
        var advertId = result.Id;
        
        using var scope = _webApplicationFactory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AdBoardDbContext>();
        var advert = await db.Set<Advert>().Where(x => x.Id == advertId).Include(x => x.Images).FirstOrDefaultAsync();
        
        Assert.NotNull(advert);
        Assert.Equal(advert.Name, model.Name);
        Assert.Equal(advert.Description, model.Description);
        Assert.Equal(advert.Cost, model.Cost);
        Assert.Equal(advert.Email , model.Email);
        Assert.Equal(advert.Location , model.Location);
        Assert.Equal(advert.Phone , model.Phone);
    }
}