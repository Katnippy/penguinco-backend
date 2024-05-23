using System.Text;
using System.Text.Json;
using PenguinCo.Api.DTOs;

namespace PenguinCo.Api.Tests;

public static class TestHelpers
{
    private static async Task<ValidationJsonObject> ReturnValidationJsonObjectFromResponseAsync(
        HttpResponseMessage response
    )
    {
        ValidationJsonObject? validationJsonObject = null;
        var content = await response.Content.ReadAsStringAsync();
        try
        {
            validationJsonObject = JsonSerializer.Deserialize<ValidationJsonObject>(content);
        }
        catch (JsonException)
        {
            Assert.Fail("FAIL: The HTTP response message did not have any content.");
        }

        return validationJsonObject!;
    }

    public static StringContent SerialiseDto(ICUStoreDto storeDto)
    {
        var jsonString = JsonSerializer.Serialize(storeDto);
        StringContent contentToPut = new(jsonString, Encoding.UTF8, "application/json");

        return contentToPut;
    }

    public static async Task<(HttpResponseMessage, string)> ReturnContentOnReadAsync(
        HttpClient client,
        int storeToGet
    )
    {
        using var response = await client.GetAsync($"/stores/{storeToGet}");
        var content = await response.Content.ReadAsStringAsync();

        return (response, content);
    }

    public static async Task<(HttpResponseMessage, string)> ReturnContentOnReadAsync(
        HttpClient client,
        string storeToGet
    )
    {
        using var response = await client.GetAsync($"/stores/{storeToGet}");
        var content = await response.Content.ReadAsStringAsync();

        return (response, content);
    }

    // POST
    public static async Task<(
        HttpResponseMessage,
        ReturnStoreDto
    )> ReturnReturnStoreDtoOnCreateAsync(HttpClient client, StringContent contentToPost)
    {
        HttpResponseMessage response;
        ReturnStoreDto? returnStoreDto = null;
        using (response = await client.PostAsync($"/stores", contentToPost))
        {
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                returnStoreDto = JsonSerializer.Deserialize<ReturnStoreDto>(content);
            }
            catch (JsonException)
            {
                Assert.Fail("FAIL: The HTTP response message did not have any content.");
            }
        }

        return (response, returnStoreDto!);
    }

    public static async Task<(
        HttpResponseMessage,
        ValidationJsonObject
    )> ReturnValidationJsonObjectOnCreateAsync(HttpClient client, StringContent contentToPost)
    {
        using var response = await client.PostAsync($"/stores", contentToPost);
        var validationJsonObject = await ReturnValidationJsonObjectFromResponseAsync(response);

        return (response, validationJsonObject);
    }

    // GET

    // PUT
    public static async Task<(
        HttpResponseMessage,
        ValidationJsonObject
    )> ReturnValidationJsonObjectOnUpdateAsync(
        HttpClient client,
        int storeToUpdate,
        StringContent contentToPut
    )
    {
        using var response = await client.PutAsync($"/stores/{storeToUpdate}", contentToPut);
        var validationJsonObject = await ReturnValidationJsonObjectFromResponseAsync(response);

        return (response, validationJsonObject);
    }

    public static async Task<(
        HttpResponseMessage,
        string,
        ReturnStoreDto
    )> UpdateStoreAndReadUpdatedStoreAsync(
        HttpClient client,
        int storeToUpdate,
        StringContent contentToPut
    )
    {
        HttpResponseMessage? response;
        string content;
        ReturnStoreDto? returnStoreDto = null;

        using (response = await client.PutAsync($"/stores/{storeToUpdate}", contentToPut))
        {
            content = await response.Content.ReadAsStringAsync();
        }

        var (_, getContent) = await ReturnContentOnReadAsync(client, storeToUpdate);
        try
        {
            returnStoreDto = JsonSerializer.Deserialize<ReturnStoreDto>(getContent);
        }
        catch (JsonException)
        {
            Assert.Fail("FAIL: The HTTP response message did not have any content.");
        }

        return (response, content, returnStoreDto)!;
    }
}
