using System.Text;
using System.Text.Json;
using PenguinCo.Api.DTOs;

namespace PenguinCo.Api.Tests;

public static class TestHelpers
{
    private static T DeserialiseContent<T>(string content)
    {
        T? deserialised = default;
        try
        {
            deserialised = JsonSerializer.Deserialize<T>(content);
        }
        catch (JsonException)
        {
            Assert.Fail("FAIL: The HTTP response message did not have any content.");
        }

        return deserialised!;
    }

    private static async Task<ValidationJsonObject> ReturnValidationJsonObjectFromResponseAsync(
        HttpResponseMessage response
    )
    {
        var content = await response.Content.ReadAsStringAsync();
        var validationJsonObject = DeserialiseContent<ValidationJsonObject>(content);

        return validationJsonObject;
    }

    private static async Task<(HttpResponseMessage, string)> ReturnContentOnReadAsync(
        HttpClient client
    )
    {
        using var response = await client.GetAsync($"/stores");
        var content = await response.Content.ReadAsStringAsync();

        return (response, content);
    }

    public static StringContent SerialiseDto(ICUStoreDto storeDto)
    {
        var jsonString = JsonSerializer.Serialize(storeDto);
        StringContent contentToPut = new(jsonString, Encoding.UTF8, "application/json");

        return contentToPut;
    }

    // POST
    public static async Task<(
        HttpResponseMessage,
        ReturnStoreDto
    )> ReturnReturnStoreDtoOnCreateAsync(HttpClient client, StringContent contentToPost)
    {
        using var response = await client.PostAsync($"/stores", contentToPost);
        var content = await response.Content.ReadAsStringAsync();
        var returnStoreDto = DeserialiseContent<ReturnStoreDto>(content);

        return (response, returnStoreDto);
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
    public static async Task<(HttpResponseMessage, List<StoreDto>)> ReturnStoreDtosOnReadAsync(
        HttpClient client
    )
    {
        var (response, content) = await ReturnContentOnReadAsync(client);
        var storeDtos = DeserialiseContent<List<StoreDto>>(content);

        return (response, storeDtos);
    }

    public static async Task<(HttpResponseMessage, StoreDto)> ReturnStoreDtoOnReadAsync(
        HttpClient client,
        int storeToGet
    )
    {
        var (response, content) = await ReturnContentOnReadAsync(client, storeToGet);
        var storeDto = DeserialiseContent<StoreDto>(content);

        return (response, storeDto);
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
        using var response = await client.PutAsync($"/stores/{storeToUpdate}", contentToPut);
        var content = await response.Content.ReadAsStringAsync();

        var (_, getContent) = await ReturnContentOnReadAsync(client, storeToUpdate);
        var returnStoreDto = DeserialiseContent<ReturnStoreDto>(getContent);

        return (response, content, returnStoreDto);
    }
}
