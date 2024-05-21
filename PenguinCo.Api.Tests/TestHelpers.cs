using System.Text;
using System.Text.Json;
using PenguinCo.Api.DTOs;

namespace PenguinCo.Api.Tests;

public static class TestHelpers
{
    public static StringContent SerialiseUpdateStoreDto(UpdateStoreDto updatedStore)
    {
        var jsonString = JsonSerializer.Serialize(updatedStore);
        StringContent contentToPut = new(jsonString, Encoding.UTF8, "application/json");

        return contentToPut;
    }

    public static async Task<(
        HttpResponseMessage,
        string,
        ReturnStoreDto
    )> UpdateStoreAndReadUpdatedStore(
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

        using (var getResponse = await client.GetAsync($"/stores/{storeToUpdate}"))
        {
            var getContent = await getResponse.Content.ReadAsStringAsync();
            try
            {
                returnStoreDto = JsonSerializer.Deserialize<ReturnStoreDto>(getContent);
            }
            catch (JsonException)
            {
                Assert.Fail("FAIL: The HTTP response message did not have any content.");
            }
        }

        return (response, content, returnStoreDto)!;
    }

    public static async Task<(
        HttpResponseMessage,
        ValidationJsonObject
    )> ReturnValidationJsonObjectFromResponse(
        HttpClient client,
        int storeToUpdate,
        StringContent contentToPut
    )
    {
        ValidationJsonObject? validationJsonObject = null;
        using var response = await client.PutAsync($"/stores/{storeToUpdate}", contentToPut);
        var content = await response.Content.ReadAsStringAsync();
        try
        {
            validationJsonObject = JsonSerializer.Deserialize<ValidationJsonObject>(content);
        }
        catch (JsonException)
        {
            Assert.Fail("FAIL: The HTTP response message did not have any content.");
        }

        return (response, validationJsonObject)!;
    }
}
